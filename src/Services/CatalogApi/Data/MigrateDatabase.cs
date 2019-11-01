using System;
using Microsoft.EntityFrameworkCore;

namespace CatalogApi.Data
{
    public static class MigrateDatabase
    {
        public static void EnsureCreated(CatalogContext context)
        {
            Console.WriteLine("Migration taking place....Creating database...");
            context.Database.Migrate();
            Console.WriteLine("Migrations  complete.....");
        }
    }
}
