using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.People;

namespace WebApplication1.Services
{
    public interface IWebPeopleService
    {
        List<PersonViewModel> GetAllPeople();
        PersonViewModel GetPersonById(Guid id);
        void UpdatePerson(PersonViewModel person);
        void CreateNewPerson(PersonViewModel person);
        void RemovePersonById(Guid id);
    }
}
