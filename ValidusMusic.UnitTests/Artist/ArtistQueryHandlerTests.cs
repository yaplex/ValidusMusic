using Moq;
using ValidusMusic.Core.Domain;
using ValidusMusic.Core.Domain.Artists.Queries;
using ValidusMusic.Core.Domain.Repository;
using Xunit;

namespace ValidusMusic.UnitTests.Artist
{

    public class ArtistQueryHandlerTests
    {
        [Fact]
        public async Task Should_Call_Repository_to_Return_Single_Artist()
        {
            var query = new GetArtistQuery(5);
            var mockArtistRepository = new Mock<IArtistRepository>();
            mockArtistRepository.Setup(x => x.GetById(5)).ReturnsAsync(new Core.Domain.Artist() { Id = 5, Name = "xUnit" });
            var queryHandler = new GetArtistQueryHandler(mockArtistRepository.Object);
            var queryHandlerResponse = await queryHandler.Handle(query, CancellationToken.None);
            Assert.True(queryHandlerResponse.IsSuccess);
            mockArtistRepository.Verify(x => x.GetById(5), Times.Once);
        }

        [Fact]
        public async Task Should_Return_Fail_Result_When_Artist_Not_Found()
        {
            var query = new GetArtistQuery(50);
            var mockArtistRepository = new Mock<IArtistRepository>();
            mockArtistRepository.Setup(x => x.GetById(5)).ReturnsAsync(new Core.Domain.Artist() { Id = 5, Name = "xUnit" });
            var queryHandler = new GetArtistQueryHandler(mockArtistRepository.Object);
            var queryHandlerResponse = await queryHandler.Handle(query, CancellationToken.None);
            Assert.False(queryHandlerResponse.IsSuccess);
            mockArtistRepository.Verify(x => x.GetById(50), Times.Once);
            Assert.Single(queryHandlerResponse.Reasons);
        }
    }
}