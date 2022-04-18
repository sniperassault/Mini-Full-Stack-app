using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Final.Data;
using Final.Models;

namespace Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WinnersEFController : ControllerBase
    {
        private readonly FinalContext _context;

        public WinnersEFController(FinalContext context)
        {
            _context = context;
        }

        // GET: api/WinnersEF
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Winners>>> GetWinners()
        {
            return await _context.Winners.Include(w => w.Driver).Include(w => w.Car).Include(w => w.Track).ToListAsync();
        }

        // GET: api/WinnersEF/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Winners>> GetWinners(int id)
        {
            var winners = await _context.Winners.FindAsync(id);

            if (winners == null)
            {
                return NotFound();
            }

            return winners;
        }

        // PUT: api/WinnersEF/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWinners(int id, Winners winners)
        {
            if (id != winners.WinnerID)
            {
                return BadRequest();
            }

            _context.Winners.Include(w => w.Driver).Include(w => w.Car).Include(w => w.Track);
            _context.Entry(winners).State = EntityState.Modified;
            _context.Entry(winners.Driver).State = EntityState.Modified;
            _context.Entry(winners.Car).State = EntityState.Modified;
            _context.Entry(winners.Track).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WinnersExists(id))
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

        // POST: api/WinnersEF
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Winners>> PostWinners(Winners winners)
        {
            _context.Winners.Add(winners);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWinners", new { id = winners.WinnerID }, winners);
        }

        // DELETE: api/WinnersEF/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWinners(int id)
        {
            _context.Winners.Include(w => w.Driver).Include(w => w.Car).Include(w => w.Track).ToList();
            var winners = await _context.Winners.FindAsync(id);
            if (winners == null)
            {
                return NotFound();
            }

            _context.Drivers.Remove(winners.Driver);
            _context.Cars.Remove(winners.Car);
            _context.Tracks.Remove(winners.Track);
            _context.Winners.Remove(winners);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WinnersExists(int id)
        {
            return _context.Winners.Any(e => e.WinnerID == id);
        }
    }
}
