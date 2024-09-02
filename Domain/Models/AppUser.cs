using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AppUser : IdentityUser
    {
            
        public string NId { get; set; }
        public string Address { get; set; }
        public bool IsLocked { get; set; } = false;
        public DocumentAccessControl AccessControl { get; set; } = DocumentAccessControl.RootUser;

        [ForeignKey("WorkSpace")]
        public Guid WorkspaceId { get; set; }
        public WorkSpace WorkSpace { get; set; }
        public virtual ICollection<Directory> Directories { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}
