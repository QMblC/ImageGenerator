using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGenerator
{
    public class UpdateImageMetadataRequest
    {
        public string Title { get; set; }
        public double NameXPos { get; set; }
        public double NameYPos { get; set; }
        public double ScaleFactor { get; set; }
        public double FontSize { get; set; }
    }
}
