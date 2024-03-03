using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManager.Models;

namespace UserManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressCountriesController : ControllerBase
    {
        private readonly NeaRentContext _context;

        public AddressCountriesController(NeaRentContext context)
        {
            _context = context;
        }

        // GET: api/AddressCountries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressCountry>>> GetAddressCountries()
        {
            return await _context.AddressCountries.ToListAsync();
        }

        // GET: api/AddressCountries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressCountry>> GetAddressCountry(int id)
        {
            var addressCountry = await _context.AddressCountries.FindAsync(id);

            if (addressCountry == null)
            {
                return NotFound();
            }

            return addressCountry;
        }

        // PUT: api/AddressCountries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddressCountry(int id, AddressCountry addressCountry)
        {
            if (id != addressCountry.Id)
            {
                return BadRequest();
            }

            _context.Entry(addressCountry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressCountryExists(id))
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

        // POST: api/AddressCountries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AddressCountry>> PostAddressCountry(AddressCountry addressCountry)
        {
            _context.AddressCountries.Add(addressCountry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddressCountry", new { id = addressCountry.Id }, addressCountry);
        }

        // DELETE: api/AddressCountries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddressCountry(int id)
        {
            var addressCountry = await _context.AddressCountries.FindAsync(id);
            if (addressCountry == null)
            {
                return NotFound();
            }

            _context.AddressCountries.Remove(addressCountry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressCountryExists(int id)
        {
            return _context.AddressCountries.Any(e => e.Id == id);
        }
    }
}
