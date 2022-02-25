using Bogus;
using Lms.CORE.Entities;
using Lms.DATA.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Lms.API.Extensions;
public static class ApplicationBuilderExtensions
{

    public static async Task<IApplicationBuilder> SeedDataAsync(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var db = serviceProvider.GetRequiredService<LmsContext>();

            //db.Database.EnsureDeleted();
            //db.Database.Migrate();

            try
            {
                await SeedData.InitAsync(db);
            }
            catch (Exception e)
            {
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(string.Join(" ", e.Message));
                //throw;
            }
        }

        return app;
    }


}

