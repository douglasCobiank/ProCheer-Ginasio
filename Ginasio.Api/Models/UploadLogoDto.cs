using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ginasio.Api.Models
{
    public class UploadLogoDto
    {
        public IFormFile? Arquivo { get; set; }
        public int Id { get; set; }
    }
}