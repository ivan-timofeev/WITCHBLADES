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
    public class ArtistsController : ControllerBase
    {
        #region Private fields
        private readonly WitchbladesContext _context;
        private readonly IMapper _mapper;
        private readonly IPagedModelFactory _pagedModelFactory;
        #endregion

        #region Constructors
        public ArtistsController(
            WitchbladesContext context,
            IMapper mapper,
            IPagedModelFactory pagedModelFactory)
        {
            _context = context;
            _mapper = mapper;
            _pagedModelFactory = pagedModelFactory;
        }
        #endregion


        #region GET: api/Artists (with pagination)
        /// <summary>
        /// Get artists with pagination
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedModel<Artist>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<ActionResult> GetArtists([FromQuery] PaginationParameters options)
        {
            // IOrderedQueryable
            var query = _context.Artists
                .Include(t => t.Albums)
                .Include(t => t.MusicLabel)
                .Include("Tracks.TrackArtists")
            //  .AsNoTracking()
                .OrderBy(t => t.ArtistName);

            var pagedModel = await _pagedModelFactory.CreatePagedModelAsync<Models.Artist, Artist>(options, query);

            if (pagedModel.PageElemensCount == 0)
            {
                return NoContent();
            }

            return Ok(pagedModel);
        }
        #endregion

        #region GET: api/Artists/{id}
        /// <summary>
        /// Returns the Artist by id
        /// </summary>
        /// <param name="id">Artist GUID</param>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Artist))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status424FailedDependency, Type = typeof(ProblemDetails))]
        public async Task<ActionResult> GetArtist(Guid id)
        {
            var artist = await _context.Artists
                .Include(t => t.MusicLabel)
                .Include(t => t.Albums)
                .Include("Tracks.TrackArtists")
            //  .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (artist is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Artist>(artist));
        }
        #endregion

        #region PUT: api/Artists/5
        /// <summary>
        /// Updates the artist (null fields will be not updated)
        /// </summary>
        /// <param name="id">Artist GUID</param>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(Artist))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutArtist(Guid id, ArtistUpdate newState)
        {
            var model = await _context.Artists
                .Include(t => t.Albums)
                .Include(t => t.MusicLabel)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (model is null)
            {
                return NotFound();
            }

            if (newState.ArtistName != null)
                model.ArtistName = newState.ArtistName;
            if (newState.ArtistImage != null)
                model.ArtistImage = newState.ArtistImage;

            if (newState.MusicLabel != null)
            {
                var musicLabel = await _context.Labels.FirstOrDefaultAsync(t => t.Id == newState.MusicLabel);

                if (musicLabel is null)
                {
                    return Problem($"MusicLabel with id '{newState.MusicLabel}' not found",
                        "MusicLabel", 424, "Failed dependency error", "MusicLabel");
                }
                else
                {
                    model.MusicLabel = musicLabel;
                }
            }

            if (newState.Albums != null)
            {
                var albums = new List<Models.Album>(newState.Albums.Count());

                foreach (var albumId in newState.Albums)
                {
                    var album = await _context.Albums.FirstOrDefaultAsync(t => t.Id == albumId);

                    if (album is null)
                    {
                        return Problem($"Album with id '{newState.MusicLabel}' not found",
                            "Album", 424, "Failed dependency error", "Album");
                    }
                    else
                    {
                        albums.Add(album);
                    }
                }

                model.Albums = albums;
            }

            await _context.SaveChangesAsync();

            return Accepted(_mapper.Map<Artist>(model));
        }
        #endregion

        #region POST: api/Artists
        /// <summary>
        /// Creates an artist
        /// </summary>
        /// <response code="424">Failed Dependency Error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Artist))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status424FailedDependency, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<Artist>> PostArtist(ArtistCreate artist)
        {
            var newArtist = new Models.Artist
            {
                ArtistName = artist.ArtistName,
                ArtistImage = artist.ArtistImage
            };

            if (newArtist.MusicLabel != null)
            {
                var musicLabel = await _context.Labels.FirstOrDefaultAsync(t => t.Id == artist.MusicLabelId);

                if (musicLabel is null)
                {
                    return Problem($"MusicLabel with id '{newArtist.MusicLabel}' not found",
                            "MusicLabel", 424, "Failed dependency error", "MusicLabel");
                }
                else
                {
                    newArtist.MusicLabel = musicLabel;
                }
            }

            if (artist.Albums != null && artist.Albums.Count() > 0)
            {
                var albums = new List<Models.Album>(artist.Albums.Count());

                foreach (var albumId in artist.Albums)
                {
                    var album = await _context.Albums.FirstOrDefaultAsync(t => t.Id == albumId);

                    if (album is null)
                    {
                        return Problem($"Album with id '{newArtist.MusicLabel}' not found",
                            "Album", 424, "Failed dependency error", "Album");
                    }
                    else
                    {
                        albums.Add(album);
                    }
                }

                newArtist.Albums = albums;
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", _mapper.Map<Artist>(newArtist));
        }
        #endregion

        #region DELETE: api/Artists/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(Artist))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status424FailedDependency, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteArtist(Guid id)
        {
            var artist = await _context.Artists.FirstOrDefaultAsync(t => t.Id == id);

            if (artist is null)
            {
                return NotFound();
            }

            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();

            return Accepted();
        }
        #endregion
    }
}
