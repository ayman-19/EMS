using EMS.Application.Features.Employees.Queries.GetById;
using EMS.Domain.Abstraction;
using Moq;
using NUnit.Framework;

namespace EMS.Test.Feature.Employees
{
    [TestFixture]
    public sealed class Test
    {
        private GetEmployeeHandler handler;

        [SetUp]
        public void Setup()
        {
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            handler = new GetEmployeeHandler(employeeRepositoryMock.Object);
        }

        [Test]
        public async Task GetEmployeeTestSuccesResponse()
        {
            var arrange = await handler.Handle(
                new GetEmployeeQuery(Guid.NewGuid()),
                CancellationToken.None
            );
            var success = true;
            Assert.AreEqual(arrange.Success, success);
        }
    }
}
