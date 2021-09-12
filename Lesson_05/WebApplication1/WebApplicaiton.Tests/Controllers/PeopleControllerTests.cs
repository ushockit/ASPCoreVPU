using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Controllers;
using WebApplication1.Models.People;
using WebApplication1.Services.Abstract;
using Xunit;

namespace WebApplicaiton.Tests.Controllers
{
    public class PeopleControllerTests
    {
        [Fact]
        public void IndexViewWithPeopleResult()
        {
            // Arrange
            var peopleServiceMock = new Mock<IWebPeopleService>();
            var cacheMock = new Mock<IMemoryCache>();
            var person1Mock = new PersonModel
            {
                Id = Guid.Parse("6ba315a9-b389-4f66-92eb-f2359f4b382c"),
                FirstName = "First name 1",
                LastName = "Last name 1",
                Birth = new DateTime(2000, 10, 05)
            };
            var person2Mock = new PersonModel
            {
                Id = Guid.Parse("b348ca3a-fee0-4531-9a2b-c1b5a480ce96"),
                FirstName = "First name 2",
                LastName = "Last name 2",
                Birth = new DateTime(1990, 1, 1)

            };
            var peopleListMock = new List<PersonModel> { person1Mock, person2Mock };

            peopleServiceMock
                .Setup(service => service.GetAllPeople())
                .Returns(() => peopleListMock);

            var controller = new PeopleController(
                peopleServiceMock.Object,
                cacheMock.Object);


            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<PeopleIndexViewModel>(viewResult.Model);

            //Assert.Equal(
            //    JsonConvert.SerializeObject(peopleListMock),
            //    JsonConvert.SerializeObject(model.People));
            //Assert.Equal(
            //    JsonConvert.SerializeObject(person1Mock),
            //    JsonConvert.SerializeObject(model.MinPerson));
            //Assert.Equal(
            //    JsonConvert.SerializeObject(person2Mock),
            //    JsonConvert.SerializeObject(model.MaxPerson));
            Assert.Equal(JsonConvert.SerializeObject(new PeopleIndexViewModel
            {
                People = peopleListMock,
                MaxPerson = person2Mock,
                MinPerson = person1Mock
            }), JsonConvert.SerializeObject(model));


        }
    }
}
