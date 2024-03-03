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
    public class ProductShippingMapsController : ControllerBase
    {
        private readonly NeaRentContext _context;

        public ProductShippingMapsController(NeaRentContext context)
        {
            _context = context;
        }

        // GET: api/ProductShippingMaps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductShippingMap>>> GetProductShippingMaps()
        {
            return await _context.ProductShippingMaps.ToListAsync();
        }

        // GET: api/ProductShippingMaps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductShippingMap>> GetProductShippingMap(Guid id)
        {
            var productShippingMap = await _context.ProductShippingMaps.FindAsync(id);

            if (productShippingMap == null)
            {
                return NotFound();
            }

            return productShippingMap;
        }

        // PUT: api/ProductShippingMaps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductShippingMap(Guid id, ProductShippingMap productShippingMap)
        {
            if (id != productShippingMap.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(productShippingMap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductShippingMapExists(id))
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

        // POST: api/ProductShippingMaps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductShippingMap>> PostProductShippingMap(ProductShippingMap productShippingMap)
        {
            _context.ProductShippingMaps.Add(productShippingMap);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductShippingMapExists(productShippingMap.ProductId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProductShippingMap", new { id = productShippingMap.ProductId }, productShippingMap);
        }

        // DELETE: api/ProductShippingMaps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductShippingMap(Guid id)
        {
            var productShippingMap = await _context.ProductShippingMaps.FindAsync(id);
            if (productShippingMap == null)
            {
                return NotFound();
            }

            _context.ProductShippingMaps.Remove(productShippingMap);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductShippingMapExists(Guid id)
        {
            return _context.ProductShippingMaps.Any(e => e.ProductId == id);
        }
    }
}
