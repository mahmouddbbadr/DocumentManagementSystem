using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class UserRegisterDto
    {
        [Required]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        [Required]

        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [RegularExpression(@"^[2-3][0-9]{2}(0[1-9]|1[0-2])(0[1-9]|[12][0-9]|3[01])[0-9]{7}$",
        ErrorMessage = "Please enter a valid Egyptian national ID.")]
        public string NId { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^01[0-25]\d{8}$", ErrorMessage = "please enter a valid phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        public string WorkSpaceName { get; set; }
    }
}
