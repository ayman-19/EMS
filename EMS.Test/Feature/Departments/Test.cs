using EMS.Application.Features.Departments.Queries.GetById;
using EMS.Domain.Abstraction;
using Moq;
using NUnit.Framework;

namespace EMS.Test.Feature.Departments
{
    [TestFixture]
    public sealed class Test
    {
        private GetDepartmentHandler handler;

        [SetUp]
        public void Setup()
        {
            var departmentRepositoryMock = new Mock<IDepartmentRepository>();
            handler = new GetDepartmentHandler(departmentRepositoryMock.Object);
        }

        [Test]
        public async Task GetDepartmentTestSuccesResponse()
        {
            var arrange = await handler.Handle(
                new GetDepartmentQuery(Guid.NewGuid()),
                CancellationToken.None
            );
            var success = true;
            Assert.AreEqual(arrange.Success, success);
        }
    }
}
