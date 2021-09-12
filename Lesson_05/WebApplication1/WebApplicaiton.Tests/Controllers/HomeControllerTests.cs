using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models.Home;
using WebApplication1.Controllers;
using WebApplication1.Utils;
using Xunit;

namespace WebApplicaiton.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexGetViewResultWithMessage()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HomeController>>();
            var localizerMock = new Mock<IStringLocalizer>();
            var appUtilsMock = new Mock<AppUtils>();

            var controller = new HomeController(
                loggerMock.Object,
                localizerMock.Object,
                appUtilsMock.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<HomeIndexViewModel>(viewResult.Model);
            Assert.Equal("Hello world", model.Message);
        }
    }
}
