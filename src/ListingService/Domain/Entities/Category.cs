using ListingService.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ListingService.Domain.Entities;

public class Category : BaseDomainEntity
{
    public const string TableName = "Categories";

    public string Title { get; private set; } = null!;
    public string Slug { get; private set; } = null!;
    public ICollection<Listing> Listings { get; private set; } = [];
}


public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(Category.TableName);

        builder.HasKey(x => x.Id);

        builder.Property(a => a.Title)
               .HasMaxLength(50)
               .IsRequired();

        builder.HasIndex(l => l.Slug)
               .IsUnique();
    }
}