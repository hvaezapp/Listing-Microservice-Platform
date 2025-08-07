using ListingService.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ListingService.Domain.Entities;

public class Listing : BaseDomainEntity
{
    public const string TableName = "Listings";

    public Guid UserId { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;
    public string Title { get; private set; } = null!;
    public string Slug { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string ImageUrl { get; private set; } = null!;

}

public class ListingConfiguration : IEntityTypeConfiguration<Listing>
{
    public void Configure(EntityTypeBuilder<Listing> builder)
    {
        builder.ToTable(Listing.TableName);

        builder.HasKey(x => x.Id);

        builder.Property(a => a.Title)
               .HasMaxLength(200)
               .IsRequired();


        builder.Property(a => a.Description)
               .HasMaxLength(500)
               .IsRequired();


        builder.HasIndex(l => l.Slug)
               .IsUnique();


        builder.HasOne(o => o.Category)
               .WithMany(m => m.Listings)
               .HasForeignKey(fk => fk.CategoryId);
    }
}
