using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyVirtualWallet.Models
{
    public class SchemaObjectBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ObjectID { get; set; }
        public string ObjectName { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
