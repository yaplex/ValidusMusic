using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ValidusMusic.Api.Controllers;
using ValidusMusic.Core.Domain.Artists.Queries;
using ValidusMusic.Core.ExternalContracts.DataTransfer.Artist;
using Xunit;

namespace ValidusMusic.UnitTests.Artist;

public class ArtistControllerTests
{
    [Fact]
    public async Task Should_Return_Single_Artist()
    {
        var loggerMock = new Mock<ILogger<ArtistController>>();
        var mediatorMock = new Mock<IMediator>();
        var artist = new Core.Domain.Artist { Id = 5, Name = "Hello" };
        mediatorMock.Setup(x => x.Send(It.IsAny<GetArtistQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Ok(artist));
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x => x.Map<ArtistDto>(artist)).Returns(new ArtistDto { Name = "Hello", Id = 5 });
        var ctrl = new ArtistController(loggerMock.Object, mediatorMock.Object, mapperMock.Object);

        var getResult = await ctrl.Get(5);
        var stausCode = ((ObjectResult)getResult.Result).StatusCode;
        Assert.Equal(200, stausCode);
        Assert.Equal("Hello", ((ArtistDto)((ObjectResult)getResult.Result).Value).Name);
    }


    [Fact]
    public async Task Should_Return_NotFound_Result_When_Artist_DoesNotExists()
    {
        var loggerMock = new Mock<ILogger<ArtistController>>();
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(x => x.Send(It.IsAny<GetArtistQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Fail("Artist not found"));
        var mapperMock = new Mock<IMapper>();
        var ctrl = new ArtistController(loggerMock.Object, mediatorMock.Object, mapperMock.Object);

        var getResult = await ctrl.Get(5);
        var stausCode = ((NotFoundResult)getResult.Result).StatusCode;
        Assert.Equal(404, stausCode);
    }
}