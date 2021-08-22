using Domain.Entity;
using Domain.Repository;
using Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PeopleService : IPeopleService
    {
        readonly IUnitOfWork unitOfWork;

        public PeopleService(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

        public void CreateNewPerson(PersonDto person)
        {
            unitOfWork.PeopleRepository.Create(new Person
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Birth = person.Birth
            });
            unitOfWork.SaveChanges();
        }

        public IEnumerable<PersonDto> GetAllPeople()
        {
            var people = unitOfWork.PeopleRepository.GetAll();
            // TODO: AutoMapper
            return people.Select((p) => new PersonDto
            {
                Id = p.Id,
                Birth = p.Birth,
                FirstName = p.FirstName,
                LastName = p.LastName,
                CreatedAt = p.CreatedAt
            }).ToList();
        }

        public PersonDto GetPersonById(Guid id)
        {
            var person = unitOfWork.PeopleRepository.Get(id);
            return new PersonDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Birth = person.Birth,
                CreatedAt = person.CreatedAt
            };
        }

        public void RemovePersonById(Guid id)
        {
            unitOfWork.PeopleRepository.Remove(id);
            unitOfWork.SaveChanges();
        }

        public void UpdatePerson(PersonDto person)
        {
            unitOfWork.PeopleRepository.Update(new Person
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Birth = person.Birth,
                CreatedAt = person.CreatedAt
            });
            unitOfWork.SaveChanges();
        }
    }
}
