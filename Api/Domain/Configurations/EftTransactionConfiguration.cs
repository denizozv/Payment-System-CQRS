using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Domain.Configurations;
public class EftTransactionConfiguration : IEntityTypeConfiguration<EftTransaction>
{
    public void Configure(EntityTypeBuilder<EftTransaction> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();

        builder.Property(x => x.InsertedDate).IsRequired(true);
        builder.Property(x => x.UpdatedDate).IsRequired(false);
        builder.Property(x => x.InsertedUser).IsRequired(true).HasMaxLength(250);
        builder.Property(x => x.UpdatedUser).IsRequired(false).HasMaxLength(250);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(x => x.FromAccountId).IsRequired(true);
        builder.Property(x => x.ReceiverName).IsRequired(true).HasMaxLength(500);
        builder.Property(x => x.ReveiverIban).IsRequired(true).HasMaxLength(26);
        builder.Property(x => x.Amount).IsRequired(true).HasPrecision(16, 4);
        builder.Property(x => x.FeeAmount).IsRequired(false).HasPrecision(16, 4);
        builder.Property(x => x.TransactionDate).IsRequired(true);
        builder.Property(x => x.PaymentCategoryCode).IsRequired(false).HasMaxLength(50);
        builder.Property(x => x.Description).IsRequired(true).HasMaxLength(500);
        builder.Property(x => x.ReferenceNumber).IsRequired(true).HasMaxLength(50);
    
        builder.HasIndex(x => x.FromAccountId).IsUnique(false);
    }
}