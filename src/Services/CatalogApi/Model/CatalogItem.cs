using System;
namespace CatalogApi.Domain
{
    public class CatalogItem
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public string picture_url { get; set; }
        public int catalog_type_id { get; set; }
        public int catalog_brand_id { get; set; }
        public CatalogBrand catalog_brand { get; set; }
        public CatalogType catalog_type { get; set; }
    }
}
