#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using COCAINE.Data;
using COCAINE.Models.DomainModels;
using COCAINE.Models.ViewModels;
using COCAINE.FilteringLogic;

namespace COCAINE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracksController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TracksController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Tracks
        [HttpGet]
        public ActionResult<IEnumerable<Track>> GetTracks([FromQuery] string filter)
        {
            // Select data from database
            var tracks = _context.Tracks
                .Include(t => t.TrackArtists)
                .Include(t => t.TrackAlbum);

            // Filtering and sorting
            var manager = new TracksSortingManager(tracks);
            var filtered = manager.ApplySort(filter).ToList();

            // Remove shit
            filtered.ForEach(
                item => item.TrackArtists.ForEach(
                    artist => artist.Tracks = null));

            // Return results
            return filtered;
        }

        [Route("GetTracksOfArtist/{artistId}")]
        [HttpGet]
        public async Task<ActionResult<TracksOfArtists>> GetTracksOfArtist(int artistId)
        {
            var artist = await _context.Artists
                .Include(t => t.Albums)
                .Include(t => t.Tracks)
                .ThenInclude(t => t.TrackArtists)
                .FirstOrDefaultAsync(t => t.Id == artistId);

            if (artist is null)
            {
                return NotFound();
            }

            var result = new TracksOfArtists()
            {
                Artist = artist,
                Albums = artist.Albums
            };

            // Removing a redundant data
            result.Artist.Tracks = null;
            result.Artist.Albums = null;
            result.Albums.ForEach(album =>
            {
                album.Artist = null;

                if (album.Tracks is null)
                {
                    album.Tracks = new List<Track>();
                }
            });

            return result;
        }

        // GET: api/Tracks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Track>> GetTrack(int id)
        {
            var track = await _context.Tracks
                .Include(t => t.TrackArtists)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (track == null)
            {
                return NotFound();
            }

            track.TrackArtists.ForEach(t => t.Tracks = null);

            return track;
        }

        // PUT: api/Tracks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrack(int id, Track track)
        {
            if (id != track.Id)
            {
                return BadRequest();
            }

            _context.Entry(track).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrackExists(id))
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

        // POST: api/Tracks
        [HttpPost]
        public async Task<ActionResult<Track>> PostTrack([FromBody] CreateTrack model)
        {
            var artists = new List<Artist>();
            var album = await _context.Albums.FindAsync(model.AlbumId);

            // Album specified but not found
            if (model.AlbumId is not null && album is null)
            {
                return NotFound($"Album specified but not found. (Album id is {model.AlbumId})");
            }

            foreach (int artistId in model.ArtistsIds)
            {
                var artist = await _context.Artists.FirstOrDefaultAsync(t => t.Id == artistId);

                if (artist is null)
                {
                    return NotFound($"Artist with id {artistId} not found");
                }

                artists.Add(artist);
            }

            // Create new track object
            var track = new Track()
            {
                TrackName = model.NewTrackName,
                Lyrics = model.NewTrackLyrics,
                TrackAlbum = album,
                TrackArtists = artists
            };

            // Save track into the database
            _context.Tracks.Add(track);
            await _context.SaveChangesAsync();

            // Remove shitty data
            track.TrackArtists.ForEach(artist => artist.Tracks = null);

            return track;
        }

        // DELETE: api/Tracks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrack(int id)
        {
            var track = await _context.Tracks.FindAsync(id);
            if (track == null)
            {
                return NotFound();
            }

            _context.Tracks.Remove(track);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrackExists(int id)
        {
            return _context.Tracks.Any(e => e.Id == id);
        }
    }
}
