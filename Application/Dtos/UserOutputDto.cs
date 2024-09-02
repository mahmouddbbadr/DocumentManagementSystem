using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagementSystem.Services.Dtos
{
    public class UserOutputDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string WorkSpaceName { get; set; }
    }
}
