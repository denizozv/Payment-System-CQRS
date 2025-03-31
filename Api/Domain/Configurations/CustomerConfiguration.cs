using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Domain.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer", "dbo");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();

        builder.Property(x => x.InsertedDate).IsRequired();
        builder.Property(x => x.UpdatedDate).IsRequired(false);
        builder.Property(x => x.InsertedUser).IsRequired().HasMaxLength(250);
        builder.Property(x => x.UpdatedUser).IsRequired(false).HasMaxLength(250);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.MiddleName).IsRequired(false).HasMaxLength(100);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.IdentityNumber).IsRequired().HasMaxLength(11);
        builder.Property(x => x.CustomerNumber).IsRequired();
        builder.Property(x => x.OpenDate).IsRequired();

        builder.HasMany(x => x.CustomerAddresses)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Accounts)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.CustomerPhones)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.CustomerNumber).IsUnique();
    }
}
