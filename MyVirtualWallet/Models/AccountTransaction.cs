using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace MyVirtualWallet.Models
{
    public class AccountTransaction : SchemaObjectBase
    {
        [Column(TypeName = "decimal(16,4)")]
        public decimal? CreditAmount { get; set; }
        [Column(TypeName = "decimal(16,4)")]
        public decimal? DebitAmount { get; set; }
        public Guid? SourceAccountID { get; set; }
        public Guid? DestinationAccountID { get; set; }
        public int? TransactionType { get; set; }

        public DateTime? CreatedDateTime { get; set; }
    }

    public enum AccountTransactionTypeEnum
    {
        Deposit = 1
        , Transfer = 2
        , Withdraw = 3
    }
}
