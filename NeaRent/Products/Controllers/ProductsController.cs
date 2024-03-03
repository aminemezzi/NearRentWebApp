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
    public class ProductsController : ControllerBase
    {
        private readonly NeaRentContext _context;

        public ProductsController(NeaRentContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products
                .Include(i => i.ProductCategoryMaps)
                .ThenInclude(i => i.Category)
                .Include(i => i.ProductRatings)
                .ToListAsync();
        }

        // GET: api/Products/GetProductIDsForCategory
        [HttpGet("GetProductIDsForCategory/{categoryId}")]
        public async Task<List<Guid>> GetProductIDsForCategory(Guid categoryId)
        {
            List<Guid> categories = await _context.Categories.Where(x => x.Id == categoryId || x.ParentId == categoryId && x.Active).Select(s => s.Id).ToListAsync();

            return await _context.ProductCategoryMaps.Where(x => categories.Contains(x.CategoryId) && x.Active).Select(s => s.ProductId).ToListAsync();
        }

        // GET: api/Products/GetProductIDsForLocation
        [HttpGet("GetProductIDsForLocation/{locationId}")]
        public async Task<List<Guid>> GetProductIDsForLocation(Guid locationId)
        {
            // Get the main location
            Location location = await _context.Locations.Where(x => x.Id == locationId && x.Active).FirstOrDefaultAsync();
            List<Guid> locationIDs = new List<Guid>();

            if (location != null)
            {
                switch (location.LocationType)
                {
                    case 1: // Country
                        List<Guid> stateIDs = await _context.Locations
                            .Where(x => x.ParentId == location.Id)
                            .Select(s => s.Id)
                            .ToListAsync();

                        locationIDs = await _context.Locations
                            .Where(x => stateIDs.Contains((Guid) x.ParentId))
                            .Select(s => s.Id)
                            .ToListAsync();

                        break;
                    case 2: // State
                        locationIDs = await _context.Locations
                            .Where(x => x.ParentId == location.Id)
                            .Select(s => s.Id)
                            .ToListAsync();

                        break;
                    case 3: // City
                        locationIDs = new List<Guid>{ locationId }; 
                        break;
                    default:
                        break;
                }

                if (locationIDs != null && locationIDs.Count > 0)
                {
                    return await _context.ProductLocationMaps
                        .Where(x => locationIDs.Contains(x.LocationId))
                        .Select(s => s.ProductId)
                        .ToListAsync ();
                }
                else
                {
                    return new List<Guid>();
                }
            }
            else
            {
                return new List<Guid>();
            }
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
