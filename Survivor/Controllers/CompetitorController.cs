using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Survivor.Data;
using Survivor.Entites;

namespace Survivor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitorController : ControllerBase
    {
        private readonly SurvivorDbContext _context;

        public CompetitorController(SurvivorDbContext context)
        {
            _context = context;
        }

        // GET: api/competitors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompetitorEntity>>> GetCompetitors()
        {
            return await _context.Competitors.Include(c => c.Category).ToListAsync();
        }

        // GET: api/competitors/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CompetitorEntity>> GetCompetitor(int id)
        {
            var competitor = await _context.Competitors.Include(c => c.Category).FirstOrDefaultAsync(c => c.Id == id);
            if (competitor == null)
            {
                return NotFound();
            }
            return competitor;
        }

        // GET: api/competitors/categories/{categoryId}
        [HttpGet("categories/{categoryId}")]
        public async Task<ActionResult<IEnumerable<CompetitorEntity>>> GetCompetitorsByCategory(int categoryId)
        {
            return await _context.Competitors.Where(c => c.CategoryId == categoryId).ToListAsync();
        }

        // POST: api/competitors
        [HttpPost]
        public async Task<ActionResult<CompetitorEntity>> CreateCompetitor(CompetitorEntity competitor)
        {
            _context.Competitors.Add(competitor);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCompetitor), new { id = competitor.Id }, competitor);
        }

        // PUT: api/competitors/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompetitor(int id, CompetitorEntity competitor)
        {
            if (id != competitor.Id)
            {
                return BadRequest();
            }

            _context.Entry(competitor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/competitors/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompetitor(int id)
        {
            var competitor = await _context.Competitors.FindAsync(id);
            if (competitor == null)
            {
                return NotFound();
            }

            _context.Competitors.Remove(competitor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
