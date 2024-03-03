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
    public class SysUserTypesController : ControllerBase
    {
        private readonly NeaRentContext _context;

        public SysUserTypesController(NeaRentContext context)
        {
            _context = context;
        }

        // GET: api/SysUserTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SysUserType>>> GetSysUserTypes()
        {
            return await _context.SysUserTypes.ToListAsync();
        }

        // GET: api/SysUserTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SysUserType>> GetSysUserType(int id)
        {
            var sysUserType = await _context.SysUserTypes.FindAsync(id);

            if (sysUserType == null)
            {
                return NotFound();
            }

            return sysUserType;
        }

        // PUT: api/SysUserTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSysUserType(int id, SysUserType sysUserType)
        {
            if (id != sysUserType.Id)
            {
                return BadRequest();
            }

            _context.Entry(sysUserType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SysUserTypeExists(id))
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

        // POST: api/SysUserTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SysUserType>> PostSysUserType(SysUserType sysUserType)
        {
            _context.SysUserTypes.Add(sysUserType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SysUserTypeExists(sysUserType.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSysUserType", new { id = sysUserType.Id }, sysUserType);
        }

        // DELETE: api/SysUserTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSysUserType(int id)
        {
            var sysUserType = await _context.SysUserTypes.FindAsync(id);
            if (sysUserType == null)
            {
                return NotFound();
            }

            _context.SysUserTypes.Remove(sysUserType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SysUserTypeExists(int id)
        {
            return _context.SysUserTypes.Any(e => e.Id == id);
        }
    }
}
