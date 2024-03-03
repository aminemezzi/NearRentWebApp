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
    public class ProductRentalMapsController : ControllerBase
    {
        private readonly NeaRentContext _context;

        public ProductRentalMapsController(NeaRentContext context)
        {
            _context = context;
        }

        // GET: api/ProductRentalMaps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductRentalMap>>> GetProductRentalMaps()
        {
            return await _context.ProductRentalMaps.ToListAsync();
        }

        // GET: api/ProductRentalMaps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductRentalMap>> GetProductRentalMap(Guid id)
        {
            var productRentalMap = await _context.ProductRentalMaps.FindAsync(id);

            if (productRentalMap == null)
            {
                return NotFound();
            }

            return productRentalMap;
        }

        // PUT: api/ProductRentalMaps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductRentalMap(Guid id, ProductRentalMap productRentalMap)
        {
            if (id != productRentalMap.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(productRentalMap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductRentalMapExists(id))
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

        // POST: api/ProductRentalMaps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductRentalMap>> PostProductRentalMap(ProductRentalMap productRentalMap)
        {
            _context.ProductRentalMaps.Add(productRentalMap);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductRentalMapExists(productRentalMap.ProductId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProductRentalMap", new { id = productRentalMap.ProductId }, productRentalMap);
        }

        // DELETE: api/ProductRentalMaps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductRentalMap(Guid id)
        {
            var productRentalMap = await _context.ProductRentalMaps.FindAsync(id);
            if (productRentalMap == null)
            {
                return NotFound();
            }

            _context.ProductRentalMaps.Remove(productRentalMap);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductRentalMapExists(Guid id)
        {
            return _context.ProductRentalMaps.Any(e => e.ProductId == id);
        }
    }
}
