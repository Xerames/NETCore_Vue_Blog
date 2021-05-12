using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class ImageUploadModel
    {
        public IFormFile Image { get; set; }
    }
}
