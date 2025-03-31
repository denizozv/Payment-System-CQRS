using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Domain.Configurations;
public class MoneyTransferConfiguration : IEntityTypeConfiguration<MoneyTransfer>
{
    public void Configure(EntityTypeBuilder<MoneyTransfer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();

        builder.Property(x => x.InsertedDate).IsRequired(true);
        builder.Property(x => x.UpdatedDate).IsRequired(false);
        builder.Property(x => x.InsertedUser).IsRequired(true).HasMaxLength(250);
        builder.Property(x => x.UpdatedUser).IsRequired(false).HasMaxLength(250);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(x => x.FromAccountId).IsRequired(true);
        builder.Property(x => x.ToAccountId).IsRequired(true);
        builder.Property(x => x.Amount).IsRequired(true).HasPrecision(16, 4);
        builder.Property(x => x.FeeAmount).IsRequired(false).HasPrecision(16, 4);
        builder.Property(x => x.TransactionDate).IsRequired(true);
        builder.Property(x => x.Description).IsRequired(true).HasMaxLength(500);
        builder.Property(x => x.ReferenceNumber).IsRequired(true).HasMaxLength(50);
    }
}