using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Base;

namespace Api.Domain;

[Table("CustomerPhone", Schema = "dbo")]
public class CustomerPhone : BaseEntity
{
    public long CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    public string CountryCode { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsDefault { get; set; }
}


