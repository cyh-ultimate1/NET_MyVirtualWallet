using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyVirtualWallet.WebAPI.Models
{
    public class ApplicationUserDTO
    {
        public Guid? Id { get; set; }
        public string UserName { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
        public string IdentityCardNumber { get; set; }
        public AccountDetailsDTO AccountDetails { get; set; }
    }
}
