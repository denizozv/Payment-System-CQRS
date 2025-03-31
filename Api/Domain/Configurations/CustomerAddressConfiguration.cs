using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Domain.Configurations;


public class CustomerAddressConfiguration : IEntityTypeConfiguration<CustomerAddress>
{
    public void Configure(EntityTypeBuilder<CustomerAddress> builder)
    {
        builder.HasKey(ca => ca.Id);
        builder.Property(ca => ca.Id).UseIdentityColumn();

        builder.Property(x => x.InsertedDate).IsRequired(true);
        builder.Property(x => x.UpdatedDate).IsRequired(false);
        builder.Property(x => x.InsertedUser).IsRequired(true).HasMaxLength(250);
        builder.Property(x => x.UpdatedUser).IsRequired(false).HasMaxLength(250);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(x => x.CustomerId).IsRequired(true);
        builder.Property(ca => ca.CountryCode).IsRequired().HasMaxLength(3);
        builder.Property(ca => ca.City).IsRequired().HasMaxLength(100);
        builder.Property(ca => ca.District).IsRequired().HasMaxLength(100);
        builder.Property(ca => ca.Street).IsRequired().HasMaxLength(100);
        builder.Property(ca => ca.ZipCode).IsRequired().HasMaxLength(10);
        builder.Property(ca => ca.IsDefault).IsRequired();
    }
}