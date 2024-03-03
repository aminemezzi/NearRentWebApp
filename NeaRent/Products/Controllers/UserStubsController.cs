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
    public class UserStubsController : ControllerBase
    {
        private readonly NeaRentContext _context;

        public UserStubsController(NeaRentContext context)
        {
            _context = context;
        }

        // GET: api/UserStubs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserStub>>> GetUserStubs()
        {
            return await _context.UserStubs.ToListAsync();
        }

        // GET: api/UserStubs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserStub>> GetUserStub(Guid id)
        {
            var userStub = await _context.UserStubs.FindAsync(id);

            if (userStub == null)
            {
                return NotFound();
            }

            return userStub;
        }

        // PUT: api/UserStubs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserStub(Guid id, UserStub userStub)
        {
            if (id != userStub.Id)
            {
                return BadRequest();
            }

            _context.Entry(userStub).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserStubExists(id))
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

        // POST: api/UserStubs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserStub>> PostUserStub(UserStub userStub)
        {
            _context.UserStubs.Add(userStub);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserStubExists(userStub.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserStub", new { id = userStub.Id }, userStub);
        }

        // POST: api/UserStubs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id:guid}")]
        public async Task<ActionResult<UserStub>> PostUserStub(Guid id)
        {
            UserStub stub = new UserStub();
            stub.Id = id;

            _context.UserStubs.Add(stub);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserStubExists(stub.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created();
        }

        // DELETE: api/UserStubs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserStub(Guid id)
        {
            var userStub = await _context.UserStubs.FindAsync(id);
            if (userStub == null)
            {
                return NotFound();
            }

            _context.UserStubs.Remove(userStub);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserStubExists(Guid id)
        {
            return _context.UserStubs.Any(e => e.Id == id);
        }
    }
}
