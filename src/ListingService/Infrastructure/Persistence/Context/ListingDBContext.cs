using ListingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ListingService.Infrastructure.Persistence.Context;

public class ListingDBContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
{
    private const string DefaultSchema = "listing";
    public const string DefaultConnectionStringName = "SvcDbContext";

    public DbSet<Listing> Listings => Set<Listing>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(DefaultSchema);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}
