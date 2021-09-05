using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Features.Commands.CreatePerson;
using Presentation.Features.Queries.GetAllPeople;
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
    [Authorize]
    public class PeopleController : ControllerBase
    {
        readonly IServiceManager serviceManager;
        readonly IMediator mediator;

        public PeopleController(
            IServiceManager serviceManager,
            IMediator mediator)
        {
            this.serviceManager = serviceManager;
            this.mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<List<GetAllPeopleQueryResponse>> GetAllPeople()
        {
            return await mediator.Send(new GetAllPeopleQueryRequest());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<CreatePersonCommandResponse> Create([FromBody] CreatePersonCommandRequest request)
        {
            return await mediator.Send(request);
            //return serviceManager.PeopleService.CreateNewPerson(new PersonDto
            //{
            //    FirstName = model.FirstName,
            //    LastName = model.LastName,
            //    Birth = model.Birth
            //});
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
