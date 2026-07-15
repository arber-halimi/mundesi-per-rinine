using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouthOpportunities.Domain.Categories;

namespace YouthOpportunities.Infrastructure.Persistence.Configurations
{
    public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(category => category.Id);

            builder.Property(category => category.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(category => category.Slug)
                .IsRequired()
                .HasMaxLength(180);

            builder.HasIndex(category => category.Slug)
                .IsUnique();

            builder.Property(category => category.Description)
                .HasMaxLength(1000);

            builder.Property(category => category.IsActive)
                .HasDefaultValue(true);

            builder.Property(category => category.CreatedAtUtc)
                .IsRequired();

            builder.Property(category => category.UpdatedAtUtc);
        }
    }
}
