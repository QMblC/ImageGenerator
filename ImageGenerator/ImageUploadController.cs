using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ImageGenerator
{
    using ImageGenerator.Data;
    using ImageGenerator.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Text.Json;

    [ApiController]
    [Route("api/[controller]")]
    public class ImageUploadController : ControllerBase
    {
        public class ImageJson
        {
            public string title { get; set; }
            public double xPos { get; set; }
            public double yPos { get; set; }
            public double scaleFactor { get; set; }
            public double fontSize { get; set; }
        }

        public class ImageUploadRequest
        {
            public IFormFile File { get; set; }
            public IFormFile MetadataJson { get; set; }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage(int id, [FromServices] AppDbContext context)
        {
            var imageEntity = await context.Images.FindAsync(id);
            if (imageEntity == null)
                return NotFound();

            var metadata = new
            {
                Title = imageEntity.title,
                imageEntity.xPos,
                imageEntity.yPos,
                imageEntity.scaleFactor,
                imageEntity.fontSize
            };

            return Ok(new
            {
                FileName = imageEntity.fileName,
                FileDataBase64 = Convert.ToBase64String(imageEntity.fileData),
                Metadata = metadata
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id, [FromServices] AppDbContext context)
        {
            var imageEntity = await context.Images.FindAsync(id);
            if (imageEntity == null)
                return NotFound($"Изображение с Id={id} не найдено.");

            context.Images.Remove(imageEntity);
            await context.SaveChangesAsync();

            return Ok(new { Message = $"Изображение с Id={id} успешно удалено." });
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateImage(int id, [FromForm] IFormFile MetadataJson, [FromServices] AppDbContext context)
        {
            if (MetadataJson == null || MetadataJson.Length == 0)
                return BadRequest("Файл метаданных не загружен.");

            ImageJson metadata;
            using (var reader = new StreamReader(MetadataJson.OpenReadStream()))
            {
                var jsonString = await reader.ReadToEndAsync();
                try
                {
                    metadata = JsonSerializer.Deserialize<ImageJson>(jsonString);
                }
                catch (JsonException ex)
                {
                    return BadRequest($"Ошибка разбора JSON: {ex.Message}");
                }
            }

            var imageEntity = await context.Images.FindAsync(id);
            if (imageEntity == null)
                return NotFound();

            imageEntity.title = metadata.title;
            imageEntity.xPos = metadata.xPos;
            imageEntity.yPos = metadata.yPos;
            imageEntity.scaleFactor = metadata.scaleFactor;
            imageEntity.fontSize = metadata.fontSize;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при обновлении: {ex.Message}");
            }

            return Ok(new { Message = "Данные успешно обновлены" });
        }



        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequest request, [FromServices] AppDbContext context)
        {
            if (request.File == null || request.File.Length == 0)
                return BadRequest("Файл изображения не загружен.");

            if (request.MetadataJson == null || request.MetadataJson.Length == 0)
                return BadRequest("Файл метаданных не загружен.");

            ImageJson metadata;

            using (var reader = new StreamReader(request.MetadataJson.OpenReadStream()))
            {
                var jsonString = await reader.ReadToEndAsync();

                try
                {
                    metadata = JsonSerializer.Deserialize<ImageJson>(jsonString);
                }
                catch (JsonException ex)
                {
                    return BadRequest($"Ошибка разбора JSON: {ex.Message}");
                }
            }

            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                await request.File.CopyToAsync(ms);
                fileBytes = ms.ToArray();
            }

            var imageEntity = new ImageEntity
            {
                fileName = request.File.FileName,
                fileData = fileBytes,
                title = metadata.title,
                xPos = metadata.xPos,
                yPos = metadata.yPos,
                scaleFactor = metadata.scaleFactor,
                fontSize = metadata.fontSize
            };

            context.Images.Add(imageEntity);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при сохранении в базу: {ex.Message}");
            }

            if (imageEntity.id == 0)
                return StatusCode(500, "Ошибка сохранения в базу");

            return Ok(new
            {
                Message = "Данные успешно сохранены в базе",
                ImageId = imageEntity.id
            });
        }

        [HttpGet("first/{count}")]
        public async Task<IActionResult> GetFirstNImages(int count, [FromServices] AppDbContext context)
        {
            var images = await context.Images
                .OrderBy(i => i.id)
                .Take(count)
                .Select(i => new
                {
                    i.id,
                    i.fileName,
                    FileDataBase64 = Convert.ToBase64String(i.fileData),
                    Metadata = new
                    {
                        i.title,
                        i.xPos,
                        i.yPos,
                        i.scaleFactor,
                        i.fontSize
                    }
                })
                .ToListAsync();

            return Ok(images);
        }
    }
}
