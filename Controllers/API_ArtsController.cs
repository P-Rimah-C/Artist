using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Asernatat_3.Data;
using Asernatat_3.Models;

namespace Asernatat_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class API_ArtsController : ControllerBase
    {
        private readonly DataContext _context;

        public API_ArtsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/API_Arts
        [HttpGet]
        public IEnumerable<Art> GetArts()
        {
            return _context.Arts;
        }

        // GET: api/API_Arts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Art>> GetArt([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var art = await _context.Arts.FindAsync(id);

            if (art == null)
            {
                return NotFound();
            }

            return Ok(art);
        }

        // PUT: api/API_Arts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArt(Guid id, Art art)
        {
            if (id != art.Id)
            {
                return BadRequest();
            }

            _context.Entry(art).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtExists(id))
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

        // POST: api/API_Arts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Art>> PostArt(Art art)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Arts.Add(art);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArt", new { id = art.Id }, art);
        }

        // DELETE: api/API_Arts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Art>> DeleteArt(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var art = await _context.Arts.FindAsync(id);
            if (art == null)
            {
                return NotFound();
            }

            _context.Arts.Remove(art);
            await _context.SaveChangesAsync();

            return Ok(art);
        }

        private bool ArtExists(Guid id)
        {
            return _context.Arts.Any(e => e.Id == id);
        }
    }
}
