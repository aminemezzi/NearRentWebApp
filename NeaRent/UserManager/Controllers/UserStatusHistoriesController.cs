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
    public class UserStatusHistoriesController : ControllerBase
    {
        private readonly NeaRentContext _context;

        public UserStatusHistoriesController(NeaRentContext context)
        {
            _context = context;
        }

        // GET: api/UserStatusHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserStatusHistory>>> GetUserStatusHistories()
        {
            return await _context.UserStatusHistories.ToListAsync();
        }

        // GET: api/UserStatusHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserStatusHistory>> GetUserStatusHistory(Guid id)
        {
            var userStatusHistory = await _context.UserStatusHistories.FindAsync(id);

            if (userStatusHistory == null)
            {
                return NotFound();
            }

            return userStatusHistory;
        }

        // PUT: api/UserStatusHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserStatusHistory(Guid id, UserStatusHistory userStatusHistory)
        {
            if (id != userStatusHistory.Id)
            {
                return BadRequest();
            }

            _context.Entry(userStatusHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserStatusHistoryExists(id))
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

        // POST: api/UserStatusHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserStatusHistory>> PostUserStatusHistory(UserStatusHistory userStatusHistory)
        {
            _context.UserStatusHistories.Add(userStatusHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserStatusHistory", new { id = userStatusHistory.Id }, userStatusHistory);
        }

        // DELETE: api/UserStatusHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserStatusHistory(Guid id)
        {
            var userStatusHistory = await _context.UserStatusHistories.FindAsync(id);
            if (userStatusHistory == null)
            {
                return NotFound();
            }

            _context.UserStatusHistories.Remove(userStatusHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserStatusHistoryExists(Guid id)
        {
            return _context.UserStatusHistories.Any(e => e.Id == id);
        }
    }
}
