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
    public class UserRegistrationsController : ControllerBase
    {
        private readonly NeaRentContext _context;

        public UserRegistrationsController(NeaRentContext context)
        {
            _context = context;
        }

        // GET: api/UserRegistrations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRegistration>>> GetUserRegistrations()
        {
            return await _context.UserRegistrations.ToListAsync();
        }

        // GET: api/UserRegistrations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRegistration>> GetUserRegistration(Guid id)
        {
            var userRegistration = await _context.UserRegistrations.FindAsync(id);

            if (userRegistration == null)
            {
                return NotFound();
            }

            return userRegistration;
        }

        // GET: api/GetUserRegistrationForUser/5
        [HttpGet("GetUserRegistrationForUser/{userId}")]
        public async Task<ActionResult<UserRegistration>> GetUserRegistrationForUser(Guid userId)
        {
            var userRegistration = await _context.UserRegistrations.Where(x => x.AzureObjectId == userId).FirstOrDefaultAsync();

            if (userRegistration == null)
            {
                return NotFound();
            }

            return userRegistration;
        }

        // PUT: api/UserRegistrations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserRegistration(Guid id, UserRegistration userRegistration)
        {
            if (id != userRegistration.Id)
            {
                return BadRequest();
            }

            _context.Entry(userRegistration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRegistrationExists(id))
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

        // POST: api/UserRegistrations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserRegistration>> PostUserRegistration(UserRegistration userRegistration)
        {
            List<UserRegistration> toDelete = _context.UserRegistrations.Where(x => x.AzureObjectId == userRegistration.AzureObjectId).ToList();

            if(toDelete != null & toDelete.Count > 0)
            {
                _context.RemoveRange(toDelete);
            }

            _context.UserRegistrations.Add(userRegistration);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserRegistration", new { id = userRegistration.Id }, userRegistration);
        }

        // DELETE: api/UserRegistrations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserRegistration(Guid id)
        {
            var userRegistration = await _context.UserRegistrations.FindAsync(id);
            if (userRegistration == null)
            {
                return NotFound();
            }

            _context.UserRegistrations.Remove(userRegistration);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserRegistrationExists(Guid id)
        {
            return _context.UserRegistrations.Any(e => e.Id == id);
        }
    }
}
