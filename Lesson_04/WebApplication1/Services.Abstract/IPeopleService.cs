using Services.Abstract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IPeopleService
    {
        IEnumerable<PersonDto> GetAllPeople();
        PersonDto GetPersonById(Guid id);
        PersonDto UpdatePerson(PersonDto person);
        PersonDto CreateNewPerson(PersonDto person);
        void RemovePersonById(Guid id);
        PersonDto GetPersonByFirstNameAndLastName(string firstName, string lastName);
    }
}
