using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyVirtualWallet.WebAPI.Models.Base
{
    public class SchemaObjectBaseDTO
    {
        public Guid ObjectID { get; set; }
        public string ObjectName { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
