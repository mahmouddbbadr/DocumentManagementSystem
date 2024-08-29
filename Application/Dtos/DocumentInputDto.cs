using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class DocumentInputDto
    {
        public IFormFile File { get; set; }
        public List<string> Tags { get; set; }
        public string DirectoryName { get; set; }

    }
}
