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
    public class RentalTypesController : ControllerBase
    {
        private readonly NeaRentContext _context;

        public RentalTypesController(NeaRentContext context)
        {
            _context = context;
        }

        // GET: api/RentalTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalType>>> GetRentalTypes()
        {
            return await _context.RentalTypes.ToListAsync();
        }

        // GET: api/RentalTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RentalType>> GetRentalType(int id)
        {
            var rentalType = await _context.RentalTypes.FindAsync(id);

            if (rentalType == null)
            {
                return NotFound();
            }

            return rentalType;
        }

        // PUT: api/RentalTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRentalType(int id, RentalType rentalType)
        {
            if (id != rentalType.Id)
            {
                return BadRequest();
            }

            _context.Entry(rentalType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalTypeExists(id))
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

        // POST: api/RentalTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RentalType>> PostRentalType(RentalType rentalType)
        {
            _context.RentalTypes.Add(rentalType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RentalTypeExists(rentalType.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRentalType", new { id = rentalType.Id }, rentalType);
        }

        // DELETE: api/RentalTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRentalType(int id)
        {
            var rentalType = await _context.RentalTypes.FindAsync(id);
            if (rentalType == null)
            {
                return NotFound();
            }

            _context.RentalTypes.Remove(rentalType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RentalTypeExists(int id)
        {
            return _context.RentalTypes.Any(e => e.Id == id);
        }
    }
}
