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
        public int id { get; set; }

        public string title { get; set; }

        public string fileName { get; set; } = "";
        public byte[] fileData { get; set; } = Array.Empty<byte>();

        public double xPos { get; set; }
        public double yPos { get; set; }
        public double scaleFactor { get; set; }
        public double fontSize { get; set; }
    }
}
