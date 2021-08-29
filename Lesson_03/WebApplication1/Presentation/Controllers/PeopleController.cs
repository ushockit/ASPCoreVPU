using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.People;
using Services.Abstract;
using Services.Abstract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/people")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        IServiceManager serviceManager;

        public PeopleController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet]
        public IEnumerable<PersonDto> GetAllPeople()
        {
            return serviceManager.PeopleService.GetAllPeople();
        }

        [HttpPost]
        public PersonDto Create(CreatePersonModel model)
        {
            return serviceManager.PeopleService.CreateNewPerson(new PersonDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Birth = model.Birth
            });
        }

        [HttpPut]
        public PersonDto Update(UpdatePersonModel model)
        {
            return serviceManager.PeopleService.UpdatePerson(new PersonDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Birth = model.Birth,
                Id = model.Id
            });
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            serviceManager.PeopleService.RemovePersonById(id);
            return new JsonResult("Ok");
        }
    }
}
