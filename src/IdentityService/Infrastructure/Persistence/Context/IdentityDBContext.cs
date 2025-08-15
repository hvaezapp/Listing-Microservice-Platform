using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IdentityService.Infrastructure.Persistence.Context;

public class IdentityDBContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
{
    private const string DefaultSchema = "identity";
    public const string DefaultConnectionStringName = "SvcDbContext";

    public DbSet<User> Listings => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(DefaultSchema);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}
