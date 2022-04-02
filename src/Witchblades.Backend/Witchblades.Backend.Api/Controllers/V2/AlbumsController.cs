using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Witchblades.Backend.Data;

namespace Witchblades.Backend.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlbumsController : ControllerBase
    {
        #region Private fields
        private readonly WitchbladesContext _context;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public AlbumsController(
            WitchbladesContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion


        #region GET: api/Albums
        /// <summary>
        /// Returns all albums
        /// </summary>
        [HttpGet]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public ActionResult Get()
        {
            return Ok("VERSIONING WORKS");
        }
        #endregion
    }
}
