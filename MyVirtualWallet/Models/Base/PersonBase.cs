using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace MyVirtualWallet.Models
{
    public class PersonBase : SchemaObjectBase
    {
        public string IdentityCardNumber { get; set; }
        public int? Age { get; set; }
    }
}
