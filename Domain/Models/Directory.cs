using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Models
{
    public class Directory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsPrivate { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public ICollection<Document> Documents { get; set; }

        [ForeignKey("WorkSpace")]
        public Guid WorkSpaceId { get; set; }
        public virtual WorkSpace WorkSpace { get; set; }



        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
