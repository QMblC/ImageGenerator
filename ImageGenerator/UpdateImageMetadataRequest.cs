using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGenerator
{
    public class UpdateImageMetadataRequest
    {
        public string title { get; set; }
        public double xPos { get; set; }
        public double yPos { get; set; }
        public double scaleFactor { get; set; }
        public double fontSize { get; set; }
    }
}
