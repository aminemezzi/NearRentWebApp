using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.Models;

namespace Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryMapsController : ControllerBase
    {
        private readonly NeaRentContext _context;

        public ProductCategoryMapsController(NeaRentContext context)
        {
            _context = context;
        }

        // GET: api/ProductCategoryMaps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategoryMap>>> GetProductCategoryMaps()
        {
            return await _context.ProductCategoryMaps.ToListAsync();
        }

        // GET: api/ProductCategoryMaps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategoryMap>> GetProductCategoryMap(Guid id)
        {
            var productCategoryMap = await _context.ProductCategoryMaps.FindAsync(id);

            if (productCategoryMap == null)
            {
                return NotFound();
            }

            return productCategoryMap;
        }

        // PUT: api/ProductCategoryMaps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductCategoryMap(Guid id, ProductCategoryMap productCategoryMap)
        {
            if (id != productCategoryMap.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(productCategoryMap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCategoryMapExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductCategoryMaps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductCategoryMap>> PostProductCategoryMap(ProductCategoryMap productCategoryMap)
        {
            _context.ProductCategoryMaps.Add(productCategoryMap);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductCategoryMapExists(productCategoryMap.CategoryId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProductCategoryMap", new { id = productCategoryMap.CategoryId }, productCategoryMap);
        }

        // DELETE: api/ProductCategoryMaps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductCategoryMap(Guid id)
        {
            var productCategoryMap = await _context.ProductCategoryMaps.FindAsync(id);
            if (productCategoryMap == null)
            {
                return NotFound();
            }

            _context.ProductCategoryMaps.Remove(productCategoryMap);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductCategoryMapExists(Guid id)
        {
            return _context.ProductCategoryMaps.Any(e => e.CategoryId == id);
        }
    }
}
