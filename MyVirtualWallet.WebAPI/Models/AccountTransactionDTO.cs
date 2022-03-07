using MyVirtualWallet.WebAPI.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyVirtualWallet.WebAPI.Models
{
    public class AccountTransactionDTO : SchemaObjectBaseDTO
    {
        public decimal? CreditAmount { get; set; }
        public decimal? DebitAmount { get; set; }
        public Guid? SourceAccountID { get; set; }
        public Guid? DestinationAccountID { get; set; }
        public int? TransactionType { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public Guid? currentUserID { get; set; }
    }
}
