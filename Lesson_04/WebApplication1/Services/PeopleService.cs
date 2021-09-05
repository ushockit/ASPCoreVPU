using AutoMapper;
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
        readonly IMapper mapper;

        public PeopleService(
            IUnitOfWork uow,
            IMapper mapper)
        {
            unitOfWork = uow;
            this.mapper = mapper;
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
            return mapper.Map<PersonDto>(person);
        }

        public IEnumerable<PersonDto> GetAllPeople()
        {
            var people = unitOfWork.PeopleRepository.GetAll();
            return mapper.Map<IEnumerable<PersonDto>>(people);
        }

        public PersonDto GetPersonByFirstNameAndLastName(string firstName, string lastName)
        {
            var person = unitOfWork.PeopleRepository.GetAll((person)
                => person.FirstName.Equals(firstName) &&
                person.LastName.Equals(lastName)).FirstOrDefault();
            return mapper.Map<PersonDto>(person);
        }

        public PersonDto GetPersonById(Guid id)
        {
            var person = unitOfWork.PeopleRepository.Get(id);
            return mapper.Map<PersonDto>(person);
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

            return mapper.Map<PersonDto>(person);
        }
    }
}
