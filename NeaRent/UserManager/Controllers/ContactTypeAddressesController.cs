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
    public class ContactTypeAddressesController : ControllerBase
    {
        private readonly NeaRentContext _context;

        public ContactTypeAddressesController(NeaRentContext context)
        {
            _context = context;
        }

        // GET: api/ContactTypeAddresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactTypeAddress>>> GetContactTypeAddresses()
        {
            return await _context.ContactTypeAddresses.ToListAsync();
        }

        // GET: api/ContactTypeAddresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactTypeAddress>> GetContactTypeAddress(int id)
        {
            var contactTypeAddress = await _context.ContactTypeAddresses.FindAsync(id);

            if (contactTypeAddress == null)
            {
                return NotFound();
            }

            return contactTypeAddress;
        }

        // PUT: api/ContactTypeAddresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactTypeAddress(int id, ContactTypeAddress contactTypeAddress)
        {
            if (id != contactTypeAddress.Id)
            {
                return BadRequest();
            }

            _context.Entry(contactTypeAddress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactTypeAddressExists(id))
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

        // POST: api/ContactTypeAddresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContactTypeAddress>> PostContactTypeAddress(ContactTypeAddress contactTypeAddress)
        {
            _context.ContactTypeAddresses.Add(contactTypeAddress);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ContactTypeAddressExists(contactTypeAddress.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetContactTypeAddress", new { id = contactTypeAddress.Id }, contactTypeAddress);
        }

        // DELETE: api/ContactTypeAddresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactTypeAddress(int id)
        {
            var contactTypeAddress = await _context.ContactTypeAddresses.FindAsync(id);
            if (contactTypeAddress == null)
            {
                return NotFound();
            }

            _context.ContactTypeAddresses.Remove(contactTypeAddress);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactTypeAddressExists(int id)
        {
            return _context.ContactTypeAddresses.Any(e => e.Id == id);
        }
    }
}
