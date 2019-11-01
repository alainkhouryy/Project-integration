using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogApi.Data;
using CatalogApi.Domain;
using CatalogApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatalogApi.Controllers
{
    [Route("api/[controller]")]
    public class CatalogController : Controller
    {
        private readonly CatalogContext _catalogContext;
        private readonly IOptionsSnapshot<CatalogSettings> _settings;

        public CatalogController(CatalogContext catalogContext, IOptionsSnapshot<CatalogSettings> settings)
        {
            _catalogContext = catalogContext;
            _settings = settings;
            ((DbContext)catalogContext).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; // hek menbatel na3mol track lal changes bel database
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> catalogTypes()
        {
            List<CatalogType> items = await _catalogContext.catalogTypes.ToListAsync();
            return Ok(items);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> catalogBrands()
        {
            List<CatalogBrand> items = await _catalogContext.catalogBrands.ToListAsync();
            return Ok(items);
        }

        [HttpGet("items/{id}")]
        public async Task<IActionResult> getItemById(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }

            CatalogItem item = await _catalogContext.catalogItems.SingleOrDefaultAsync(c => c.id == id);
            if (item != null)
            {
                //item.picture_url = item.picture_url;// we can also leave the externalcatalogbaseurltoberepalced in the database and replace it here with _settings.Value.ExternalCatalogBaseUrl
                return Ok(item);
            }

            return NotFound();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> items([FromQuery] int pageSize = 6, [FromQuery] int pageIndex = 0)
        {
            long totalItems = await _catalogContext.catalogItems.LongCountAsync();
            List<CatalogItem> itemsOnPage = await _catalogContext.catalogItems
                .OrderBy(c => c.name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
            PaginatedItemsViewModel<CatalogItem> model = new PaginatedItemsViewModel<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage);
            return Ok(model);
        }

        [HttpGet("[action]/withname/{name:minlength(1)}")]
        public async Task<IActionResult> items(string name, [FromQuery] int pageSize = 6, [FromQuery] int pageIndex = 0)
        {
            long totalItems = await _catalogContext.catalogItems.Where(c => c.name.StartsWith(name)).LongCountAsync();
            List<CatalogItem> itemsOnPage = await _catalogContext.catalogItems
                .Where(c => c.name.StartsWith(name))
                .OrderBy(c => c.name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
            PaginatedItemsViewModel<CatalogItem> model = new PaginatedItemsViewModel<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage);
            return Ok(model);
        }

        [HttpGet("[action]/type/{catalogTypeId}/brand/{catalogBrandId}")]
        public async Task<IActionResult> items(int? catalogTypeId, int? catalogBrandId, [FromQuery] int pageSize = 6, [FromQuery] int pageIndex = 0)
        {
            var root = (IQueryable<CatalogItem>) _catalogContext.catalogItems; // hayde ya3ne metel eno ma teb3at hala2 el query 3al database, bas men7ot el query bi variable w yemken nzid osas 3laya

            if (catalogTypeId.HasValue)
            {
                root = root.Where(c => c.catalog_type_id == catalogTypeId);
            }

            if (catalogBrandId.HasValue)
            {
                root = root.Where(c => c.catalog_brand_id == catalogBrandId);
            }

            
            long totalItems = await root.LongCountAsync();
            List<CatalogItem> itemsOnPage = await root
                .OrderBy(c => c.name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
            PaginatedItemsViewModel<CatalogItem> model = new PaginatedItemsViewModel<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage);
            return Ok(model);
        }

        [HttpPost("items")]
        public async Task<IActionResult> createProduct([FromBody] CatalogItem product)
        {
            var item = new CatalogItem
            {
                catalog_type_id = product.catalog_type_id,
                catalog_brand_id = product.catalog_brand_id,
                price = product.price,
                description = product.description,
                name = product.name,
                picture_url = product.picture_url
            };

            _catalogContext.catalogItems.Add(item);
            await _catalogContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("items")]
        public async Task<IActionResult> updateProduct([FromBody] CatalogItem productToUpdate)
        {
            var catalogItem = await _catalogContext.catalogItems.SingleOrDefaultAsync(i => i.id == productToUpdate.id);
            if (catalogItem == null) { return NotFound(new { message = $"Item withid {productToUpdate.id} not found."}); }
            catalogItem = productToUpdate;
            _catalogContext.catalogItems.Update(catalogItem);
            await _catalogContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteProduct(int id)
        {
            var product = await _catalogContext.catalogItems.SingleOrDefaultAsync(p => p.id == id);
            if (product == null) { return NotFound(); }

            _catalogContext.catalogItems.Remove(product);
            await _catalogContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
