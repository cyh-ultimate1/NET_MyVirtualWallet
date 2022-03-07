using MyVirtualWallet.WebAPI.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyVirtualWallet.WebAPI.Models
{
    public class AccountDetailsDTO : SchemaObjectBaseDTO
    {
        public decimal? Balance { get; set; }
        public Guid? UserID { get; set; }
        [JsonIgnore]
        public ApplicationUserDTO User { get; set; }
    }
}
