using System.ComponentModel.DataAnnotations.Schema;
using Api.Domain;
using Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[Table("Customer", Schema = "dbo")]
public class Customer : BaseEntity
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string IdentityNumber { get; set; }
    public int CustomerNumber { get; set; }
    public DateTime OpenDate { get; set; }
    public virtual List<CustomerAddress> CustomerAddresses { get; set; }
    public virtual List<CustomerPhone> CustomerPhones { get; set; }
    public virtual List<Account> Accounts { get; set; }
}

