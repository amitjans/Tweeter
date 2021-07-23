using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tweeter.Models;

namespace Tweeter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelationshipController : ControllerBase
    {
        private readonly tweeterContext _context;

        public RelationshipController(tweeterContext context)
        {
            _context = context;
        }

        // GET: api/Relationship
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Relationship>>> GetRelationships()
        {
            return await _context.Relationships.ToListAsync();
        }

        // GET: api/Relationship/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Relationship>> GetRelationship(long id)
        {
            var relationship = await _context.Relationships.FindAsync(id);

            if (relationship == null)
            {
                return NotFound();
            }

            return relationship;
        }

        // PUT: api/Relationship/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRelationship(long id, Relationship relationship)
        {
            if (id != relationship.Id)
            {
                return BadRequest();
            }

            _context.Entry(relationship).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelationshipExists(id))
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

        // POST: api/Relationship
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Relationship>> PostRelationship(Relationship relationship)
        {
            relationship.CreatedAt = DateTime.Now;
            _context.Relationships.Add(relationship);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRelationship", new { id = relationship.Id }, relationship);
        }

        // DELETE: api/Relationship/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRelationship(long id)
        {
            var relationship = await _context.Relationships.FindAsync(id);
            if (relationship == null)
            {
                return NotFound();
            }

            _context.Relationships.Remove(relationship);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RelationshipExists(long id)
        {
            return _context.Relationships.Any(e => e.Id == id);
        }
    }
}
