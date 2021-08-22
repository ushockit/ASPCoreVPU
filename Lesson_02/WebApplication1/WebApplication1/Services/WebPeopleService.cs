using Services;
using Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.People;

namespace WebApplication1.Services
{
    public class WebPeopleService : IWebPeopleService
    {
        readonly IServiceManager serviceManager;
        public WebPeopleService(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        public void CreateNewPerson(PersonViewModel person)
        {
            serviceManager.PeopleService.CreateNewPerson(new PersonDto
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Birth = person.Birth
            });
        }

        public List<PersonViewModel> GetAllPeople()
        {
            var people = serviceManager.PeopleService.GetAllPeople();
            return people.Select((p) => new PersonViewModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Birth = p.Birth
            }).ToList();
        }

        public PersonViewModel GetPersonById(Guid id)
        {
            var person = serviceManager.PeopleService.GetPersonById(id);
            return new PersonViewModel
            {
                Id = person.Id,
                Birth = person.Birth,
                FirstName = person.FirstName,
                LastName = person.LastName
            };
        }

        public void RemovePersonById(Guid id)
        {
            serviceManager.PeopleService.RemovePersonById(id);
        }

        public void UpdatePerson(PersonViewModel person)
        {
            serviceManager.PeopleService.UpdatePerson(new PersonDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Birth = person.Birth
            });
        }
    }
}
