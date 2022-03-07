using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace MyVirtualWallet.Models
{
    public class AccountDetails : SchemaObjectBase
    {
        public AccountDetails(Guid? userID)
        {
            if (userID.HasValue)
                this.UserID = userID;
        }

        [Column(TypeName = "decimal(16,4)")]
        public decimal? Balance { get; set; }

        public Guid? UserID { get; set; }
        [ForeignKey(nameof(UserID))]
        public ApplicationUser User { get; set; }
    }
}
