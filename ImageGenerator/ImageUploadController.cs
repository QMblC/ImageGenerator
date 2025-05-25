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
    using System.Text.Json;

    [ApiController]
    [Route("api/[controller]")]
    public class ImageUploadController : ControllerBase
    {
        public class ImageJson
        {
            public string Title { get; set; }
            public double NameXPos { get; set; }
            public double NameYPos { get; set; }
            public double ScaleFactor { get; set; }
            public double FontSize { get; set; }
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
                Title = imageEntity.Title,
                imageEntity.NameXPos,
                imageEntity.NameYPos,
                imageEntity.ScaleFactor,
                imageEntity.FontSize
            };

            return Ok(new
            {
                FileName = imageEntity.FileName,
                FileDataBase64 = Convert.ToBase64String(imageEntity.FileData),
                Metadata = metadata
            });
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
                FileName = request.File.FileName,
                FileData = fileBytes,
                Title = metadata.Title,
                NameXPos = metadata.NameXPos,
                NameYPos = metadata.NameYPos,
                ScaleFactor = metadata.ScaleFactor,
                FontSize = metadata.FontSize
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

            if (imageEntity.Id == 0)
                return StatusCode(500, "Ошибка сохранения в базу");

            return Ok(new
            {
                Message = "Данные успешно сохранены в базе",
                ImageId = imageEntity.Id
            });
        }
    }

}
