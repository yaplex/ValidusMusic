using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidusMusic.Core.ExternalContracts.DataTransfer;

namespace ValidusMusic.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SongController : ControllerBase
    {
        private readonly ILogger<SongController> _logger;

        public SongController(ILogger<SongController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<SongDto> Get()
        {
            _logger.LogInformation("Get All Songs");
            return new List<SongDto>();
        }
    }
}
