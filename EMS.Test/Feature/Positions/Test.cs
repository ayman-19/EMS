using EMS.Application.Features.Positions.Queries.GetById;
using EMS.Domain.Abstraction;
using Moq;
using NUnit.Framework;

namespace EMS.Test.Feature.Positions
{
    [TestFixture]
    public sealed class Test
    {
        private GetPositionHandler handler;

        [SetUp]
        public void Setup()
        {
            var positionRepositoryMock = new Mock<IPositionRepository>();
            handler = new GetPositionHandler(positionRepositoryMock.Object);
        }

        [Test]
        public async Task GetPositionTestSuccesResponse()
        {
            var arrange = await handler.Handle(
                new GetPositionQuery(Guid.NewGuid()),
                CancellationToken.None
            );
            var success = false;
            Assert.AreEqual(arrange.Success, success);
        }
    }
}
