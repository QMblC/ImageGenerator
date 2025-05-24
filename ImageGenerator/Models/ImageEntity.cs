using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGenerator.Models
{
    public class ImageEntity
    {
        [Key]
        public int Id { get; set; }

        public string FileName { get; set; } = "";
        public byte[] FileData { get; set; } = Array.Empty<byte>();

        public double NameXPos { get; set; }
        public double NameYPos { get; set; }
        public double ScaleFactor { get; set; }
        public double FontSize { get; set; }
    }
}
