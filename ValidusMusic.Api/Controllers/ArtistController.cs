using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidusMusic.Core.Domain;
using ValidusMusic.Core.Domain.Repository;
using ValidusMusic.Core.ExternalContracts.DataTransfer;

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
    }

    public class GetAllArtistsQuery: IRequest<Result<IEnumerable<Artist>>>
    {
    }

    public class GetAllArtistsQueryHandler : IRequestHandler<GetAllArtistsQuery, Result<IEnumerable<Artist>>>
    {
        private readonly IArtistRepository _repository;

        public GetAllArtistsQueryHandler(IArtistRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<IEnumerable<Artist>>> Handle(GetAllArtistsQuery request, CancellationToken cancellationToken)
        {
            var allLeads = await _repository.GetAll();
            if (allLeads.IsSuccess)
                return Result.Ok(allLeads.Value);

            return allLeads;
        }
    }

    public class ArtistProfile : Profile
    {
        public ArtistProfile()
        {
            CreateMap<Artist, ArtistDto>();
        }
    }
}
