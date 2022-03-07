using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyVirtualWallet.WebAPI.Models
{
    public class UserRegisterDTO
    {
        [Required(ErrorMessage = "username is required.")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "password is required.")]
        public string Password { get; set; }
    }
}
