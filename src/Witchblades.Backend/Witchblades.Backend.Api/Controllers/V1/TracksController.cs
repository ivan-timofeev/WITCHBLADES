using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Witchblades.Backend.Api;
using Witchblades.Backend.Api.DataContracts.ViewModels;
using Witchblades.Backend.Api.Utils;
using Witchblades.Backend.Data;

namespace Witchblades.Backend.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TracksController : ControllerBase
    {
        #region Private fields
        private readonly WitchbladesContext _context;
        private readonly IMapper _mapper;
        private readonly IPagedModelFactory _pagedModelFactory;
        #endregion

        #region Constructors
        public TracksController(
            WitchbladesContext context,
            IMapper mapper,
            IPagedModelFactory pagedModelFactory)
        {
            _context = context;
            _mapper = mapper;
            _pagedModelFactory = pagedModelFactory;
        }
        #endregion


        #region GET: api/Tracks/{id}
        /// <summary>
        /// Returns the track
        /// </summary>
        /// <param name="id">Track GUID</param>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Track))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get(Guid id)
        {
            var track = await _context.Tracks
                .Include(t => t.TrackArtists)
                .Include(t => t.TrackAlbum)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (track is null)
            {
                return NotFound();
            }

            // Return results
            return Ok(_mapper.Map<Track>(track));
        }
        #endregion

        #region PUT: api/Tracks/{id}
        /// <summary>
        /// Updates the track (null fields will be not updated)
        /// </summary>
        /// <param name="id">Track GUID</param>
        /// <response code="424">Failed Dependency Error</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(Track))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status424FailedDependency, Type = typeof(ProblemDetails))]
        public async Task<ActionResult> Put(Guid id, TrackUpdate newState)
        {
            var track = await _context.Tracks
                .Include(t => t.TrackArtists)
                .Include(t => t.TrackAlbum)
                .ThenInclude(t => t.Artist)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (track is null)
            {
                return NotFound();
            }

            if (newState.TrackName != null)
                track.TrackName = newState.TrackName;
            if (newState.Duration != null)
                track.Duration = newState.Duration;
            if (newState.TrackUrl != null)
                track.TrackUrl = newState.TrackUrl;
            if (newState.InAlbumNumber != null)
                track.InAlbumNumber = newState.InAlbumNumber.Value;
            if (newState.Lyrics != null)
                track.Lyrics = newState.Lyrics;

            if (newState.Album != null)
            {
                var album = await _context.Albums.FirstOrDefaultAsync(t => t.Id == newState.Album);

                if (album is null)
                {
                    return Problem($"Album with id '{newState.Album}' not found",
                        "Album", 424, "Failed dependency error", "Album");
                }
                else
                {
                    track.TrackAlbum = album;
                }
            }

            if (newState.Artist != null)
            {
                var artist = await _context.Artists.FirstOrDefaultAsync(t => t.Id == newState.Artist);

                if (artist is null)
                {
                    return Problem($"Artist with id '{newState.Artist}' not found",
                        "Artist", 424, "Failed dependency error", "Artist");
                }
                else
                {
                    track.TrackArtists.Clear();
                    track.TrackArtists.Add(artist);
                }
            }

            if (newState.Collaboration != null)
            {
                var owner = track.TrackAlbum.Artist;
                track.TrackArtists = new List<Models.Artist>(newState.Collaboration.Count() + 1);
                track.TrackArtists.Add(owner);

                foreach (var artistId in newState.Collaboration)
                {
                    var artist = await _context.Artists.FirstOrDefaultAsync(t => t.Id == artistId);

                    if (artist is null)
                    {
                        return Problem($"Collaboration artist with id '{artistId}' not found",
                            "Artist", 424, "Failed dependency error", "Artist");
                    }
                    else
                    {
                        track.TrackArtists.Add(artist);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return Accepted(_mapper.Map<Track>(track));
        }
        #endregion

        #region POST: api/Tracks
        /// <summary>
        /// Creates a track
        /// </summary>
        /// <param name="id">Track GUID</param>
        /// <response code="424">Failed Dependency Error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Track))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status424FailedDependency, Type = typeof(ProblemDetails))]
        public async Task<ActionResult> Post(TrackCreate track)
        {
            // Это дерьмо не работает
            var newTrack = new Models.Track
            {
                TrackArtists = new List<Models.Artist>(),
                InAlbumNumber = track.InAlbumNumber,
                Duration = track.Duration,
                Lyrics = track.Lyrics,
                TrackUrl = track.TrackUrl
            };

            var album = await _context.Albums.FirstOrDefaultAsync(t => t.Id == track.Album);

            if (album is null)
            {
                return Problem($"Album with id '{track.Album}' not found",
                        "Album", 424, "Failed dependency error", "Album");
            }

            newTrack.TrackArtists.Add(album.Artist);
            newTrack.TrackAlbum = album;
            _context.Tracks.Add(newTrack);
            album.Tracks.Add(newTrack);

            if (track.Collaboration != null)
            {
                throw new NotImplementedException();
            }

            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<Track>(newTrack));

            //if (track.Collaboration != null)
            //{
            //    var owner = newTrack.TrackAlbum.Artist;
            //    newTrack.TrackArtists = new List<Models.Artist>(track.Collaboration.Count() + 1);
            //    newTrack.TrackArtists.Add(owner);
            //
            //    foreach (var artistId in track.Collaboration)
            //    {
            //        var artist = await _context.Artists.FirstOrDefaultAsync(t => t.Id == artistId);
            //
            //        if (artist is null)
            //        {
            //            return Problem($"Collaboration artist with id '{artistId}' not found",
            //                "Artist", 424, "Failed dependency error", "Artist");
            //        }
            //        else
            //        {
            //            newTrack.TrackArtists.Add(artist);
            //        }
            //    }
            //}
        }
        #endregion
    }
}