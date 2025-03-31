using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Domain.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();

        builder.Property(x=> x.InsertedDate).IsRequired(true);
        builder.Property(x=> x.UpdatedDate).IsRequired(false);
        builder.Property(x=> x.InsertedUser).IsRequired(true).HasMaxLength(250);
        builder.Property(x=> x.UpdatedUser).IsRequired(false).HasMaxLength(250);
        builder.Property(x=> x.IsActive).IsRequired(true).HasDefaultValue(true);
        
        builder.Property(x=> x.CustomerId).IsRequired(true);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.AccountNumber).IsRequired();
        builder.Property(x => x.IBAN).IsRequired().HasMaxLength(26);
        builder.Property(x => x.Balance).IsRequired().HasPrecision(18,2);
        builder.Property(x => x.CurrencyCode).IsRequired().HasMaxLength(3);
        builder.Property(x => x.OpenDate).IsRequired();
        builder.Property(x => x.CloseDate).IsRequired(true);

        builder.HasMany(x => x.AccountTransactions)
            .WithOne(x => x.Account)
            .HasForeignKey(x => x.AccountId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x=> x.AccountNumber).IsUnique(true);
    }
}