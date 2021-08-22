using Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IPeopleService
    {
        IEnumerable<PersonDto> GetAllPeople();
        PersonDto GetPersonById(Guid id);
        void UpdatePerson(PersonDto person);
        void CreateNewPerson(PersonDto person);
        void RemovePersonById(Guid id);
    }
}
