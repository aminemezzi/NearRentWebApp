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
    public class ContactTypePhonesController : ControllerBase
    {
        private readonly NeaRentContext _context;

        public ContactTypePhonesController(NeaRentContext context)
        {
            _context = context;
        }

        // GET: api/ContactTypePhones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactTypePhone>>> GetContactTypePhones()
        {
            return await _context.ContactTypePhones.ToListAsync();
        }

        // GET: api/ContactTypePhones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactTypePhone>> GetContactTypePhone(int id)
        {
            var contactTypePhone = await _context.ContactTypePhones.FindAsync(id);

            if (contactTypePhone == null)
            {
                return NotFound();
            }

            return contactTypePhone;
        }

        // PUT: api/ContactTypePhones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactTypePhone(int id, ContactTypePhone contactTypePhone)
        {
            if (id != contactTypePhone.Id)
            {
                return BadRequest();
            }

            _context.Entry(contactTypePhone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactTypePhoneExists(id))
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

        // POST: api/ContactTypePhones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContactTypePhone>> PostContactTypePhone(ContactTypePhone contactTypePhone)
        {
            _context.ContactTypePhones.Add(contactTypePhone);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ContactTypePhoneExists(contactTypePhone.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetContactTypePhone", new { id = contactTypePhone.Id }, contactTypePhone);
        }

        // DELETE: api/ContactTypePhones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactTypePhone(int id)
        {
            var contactTypePhone = await _context.ContactTypePhones.FindAsync(id);
            if (contactTypePhone == null)
            {
                return NotFound();
            }

            _context.ContactTypePhones.Remove(contactTypePhone);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactTypePhoneExists(int id)
        {
            return _context.ContactTypePhones.Any(e => e.Id == id);
        }
    }
}
