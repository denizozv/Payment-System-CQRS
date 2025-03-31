using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Base;

namespace Api.Domain;


[Table("AccountTransaction", Schema = "dbo")]
public class AccountTransaction : BaseEntity
{
    public long AccountId { get; set; }
    public virtual Account Account { get; set; }

    public string Description { get; set; }
    public decimal? DebitAmount { get; set; } 
    public decimal? CreditAmount { get; set; } 
    public DateTime TransactionDate { get; set; }
    public string ReferenceNumber { get; set; }
    public string? TransferType { get; set; }
}

