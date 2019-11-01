using System;
using Microsoft.EntityFrameworkCore;

namespace Ordering.Api.Data
{
    public static class MigrateDatabase
    {
        public static void EnsureCreated(OrdersContext context)
        {
            System.Console.WriteLine("Migration taking place....Creating database...");
            context.Database.Migrate();
            System.Console.WriteLine("Migrations  complete.....");
        }
    }
}
