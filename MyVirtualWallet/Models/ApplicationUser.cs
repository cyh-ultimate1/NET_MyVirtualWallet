using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyVirtualWallet.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
        public string IdentityCardNumber { get; set; }
        public AccountDetails AccountDetails { get; set; }
    }
}
