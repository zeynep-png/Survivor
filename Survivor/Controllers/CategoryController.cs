using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Survivor.Data;
using Survivor.Entites;

namespace Survivor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly SurvivorDbContext _context;

        public CategoryController(SurvivorDbContext context)
        {
            _context = context;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryEntity>>> GetCategories()
        {
            return await _context.Categories.Include(c => c.Competitors).ToListAsync();
        }

        // GET: api/categories/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryEntity>> GetCategory(int id)
        {
            var category = await _context.Categories.Include(c => c.Competitors).FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return category;
        }

        // POST: api/categories
        [HttpPost]
        public async Task<ActionResult<CategoryEntity>> CreateCategory(CategoryEntity category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        // PUT: api/categories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryEntity category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/categories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
