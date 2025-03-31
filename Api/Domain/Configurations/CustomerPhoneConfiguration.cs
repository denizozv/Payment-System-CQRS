using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Domain.Configurations;

public class CustomerPhoneConfiguration : IEntityTypeConfiguration<CustomerPhone>
{
    public void Configure(EntityTypeBuilder<CustomerPhone> builder)
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
        builder.Property(ca => ca.PhoneNumber).IsRequired().HasMaxLength(12);
        builder.Property(ca => ca.IsDefault).IsRequired();
    }
}