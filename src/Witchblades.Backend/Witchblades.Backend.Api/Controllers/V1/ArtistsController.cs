using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult> GetArtist(Guid id)
        {
            var artist = await _context.Artists
                .Include(t => t.MusicLabel)
                .Include(t => t.Albums)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (artist is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Artist>(artist));
        }
        #endregion

        #region PUT: api/Artists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(int id, Artist artist)
        {
            throw new NotImplementedException();
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
