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
    public class UserCategoryPreferencesController : ControllerBase
    {
        private readonly NeaRentContext _context;

        public UserCategoryPreferencesController(NeaRentContext context)
        {
            _context = context;
        }

        // GET: api/UserCategoryPreferences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCategoryPreference>>> GetUserCategoryPreferences()
        {
            return await _context.UserCategoryPreferences.ToListAsync();
        }

        // GET: api/UserCategoryPreferences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<UserCategoryPreference>>> GetUserCategoryPreference(Guid id)
        {
            var userCategoryPreference = await _context.UserCategoryPreferences.Where(x => x.AzureObjectId == id).ToListAsync();

            if (userCategoryPreference == null)
            {
                return NotFound();
            }

            return userCategoryPreference;
        }

        // PUT: api/UserCategoryPreferences/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserCategoryPreference(Guid id, UserCategoryPreference userCategoryPreference)
        {
            if (id != userCategoryPreference.Id)
            {
                return BadRequest();
            }

            _context.Entry(userCategoryPreference).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserCategoryPreferenceExists(id))
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

        // POST: api/UserCategoryPreferences
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserCategoryPreference>> PostUserCategoryPreference(List<UserCategoryPreference> userCategoryPreference)
        {
            Guid UserID = userCategoryPreference[0].AzureObjectId;

            List<UserCategoryPreference> userCategories = await _context.UserCategoryPreferences.Where(x => x.AzureObjectId == UserID).ToListAsync();
            _context.UserCategoryPreferences.RemoveRange(userCategories);

            _context.UserCategoryPreferences.AddRangeAsync(userCategoryPreference);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/UserCategoryPreferences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserCategoryPreference(Guid id)
        {
            var userCategoryPreference = await _context.UserCategoryPreferences.FindAsync(id);
            if (userCategoryPreference == null)
            {
                return NotFound();
            }

            _context.UserCategoryPreferences.Remove(userCategoryPreference);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserCategoryPreferenceExists(Guid id)
        {
            return _context.UserCategoryPreferences.Any(e => e.Id == id);
        }
    }
}
