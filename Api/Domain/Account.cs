using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Base;

namespace Api.Domain;

[Table("Account", Schema = "dbo")]
public class Account : BaseEntity
{
    public long CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    
    public string Name { get; set; }
    public int AccountNumber { get; set; }
    public string IBAN { get; set; }
    public decimal Balance { get; set; }
    public string CurrencyCode { get; set; }
    public DateTime OpenDate { get; set; }
    public DateTime? CloseDate { get; set; }

    public virtual List<AccountTransaction> AccountTransactions { get; set; }
}

