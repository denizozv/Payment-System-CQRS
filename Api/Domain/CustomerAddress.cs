using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Base;

namespace Api.Domain;

[Table("CustomerAddress", Schema = "dbo")]
public class CustomerAddress : BaseEntity
{
    public long CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    public string? CountryCode { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
    public string? Street { get; set; }
    public string? ZipCode { get; set; }
    public bool IsDefault { get; set; }
}

