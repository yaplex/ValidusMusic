using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidusMusic.Core.Domain.Albums.Commands;
using ValidusMusic.Core.Domain.Albums.Queries;
using ValidusMusic.Core.Domain.Artists.Commands;
using ValidusMusic.Core.ExternalContracts.DataTransfer;
using ValidusMusic.Core.ExternalContracts.DataTransfer.Album;
using ValidusMusic.Core.ExternalContracts.DataTransfer.Artist;

namespace ValidusMusic.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly ILogger<AlbumController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AlbumController(ILogger<AlbumController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> Get()
        {
            _logger.LogInformation("Get All Albums");

            var all = await _mediator.Send(new GetAllAlbumsQuery());
            if (all.IsSuccess)
            {
                return Ok(_mapper.Map<IEnumerable<AlbumDto>>(all.Value));
            }

            return NotFound();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> Get(long id)
        {
            _logger.LogInformation($"Get Artists with ID: {id}");
        
            var result = await _mediator.Send(new GetAlbumQuery(id));
            if (result.IsSuccess)
            {
                return Ok(_mapper.Map<AlbumDto>(result.Value));
            }
        
            return NotFound();
        }
        
        [HttpPost]
        public async Task<ActionResult> Post(CreateAlbumDto album)
        {
            _logger.LogInformation("Creating new Album");
        
            var created = await _mediator.Send(new CreateNewAlbumCommand(album.Name, album.YearReleased));
            if (created.IsSuccess)
                return CreatedAtAction("Get", new { Id = created.Value.Id },
                    _mapper.Map<AlbumDto>(created.Value));
            return BadRequest(created.Reasons);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Put(long id, UpdateAlbumDto updateDto)
        {
            _logger.LogInformation($"Updating Album with id: {id}");
        
            var album = await _mediator.Send(new GetAlbumQuery(id));
            if (album.IsSuccess)
            {
                var updateResult = await _mediator.Send(new UpdateAlbumCommand(updateDto, album.Value));
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
            _logger.LogInformation($"Deleting Album: {id}");
        
            var album = await _mediator.Send(new GetAlbumQuery(id));
            if (album.IsSuccess)
            {
                var deleteResult = await _mediator.Send(new DeleteAlbumCommand(album.Value));
                if (deleteResult.IsSuccess)
                    return Ok(id);
                return BadRequest(deleteResult.Reasons);
        
            }
            return NotFound(id);
        }
    }
}
