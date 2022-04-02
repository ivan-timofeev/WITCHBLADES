using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Witchblades.Backend.Data;
using Witchblades.Backend.Models;

namespace Witchblades.Backend.Controllers.V1
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class ArtistsController : ControllerBase
    //{
    //    private readonly WitchbladesContext _context;
    //
    //    public ArtistsController(WitchbladesContext context)
    //    {
    //        _context = context;
    //    }
    //
    //    // GET: api/Artists
    //    [HttpGet]
    //    public async Task<ActionResult<IEnumerable<Artist>>> GetArtists()
    //    {
    //        return await _context.Artists.ToListAsync();
    //    }
    //
    //    // GET: api/Artists/5
    //    [HttpGet("{id}")]
    //    public async Task<ActionResult> GetArtist(int id)
    //    {
    //        //var artist = await _context.Artists
    //        //    .Include(t => t.Albums)
    //        //    .Include(t => t.Tracks)
    //        //    .FirstOrDefaultAsync(t => t.Id == id);
    //        //
    //        //if (artist == null)
    //        //{
    //        //    return NotFound();
    //        //}
    //
    //        return Ok();
    //    }
    //
    //    // PUT: api/Artists/5
    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> PutArtist(int id, Artist artist)
    //    {
    //        _context.Entry(artist).State = EntityState.Modified;
    //
    //        try
    //        {
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            //if (!ArtistExists(id))
    //            //{
    //            //    return NotFound();
    //            //}
    //            //else
    //            //{
    //            //    throw;
    //            //}
    //        }
    //
    //        return NoContent();
    //    }
    //
    //    // POST: api/Artists
    //    [HttpPost]
    //    public async Task<ActionResult<Artist>> PostArtist(Artist artist)
    //    {
    //        _context.Artists.Add(artist);
    //        await _context.SaveChangesAsync();
    //
    //        return CreatedAtAction("GetArtist", new { id = artist.Id }, artist);
    //    }
    //
    //    // DELETE: api/Artists/5
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeleteArtist(int id)
    //    {
    //        var artist = await _context.Artists.FindAsync(id);
    //        if (artist == null)
    //        {
    //            return NotFound();
    //        }
    //
    //        _context.Artists.Remove(artist);
    //        await _context.SaveChangesAsync();
    //
    //        return NoContent();
    //    }
    //}
}
