using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ValidusMusic.Core.Domain.Artists.Commands;
using ValidusMusic.Core.Domain.Artists.Queries;
using ValidusMusic.Core.Domain.Queries;
using ValidusMusic.Core.ExternalContracts.DataTransfer.Artist;

namespace ValidusMusic.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtistController : ControllerBase
    {
        private readonly ILogger<ArtistController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ArtistController(ILogger<ArtistController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistDto>>> Get()
        {
            _logger.LogInformation("Get All Artists");

            var allArtists = await _mediator.Send(new GetAllArtistsQuery());
            if (allArtists.IsSuccess)
            {
                return Ok(_mapper.Map<IEnumerable<ArtistDto>>(allArtists.Value));
            }

            return NotFound();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<IEnumerable<ArtistDto>>> Get(long id)
        {
            _logger.LogInformation($"Get Artists with ID: {id}");

            var artist = await _mediator.Send(new GetArtistQuery(id));
            if (artist.IsSuccess)
            {
                return Ok(_mapper.Map<ArtistDto>(artist.Value));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreateArtistDto artist)
        {
            _logger.LogInformation("Creating new Artists");

            var createdArtist = await _mediator.Send(new CreateNewArtistCommand(artist.Name));
            if (createdArtist.IsSuccess)
                return CreatedAtAction("Get", new { Id = createdArtist.Value.Id },
                    _mapper.Map<ArtistDto>(createdArtist.Value));
            return BadRequest(createdArtist.Reasons);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Put(long id, UpdateArtistDto updateDto)
        {
            _logger.LogInformation($"Updating Artists with id: {id}");

            var artist = await _mediator.Send(new GetArtistQuery(id));
            if (artist.IsSuccess)
            {
                var updateResult = await _mediator.Send(new UpdateArtistCommand(updateDto, artist.Value));
                if (updateResult.IsSuccess)
                    return Ok();
                return BadRequest(updateResult.Reasons);
            }

            return NotFound(id);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            _logger.LogInformation($"Deleting Artist: {id}");

            var artist = await _mediator.Send(new GetArtistQuery(id));
            if (artist.IsSuccess)
            {
                var deleteResult = await _mediator.Send(new DeleteArtistCommand(artist.Value));
                if(deleteResult.IsSuccess)
                    return Ok(id);
                return BadRequest(deleteResult.Reasons);

            }
            return NotFound(id);
        }
    }

   
}
