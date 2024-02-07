using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rental.DAL.DataContext;

namespace Rental.BLL.Services
{
    public static class MigrationsService
    {
        public static void ApplyMigration(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                serviceScope.ServiceProvider.GetService<AppDbContext>().Database.Migrate();
            }
        }
    }
}
