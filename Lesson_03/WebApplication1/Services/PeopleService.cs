using Domain.Entity;
using Domain.Repository;
using Services.Abstract;
using Services.Abstract.Dto;
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

        public PersonDto CreateNewPerson(PersonDto model)
        {
            var person = new Person
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Birth = model.Birth
            };
            unitOfWork.PeopleRepository.Create(person);
            unitOfWork.SaveChanges();
            return new PersonDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Birth = person.Birth,
                CreatedAt = person.CreatedAt
            };
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

        public PersonDto UpdatePerson(PersonDto model)
        {
            var person = new Person
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Birth = model.Birth,
                // CreatedAt = person.CreatedAt
            };
            unitOfWork.PeopleRepository.Update(person);
            unitOfWork.SaveChanges();

            return new PersonDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Birth = person.Birth,
                CreatedAt = person.CreatedAt
            };
        }
    }
}
