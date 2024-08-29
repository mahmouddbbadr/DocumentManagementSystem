using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagementSystem.Services.Dtos
{
    public class DocumentOutputDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime UploadedAt { get; set; }
        public byte[] Content { get; set; }
        public string Owner { get; set; } 
        public string Path { get; set; }
        public long Size { get; set; }


    }
}
