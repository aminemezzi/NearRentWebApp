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
    public class SysIdentityTypesController : ControllerBase
    {
        private readonly NeaRentContext _context;

        public SysIdentityTypesController(NeaRentContext context)
        {
            _context = context;
        }

        // GET: api/SysIdentityTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SysIdentityType>>> GetSysIdentityTypes()
        {
            return await _context.SysIdentityTypes.ToListAsync();
        }

        // GET: api/SysIdentityTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SysIdentityType>> GetSysIdentityType(int id)
        {
            var sysIdentityType = await _context.SysIdentityTypes.FindAsync(id);

            if (sysIdentityType == null)
            {
                return NotFound();
            }

            return sysIdentityType;
        }

        // PUT: api/SysIdentityTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSysIdentityType(int id, SysIdentityType sysIdentityType)
        {
            if (id != sysIdentityType.Id)
            {
                return BadRequest();
            }

            _context.Entry(sysIdentityType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SysIdentityTypeExists(id))
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

        // POST: api/SysIdentityTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SysIdentityType>> PostSysIdentityType(SysIdentityType sysIdentityType)
        {
            _context.SysIdentityTypes.Add(sysIdentityType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SysIdentityTypeExists(sysIdentityType.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSysIdentityType", new { id = sysIdentityType.Id }, sysIdentityType);
        }

        // DELETE: api/SysIdentityTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSysIdentityType(int id)
        {
            var sysIdentityType = await _context.SysIdentityTypes.FindAsync(id);
            if (sysIdentityType == null)
            {
                return NotFound();
            }

            _context.SysIdentityTypes.Remove(sysIdentityType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SysIdentityTypeExists(int id)
        {
            return _context.SysIdentityTypes.Any(e => e.Id == id);
        }
    }
}
