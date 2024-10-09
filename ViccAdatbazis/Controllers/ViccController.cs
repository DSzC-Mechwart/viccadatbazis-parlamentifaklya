using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViccAdatbazis.Data;
using ViccAdatbazis.Models;

namespace ViccAdatbazis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViccController : ControllerBase
    {
        private readonly ViccDbContext _context;

        public ViccController(ViccDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vicc>>> GetVicc(int page = 1)
        {
            int pageSize = 10;
            var jokes = await _context.Viccek.Where(j => !j.Aktiv).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(jokes);
        }

        [HttpPost]
        public async Task<ActionResult<Vicc>> PostVicc([FromBody] Vicc vicc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Viccek.Add(vicc);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVicc), new { id = vicc.Id }, vicc);
        }

        [HttpPost("{id}/vote")]
        public async Task<ActionResult<Vicc>> VoteVicc(int id, [FromBody] string type)
        {
            var vicc = await _context.Viccek.FindAsync(id);
            if (vicc == null) return NotFound();

            if (type == "Like")
            {
                vicc.Tetszik++;
            }
            else if (type == "Dislike")
            {
                vicc.NemTetszik++;
            }

            await _context.SaveChangesAsync();
            return Ok(vicc);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVicc(int id, [FromBody] Vicc vicc)
        {
            if(id != vicc.Id) return BadRequest();

            _context.Entry(vicc).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVicc(int id)
        {
            var vicc = await _context.Viccek.FindAsync(id);
            if (vicc == null) return NotFound();

            vicc.Aktiv = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
