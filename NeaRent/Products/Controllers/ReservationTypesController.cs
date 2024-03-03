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
    public class ReservationTypesController : ControllerBase
    {
        private readonly NeaRentContext _context;

        public ReservationTypesController(NeaRentContext context)
        {
            _context = context;
        }

        // GET: api/ReservationTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationType>>> GetReservationTypes()
        {
            return await _context.ReservationTypes.ToListAsync();
        }

        // GET: api/ReservationTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationType>> GetReservationType(int id)
        {
            var reservationType = await _context.ReservationTypes.FindAsync(id);

            if (reservationType == null)
            {
                return NotFound();
            }

            return reservationType;
        }

        // PUT: api/ReservationTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservationType(int id, ReservationType reservationType)
        {
            if (id != reservationType.Id)
            {
                return BadRequest();
            }

            _context.Entry(reservationType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationTypeExists(id))
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

        // POST: api/ReservationTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReservationType>> PostReservationType(ReservationType reservationType)
        {
            _context.ReservationTypes.Add(reservationType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ReservationTypeExists(reservationType.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetReservationType", new { id = reservationType.Id }, reservationType);
        }

        // DELETE: api/ReservationTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservationType(int id)
        {
            var reservationType = await _context.ReservationTypes.FindAsync(id);
            if (reservationType == null)
            {
                return NotFound();
            }

            _context.ReservationTypes.Remove(reservationType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservationTypeExists(int id)
        {
            return _context.ReservationTypes.Any(e => e.Id == id);
        }
    }
}
