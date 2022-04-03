using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Witchblades.Backend.Api.DataContracts.ViewModels;
using Witchblades.Backend.Api.Utils;
using Witchblades.Backend.Data;

namespace Witchblades.Backend.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlbumsController : ControllerBase
    {
        #region Private fields
        private readonly WitchbladesContext _context;
        private readonly IMapper _mapper;
        private readonly IPagedModelFactory _pagedModelFactory;
        #endregion

        #region Constructors
        public AlbumsController(
            WitchbladesContext context,
            IMapper mapper,
            IPagedModelFactory pagedModelFactory)
        {
            _context = context;
            _mapper = mapper;
            _pagedModelFactory = pagedModelFactory;
        }
        #endregion


        #region GET: api/Albums (with pagination)
        /// <summary>
        /// Returns all albums with pagination
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedModel<Album>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Get([FromQuery] PaginationParameters options)
        {
            // IOrderedQueryable
            var query = _context.Albums
                .Include(t => t.Artist)
                .Include(t => t.Tracks)
                .Include("Tracks.TrackArtists")
                .AsNoTracking()
                .OrderByDescending(t => t.ReleaseDate);

            var pagedModel = await _pagedModelFactory.CreatePagedModelAsync<Models.Album, Album>(options, query);

            if (pagedModel.PageElemensCount == 0)
            {
                return NoContent();
            }

            return Ok(pagedModel);
        }
        #endregion

        #region GET: api/Albums/{id}
        /// <summary>
        /// Returns the album with specified id
        /// </summary>
        /// <param name="id">Album GUID</param>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Album))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get(Guid id)
        {
            var album = await _context.Albums
                .Include(t => t.Tracks)
                .Include("Tracks.TrackArtists")
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (album == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Album>(album));
        }
        #endregion

        #region PUT: api/Albums/{id}
        /// <summary>
        /// Updates the Albums. Null fields will be not updated
        /// </summary>
        /// <param name="id">Album GUID</param>
        /// <returns></returns>
        /// <response code="424">Failed Dependency Error</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(Album))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status424FailedDependency, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Put(Guid id, AlbumUpdate newState)
        {
            var model = await _context.Albums
                .Include(t => t.Artist)
                .Include(t => t.Tracks)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (model is null)
            {
                return NotFound();
            }

            // Updating the album

            // Update the artist
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
                    model.Artist = artist;
                }
            }

            // Update the tracks
            if (newState.Tracks != null)
            {
                var tracks = new List<Models.Track>(newState.Tracks.Count());

                foreach (var trackId in newState.Tracks)
                {
                    var found = await _context.Tracks.FirstOrDefaultAsync(t => t.Id == trackId);

                    if (found is null)
                    {
                        return Problem($"Track with id '{trackId}' not found",
                            "Track", 424, "Failed dependency error", "Track");
                    }
                    else
                    {
                        tracks.Add(found);
                    }
                }

                model.Tracks = tracks;
            }

            // Update other fields
            if (newState.ReleaseDate != null)
                model.ReleaseDate = newState.ReleaseDate.Value;
            if (newState.AlbumName != null)
                model.AlbumName = newState.AlbumName;
            if (newState.AlbumImage != null)
                model.AlbumImage = newState.AlbumImage;

            await _context.SaveChangesAsync();

            return Accepted(_mapper.Map<Album>(model));
        }
        #endregion

        #region POST: api/Albums
        /// <summary>
        /// Creates an album
        /// </summary>
        /// <response code="424">Failed Dependency Error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Album))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status424FailedDependency, Type = typeof(ProblemDetails))]
        public async Task<ActionResult> Post(AlbumCreate album)
        {
            var newAlbum = new Models.Album();

            // Creating a new album

            // Check main artist
            {
                var artist = await _context.Artists.FirstOrDefaultAsync(t => t.Id == album.Artist);

                if (artist is null)
                {
                    return Problem($"Artist with id '{album.Artist}' not found",
                        "Artist", 424, "Failed dependency error", "Artist");
                }
                else
                {
                    newAlbum.Artist = artist;
                }
            }

            // Creating tracks
            {
                if (album.Tracks != null && album.Tracks.Any())
                {
                    var newTracks = new List<Models.Track>(album.Tracks.Count());

                    foreach (var track in album.Tracks)
                    {
                        var trackArtists = new List<Models.Artist>();
                        trackArtists.Add(newAlbum.Artist);

                        // Check collabaration
                        if (track.Collaboration != null && track.Collaboration.Any())
                        {
                            foreach (Guid artistId in track.Collaboration)
                            {
                                var collaborationArtist = await _context.Artists
                                    .FirstOrDefaultAsync(t => t.Id == artistId);

                                if (collaborationArtist is null)
                                {
                                    return Problem($"Collabaration artist with id '{album.Artist}' not found",
                                        "Artist", 424, "Failed dependency error", "Artist");
                                }

                                trackArtists.Add(collaborationArtist);
                            }
                        }

                        var newTrack = new Models.Track()
                        {
                            TrackName = track.TrackName,
                            InAlbumNumber = track.InAlbumNumber,
                            Duration = track.Duration,
                            Lyrics = track.Lyrics,
                            TrackUrl = track.TrackUrl,
                            TrackArtists = trackArtists
                        };

                        newTracks.Add(newTrack);
                    }

                    newAlbum.Tracks = newTracks;
                }
            }

            {
                newAlbum.ReleaseDate = album.ReleaseDate;
                newAlbum.AlbumName = album.AlbumName;
                newAlbum.AlbumImage = album.AlbumImage;
            }

            _context.Albums.Add(newAlbum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", _mapper.Map<Album>(newAlbum));
        }
        #endregion

        #region DELETE: api/Albums/{id}
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(Album))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAlbum(Guid id)
        {
            var album = await _context.Albums.FindAsync(id);

            if (album == null)
            {
                return NotFound();
            }

            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();

            return Accepted();
        }
        #endregion
    }
}
