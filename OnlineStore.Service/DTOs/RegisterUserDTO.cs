using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.DTOs
{
    public class RegisterUserDTO
    {
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression(@"^(?!\s*$)(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
                 ErrorMessage = "Password must be at least 8 characters long, contain one uppercase letter, one lowercase letter, one number, one special character, and cannot be empty.")]
        [Required]
        [MinLength(1)]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

    }
}
