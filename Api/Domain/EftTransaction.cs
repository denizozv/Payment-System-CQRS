using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Base;

namespace Api.Domain;


[Table("EftTransaction", Schema = "dbo")]
public class EftTransaction : BaseEntity
{
    public long FromAccountId { get; set; }
    public string ReveiverIban { get; set; }
    public string ReceiverName { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public decimal? FeeAmount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string ReferenceNumber { get; set; }
    public string? PaymentCategoryCode { get; set; }
}

