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

        #region GET: api/Artists
        /// <summary>
        /// Get artists with pagination
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedModel<Artist>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status424FailedDependency, Type = typeof(ProblemDetails))]
        public async Task<ActionResult> GetArtist(Guid id)
        {
            var artist = await _context.Artists
                .Include(t => t.MusicLabel)
                .Include(t => t.Albums)
                .Include("Tracks.TrackArtists")
                .FirstOrDefaultAsync(t => t.Id == id);

            if (artist is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Artist>(artist));
        }
        #endregion

        #region PUT: api/Artists/5
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(Artist))]
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
        [HttpPost]
        public async Task<ActionResult<Artist>> PostArtist(Artist artist)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region DELETE: api/Artists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
