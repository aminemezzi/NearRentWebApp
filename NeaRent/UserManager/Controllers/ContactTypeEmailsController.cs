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
    public class ContactTypeEmailsController : ControllerBase
    {
        private readonly NeaRentContext _context;

        public ContactTypeEmailsController(NeaRentContext context)
        {
            _context = context;
        }

        // GET: api/ContactTypeEmails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactTypeEmail>>> GetContactTypeEmails()
        {
            return await _context.ContactTypeEmails.ToListAsync();
        }

        // GET: api/ContactTypeEmails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactTypeEmail>> GetContactTypeEmail(int id)
        {
            var contactTypeEmail = await _context.ContactTypeEmails.FindAsync(id);

            if (contactTypeEmail == null)
            {
                return NotFound();
            }

            return contactTypeEmail;
        }

        // PUT: api/ContactTypeEmails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactTypeEmail(int id, ContactTypeEmail contactTypeEmail)
        {
            if (id != contactTypeEmail.Id)
            {
                return BadRequest();
            }

            _context.Entry(contactTypeEmail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactTypeEmailExists(id))
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

        // POST: api/ContactTypeEmails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContactTypeEmail>> PostContactTypeEmail(ContactTypeEmail contactTypeEmail)
        {
            _context.ContactTypeEmails.Add(contactTypeEmail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ContactTypeEmailExists(contactTypeEmail.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetContactTypeEmail", new { id = contactTypeEmail.Id }, contactTypeEmail);
        }

        // DELETE: api/ContactTypeEmails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactTypeEmail(int id)
        {
            var contactTypeEmail = await _context.ContactTypeEmails.FindAsync(id);
            if (contactTypeEmail == null)
            {
                return NotFound();
            }

            _context.ContactTypeEmails.Remove(contactTypeEmail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactTypeEmailExists(int id)
        {
            return _context.ContactTypeEmails.Any(e => e.Id == id);
        }
    }
}
