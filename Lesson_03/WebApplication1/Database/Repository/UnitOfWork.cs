using Domain.Entity;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly ApplicationDbContext db;

        IRepository<Guid, Person> _peopleRepository;
        IRepository<Guid, Person> IUnitOfWork.PeopleRepository 
            => _peopleRepository ?? (_peopleRepository = new PeopleRepository(db));

        public UnitOfWork(ApplicationDbContext context)
        {
            db = context;
        }

        public void SaveChanges() => db.SaveChanges();
    }
}
