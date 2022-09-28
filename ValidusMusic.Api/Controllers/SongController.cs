using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ValidusMusic.Core.Domain.Songs.Commands;
using ValidusMusic.Core.Domain.Songs.Queries;
using ValidusMusic.Core.ExternalContracts.DataTransfer.Song;

namespace ValidusMusic.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SongController : ControllerBase
    {
        private readonly ILogger<SongController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SongController(ILogger<SongController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongDto>>> Get()
        {
            _logger.LogInformation("Get All Songs");

            var allSongs = await _mediator.Send(new GetAllSongsQuery());
            if (allSongs.IsSuccess)
            {
                return Ok(_mapper.Map<IEnumerable<SongDto>>(allSongs.Value));
            }

            return NotFound();
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<IEnumerable<SongDto>>> Get(long id)
        {
            _logger.LogInformation($"Get Songs with ID: {id}");
        
            var artist = await _mediator.Send(new GetSongQuery(id));
            if (artist.IsSuccess)
            {
                return Ok(_mapper.Map<SongDto>(artist.Value));
            }
        
            return NotFound();
        }
        
        [HttpPost]
        public async Task<ActionResult> Post(CreateSongDto artist)
        {
            _logger.LogInformation("Creating new Songs");
        
            var createdSong = await _mediator.Send(new CreateNewSongCommand(artist));
            if (createdSong.IsSuccess)
                return CreatedAtAction("Get", new { Id = createdSong.Value.Id },
                    _mapper.Map<SongDto>(createdSong.Value));
            return BadRequest(createdSong.Reasons);
        }
        
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Put(long id, UpdateSongDto updateDto)
        {
            _logger.LogInformation($"Updating Songs with id: {id}");
        
            var artist = await _mediator.Send(new GetSongQuery(id));
            if (artist.IsSuccess)
            {
                var updateResult = await _mediator.Send(new UpdateSongCommand(updateDto, artist.Value));
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
            _logger.LogInformation($"Deleting Song: {id}");
        
            var song = await _mediator.Send(new GetSongQuery(id));
            if (song.IsSuccess)
            {
                var deleteResult = await _mediator.Send(new DeleteSongCommand(song.Value));
                if (deleteResult.IsSuccess)
                    return Ok(id);
                return BadRequest(deleteResult.Reasons);
        
            }
            return NotFound(id);
        }
    }


}
