using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateAPI.Data
{
    public class FileUpload
    {
        public IFormFile file { get; set; }
        public string Image { get; set; }
    }
}
