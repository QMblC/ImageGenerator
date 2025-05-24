using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGenerator
{
    public class ImageUploadRequest
    {
        public IFormFile File { get; set; }
        public IFormFile MetadataJson { get; set; }
    }
}
