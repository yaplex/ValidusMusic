using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidusMusic.Core.ExternalContracts.DataTransfer;

namespace ValidusMusic.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly ILogger<AlbumController> _logger;

        public AlbumController(ILogger<AlbumController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<AlbumDto> Get()
        {
            _logger.LogInformation("Get All Albums");
            return new List<AlbumDto>();
        }
    }
}
