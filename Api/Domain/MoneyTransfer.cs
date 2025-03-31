using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Base;

namespace Api.Domain;


[Table("MoneyTransfer", Schema = "dbo")]
public class MoneyTransfer : BaseEntity
{
    public long FromAccountId { get; set; }
    public long ToAccountId { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public decimal? FeeAmount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string ReferenceNumber { get; set; }
}

