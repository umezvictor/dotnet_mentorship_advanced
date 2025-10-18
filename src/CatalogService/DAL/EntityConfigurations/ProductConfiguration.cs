using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EntityConfigurations;
internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        builder.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(p => p.Amount).IsRequired();
        builder.ToTable(t => t.HasCheckConstraint("CK_Amount_Positive", "[Amount] > 0"));

        builder.HasOne(x => x.Category).WithMany(u => u.Products).HasForeignKey(x => x.CategoryId).IsRequired();
    }
}
