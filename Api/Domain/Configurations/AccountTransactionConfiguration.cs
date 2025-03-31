using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Domain.Configurations;

public class AccountTransactionConfiguration : IEntityTypeConfiguration<AccountTransaction>
{
    public void Configure(EntityTypeBuilder<AccountTransaction> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();

        builder.Property(x => x.InsertedDate).IsRequired(true);
        builder.Property(x => x.UpdatedDate).IsRequired(false);
        builder.Property(x => x.InsertedUser).IsRequired(true).HasMaxLength(250);
        builder.Property(x => x.UpdatedUser).IsRequired(false).HasMaxLength(250);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(x => x.AccountId).IsRequired(true);
        builder.Property(x => x.DebitAmount).IsRequired(false).HasPrecision(16, 4);
        builder.Property(x => x.CreditAmount).IsRequired(false).HasPrecision(16, 4);
        builder.Property(x => x.TransactionDate).IsRequired(true);
        builder.Property(x => x.TransferType).IsRequired(false).HasMaxLength(50);
    
        builder.HasIndex(x => x.AccountId).IsUnique(false);
    }
}