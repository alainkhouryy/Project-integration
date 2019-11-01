using System;
using CatalogApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogApi.Data
{
    public class CatalogContext:DbContext
    {
        public DbSet<CatalogType> catalogTypes { get; set; }
        public DbSet<CatalogBrand> catalogBrands { get; set; }
        public DbSet<CatalogItem> catalogItems { get; set; }

        public CatalogContext(DbContextOptions options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatalogBrand>(ConfigueCatalogBrand);
            modelBuilder.Entity<CatalogType>(ConfigueCatalogType);
            modelBuilder.Entity<CatalogItem>(ConfigueCatalogItem);
        }

        private void ConfigueCatalogItem(EntityTypeBuilder<CatalogItem> builder)
        {
            // hone 3am na3mol configuration lal catalogItem table wl properties taba3a
            builder.ToTable("Catalog"); // ya3ne el table lal catalogItem bikoun esma Catalog
            builder.Property(c => c.id)
                .ForSqlServerUseSequenceHiLo("catalog_hilo")
                .IsRequired();
            builder.Property(c => c.name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.price)
                .IsRequired();
            builder.Property(c => c.picture_url)
                .IsRequired(false);

            // relationships
            builder.HasOne(c => c.catalog_brand)
                .WithMany()// catalog brand has many catalog items but a catalog item has only one brand
                .HasForeignKey(c => c.catalog_brand_id);
            builder.HasOne(c => c.catalog_type)
                .WithMany()
                .HasForeignKey(c => c.catalog_type_id);

        }

        private void ConfigueCatalogType(EntityTypeBuilder<CatalogType> builder)
        {
            builder.ToTable("CatalogType");
            builder.Property(c => c.id)
                .ForSqlServerUseSequenceHiLo("catalog_type_hilo")
                .IsRequired();
            builder.Property(c => c.type)
                .IsRequired()
                .HasMaxLength(100);
        }

        private void ConfigueCatalogBrand(EntityTypeBuilder<CatalogBrand> builder)
        {
            builder.ToTable("CatalogBrand");
            builder.Property(c => c.id)
                .ForSqlServerUseSequenceHiLo("catalog_brand_hilo")
                .IsRequired();
            builder.Property(c => c.brand)
                .IsRequired()
                .HasMaxLength(100);
        }

        
    }
}
