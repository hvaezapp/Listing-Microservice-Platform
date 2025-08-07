using ListingService.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ListingService.Bootstraper;

public static class DependencyRegistration
{
   
    public static void RegisterMSSql(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ListingDBContext>(configure =>
        {
            configure.UseSqlServer(builder.Configuration.GetConnectionString(ListingDBContext.DefaultConnectionStringName));
        });
    }

}
