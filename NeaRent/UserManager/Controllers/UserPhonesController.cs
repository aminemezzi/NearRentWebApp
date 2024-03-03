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
    public class UserPhonesController : ControllerBase
    {
        private readonly NeaRentContext _context;

        public UserPhonesController(NeaRentContext context)
        {
            _context = context;
        }

        // GET: api/UserPhones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPhone>>> GetUserPhones()
        {
            return await _context.UserPhones.ToListAsync();
        }

        // GET: api/UserPhones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserPhone>> GetUserPhone(Guid id)
        {
            var userPhone = await _context.UserPhones.FindAsync(id);

            if (userPhone == null)
            {
                return NotFound();
            }

            return userPhone;
        }

        // PUT: api/UserPhones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserPhone(Guid id, UserPhone userPhone)
        {
            if (id != userPhone.Id)
            {
                return BadRequest();
            }

            _context.Entry(userPhone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPhoneExists(id))
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

        // POST: api/UserPhones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserPhone>> PostUserPhone(UserPhone userPhone)
        {
            _context.UserPhones.Add(userPhone);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserPhone", new { id = userPhone.Id }, userPhone);
        }

        // DELETE: api/UserPhones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserPhone(Guid id)
        {
            var userPhone = await _context.UserPhones.FindAsync(id);
            if (userPhone == null)
            {
                return NotFound();
            }

            _context.UserPhones.Remove(userPhone);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserPhoneExists(Guid id)
        {
            return _context.UserPhones.Any(e => e.Id == id);
        }
    }
}
