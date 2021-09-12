using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Repository
{
    public class PeopleRepository : BaseRepository<Guid, Person>
    {
        public PeopleRepository(ApplicationDbContext context)
            : base(context)
        {

        }

        public override Person Get(Guid id)
            => Table.FirstOrDefault(person => person.Id == id);

        public override void Remove(Guid id)
        {
            var person = Get(id);
            db.Entry(person).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public override void Update(Person entity)
        {
            var person = Get(entity.Id);

            person.LastName = entity.LastName;
            person.FirstName = entity.FirstName;
            person.Birth = entity.Birth;

            db.Entry(person).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
