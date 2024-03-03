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
    public class ProductReservationsController : ControllerBase
    {
        private readonly NeaRentContext _context;

        public ProductReservationsController(NeaRentContext context)
        {
            _context = context;
        }

        // GET: api/ProductReservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductReservation>>> GetProductReservations()
        {
            return await _context.ProductReservations.ToListAsync();
        }

        // GET: api/ProductReservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReservation>> GetProductReservation(Guid id)
        {
            var productReservation = await _context.ProductReservations.FindAsync(id);

            if (productReservation == null)
            {
                return NotFound();
            }

            return productReservation;
        }

        // PUT: api/ProductReservations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductReservation(Guid id, ProductReservation productReservation)
        {
            if (id != productReservation.Id)
            {
                return BadRequest();
            }

            _context.Entry(productReservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductReservationExists(id))
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

        // POST: api/ProductReservations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductReservation>> PostProductReservation(ProductReservation productReservation)
        {
            _context.ProductReservations.Add(productReservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductReservation", new { id = productReservation.Id }, productReservation);
        }

        // DELETE: api/ProductReservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductReservation(Guid id)
        {
            var productReservation = await _context.ProductReservations.FindAsync(id);
            if (productReservation == null)
            {
                return NotFound();
            }

            _context.ProductReservations.Remove(productReservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductReservationExists(Guid id)
        {
            return _context.ProductReservations.Any(e => e.Id == id);
        }
    }
}
