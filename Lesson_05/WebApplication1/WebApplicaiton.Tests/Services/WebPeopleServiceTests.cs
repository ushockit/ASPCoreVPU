using Moq;
using Services.Abstract;
using Services.Abstract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Services.Abstract;
using WebApplication1.Services.Impl;
using Xunit;

namespace WebApplicaiton.Tests.Services
{
    public class WebPeopleServiceTests
    {
        [Fact]
        public void CreateNewPerson()
        {
            // Arrange
            var peopleServiceMock = new Mock<IPeopleService>();
            var serviceManagerMock = new Mock<IServiceManager>();

            var createPersonDtoMock = new PersonDto
            {
                FirstName = "Vasya",
                LastName = "Pupkin",
                Birth = new DateTime(1977, 10, 12)
            };
            var returnPersonDtoMock = new PersonDto
            {
                FirstName = "Vasya",
                LastName = "Pupkin",
                Birth = new DateTime(1977, 10, 12),
                Id = Guid.NewGuid()
            };
            peopleServiceMock
                .Setup(ps => ps.CreateNewPerson(createPersonDtoMock))
                .Returns(() => returnPersonDtoMock);
            serviceManagerMock.SetupProperty(sm => sm.PeopleService, peopleServiceMock.Object);

            // Act
            var service = new WebPeopleService(serviceManagerMock.Object);

            // Assert
            // var createdPerson = service.CreateNewPerson(null);
        }
    }
}
