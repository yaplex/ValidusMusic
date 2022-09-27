using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidusMusic.Core.ExternalContracts.DataTransfer;

namespace ValidusMusic.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtistController : ControllerBase
    {
        private readonly ILogger<ArtistController> _logger;

        public ArtistController(ILogger<ArtistController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ArtistDto> Get()
        {
            _logger.LogInformation("Get All Artists");
            return new List<ArtistDto>();
        }
    }
}
