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
    public class UserEmailsController : ControllerBase
    {
        private readonly NeaRentContext _context;

        public UserEmailsController(NeaRentContext context)
        {
            _context = context;
        }

        // GET: api/UserEmails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserEmail>>> GetUserEmails()
        {
            return await _context.UserEmails.ToListAsync();
        }

        // GET: api/UserEmails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserEmail>> GetUserEmail(Guid id)
        {
            var userEmail = await _context.UserEmails.FindAsync(id);

            if (userEmail == null)
            {
                return NotFound();
            }

            return userEmail;
        }

        // PUT: api/UserEmails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserEmail(Guid id, UserEmail userEmail)
        {
            if (id != userEmail.Id)
            {
                return BadRequest();
            }

            _context.Entry(userEmail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserEmailExists(id))
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

        // POST: api/UserEmails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserEmail>> PostUserEmail(UserEmail userEmail)
        {
            _context.UserEmails.Add(userEmail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserEmail", new { id = userEmail.Id }, userEmail);
        }

        // DELETE: api/UserEmails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserEmail(Guid id)
        {
            var userEmail = await _context.UserEmails.FindAsync(id);
            if (userEmail == null)
            {
                return NotFound();
            }

            _context.UserEmails.Remove(userEmail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserEmailExists(Guid id)
        {
            return _context.UserEmails.Any(e => e.Id == id);
        }
    }
}
