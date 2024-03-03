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
    public class CountryShippingMapsController : ControllerBase
    {
        private readonly NeaRentContext _context;

        public CountryShippingMapsController(NeaRentContext context)
        {
            _context = context;
        }

        // GET: api/CountryShippingMaps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryShippingMap>>> GetCountryShippingMaps()
        {
            return await _context.CountryShippingMaps.ToListAsync();
        }

        // GET: api/CountryShippingMaps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryShippingMap>> GetCountryShippingMap(Guid id)
        {
            var countryShippingMap = await _context.CountryShippingMaps.FindAsync(id);

            if (countryShippingMap == null)
            {
                return NotFound();
            }

            return countryShippingMap;
        }

        // PUT: api/CountryShippingMaps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountryShippingMap(Guid id, CountryShippingMap countryShippingMap)
        {
            if (id != countryShippingMap.CountryId)
            {
                return BadRequest();
            }

            _context.Entry(countryShippingMap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryShippingMapExists(id))
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

        // POST: api/CountryShippingMaps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CountryShippingMap>> PostCountryShippingMap(CountryShippingMap countryShippingMap)
        {
            _context.CountryShippingMaps.Add(countryShippingMap);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CountryShippingMapExists(countryShippingMap.CountryId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCountryShippingMap", new { id = countryShippingMap.CountryId }, countryShippingMap);
        }

        // DELETE: api/CountryShippingMaps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountryShippingMap(Guid id)
        {
            var countryShippingMap = await _context.CountryShippingMaps.FindAsync(id);
            if (countryShippingMap == null)
            {
                return NotFound();
            }

            _context.CountryShippingMaps.Remove(countryShippingMap);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryShippingMapExists(Guid id)
        {
            return _context.CountryShippingMaps.Any(e => e.CountryId == id);
        }
    }
}
