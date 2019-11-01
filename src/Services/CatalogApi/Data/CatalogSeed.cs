using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace CatalogApi.Data
{
    public class CatalogSeed
    {
        public static async Task SeedAsync(CatalogContext context)
        {
            context.Database.Migrate();
            if (!context.catalogBrands.Any())
            {
                context.catalogBrands.AddRange(GetPreconfiguredCatalogBrands());
                await context.SaveChangesAsync();
            }
            if (!context.catalogTypes.Any())
            {
                context.catalogTypes.AddRange(GetPreconfiguredCatalogTypes());
                await context.SaveChangesAsync();
            }

            if (!context.catalogItems.Any())
            {
                context.catalogItems.AddRange(GetPreconfiguredItems());
                await context.SaveChangesAsync();
            }

        }

        static IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands()
        {
            return new List<CatalogBrand>()
            {
                new CatalogBrand() { brand = "Addidas"},
                new CatalogBrand() { brand = "Puma" },
                new CatalogBrand() { brand = "Slazenger" }
            };
        }

        static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
        {
            return new List<CatalogType>()
            {
                new CatalogType() { type = "Running"},
                new CatalogType() { type = "Basketball" },
                new CatalogType() { type = "Tennis" },

            };
        }
        static IEnumerable<CatalogItem> GetPreconfiguredItems()
        {
            string baseUrl = "localhost:5000";
            return new List<CatalogItem>()
            {
                new CatalogItem() { catalog_type_id=2,catalog_brand_id=3, description = "Shoes for next century", name = "World Star", price = 199.5M, picture_url = $"https://{baseUrl}/api/pic/1" },
                new CatalogItem() { catalog_type_id=1,catalog_brand_id=2, description = "will make you world champions", name = "White Line", price= 88.50M, picture_url = $"https://{baseUrl}/api/pic/2" },
                new CatalogItem() { catalog_type_id=2,catalog_brand_id=3, description = "You have already won gold medal", name = "Prism White Shoes", price = 129, picture_url = $"https://{baseUrl}/api/pic/3" },
                new CatalogItem() { catalog_type_id=2,catalog_brand_id=2, description = "Olympic runner", name = "Foundation Hitech", price = 12, picture_url = $"https://{baseUrl}/api/pic/4" },
                new CatalogItem() { catalog_type_id=2,catalog_brand_id=1, description = "Roslyn Red Sheet", name = "Roslyn White", price = 188.5M, picture_url = $"https://{baseUrl}/api/pic/5" },
                new CatalogItem() { catalog_type_id=2,catalog_brand_id=2, description = "Lala Land", name = "Blue Star", price = 112, picture_url = $"https://{baseUrl}/api/pic/6" },
                new CatalogItem() { catalog_type_id=2,catalog_brand_id=1, description = "High in the sky", name = "Roslyn Green", price = 212, picture_url = $"https://{baseUrl}/api/pic/7"  },
                new CatalogItem() { catalog_type_id=1,catalog_brand_id=1, description = "Light as carbon", name = "Deep Purple", price = 238.5M, picture_url = $"https://{baseUrl}/api/pic/8" },
                new CatalogItem() { catalog_type_id=1,catalog_brand_id=2, description = "High Jumper", name = "Addidas<White> Running", price = 129, picture_url = $"https://{baseUrl}/api/pic/9" },
                new CatalogItem() { catalog_type_id=2,catalog_brand_id=3, description = "Dunker", name = "Elequent", price = 12, picture_url = $"https://{baseUrl}/api/pic/10" },
                new CatalogItem() { catalog_type_id=1,catalog_brand_id=2, description = "All round", name = "Inredeible", price = 248.5M, picture_url = $"https://{baseUrl}/api/pic/11" },
                new CatalogItem() { catalog_type_id=2,catalog_brand_id=1, description = "Pricesless", name = "London Sky", price = 412, picture_url = $"https://{baseUrl}/api/pic/12" },
                new CatalogItem() { catalog_type_id=3,catalog_brand_id=3, description = "Tennis Star", name = "Elequent", price = 123, picture_url = $"https://{baseUrl}/api/pic/13" },
                new CatalogItem() { catalog_type_id=3,catalog_brand_id=2, description = "Wimbeldon", name = "London Star", price = 218.5M, picture_url = $"https://{baseUrl}/api/pic/14" },
                new CatalogItem() { catalog_type_id=3,catalog_brand_id=1, description = "Rolan Garros", name = "Paris Blues", price = 312, picture_url = $"https://{baseUrl}/api/pic/15" }

            };
        }

    }
}
