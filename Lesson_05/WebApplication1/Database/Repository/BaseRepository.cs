using Domain.Entity;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Repository
{
    public abstract class BaseRepository<TKey, TValue> : IRepository<TKey, TValue>
        where TKey : struct
        where TValue : BaseEntity<TKey>
    {
        protected readonly ApplicationDbContext db;

        protected DbSet<TValue> Table => db.Set<TValue>();

        public BaseRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public void Create(TValue entity)
        {
            db.Entry(entity).State = EntityState.Added;
        }

        public abstract TValue Get(TKey id);

        public IEnumerable<TValue> GetAll() => Table.ToList();

        public abstract void Remove(TKey id);

        public abstract void Update(TValue entity);

        public IEnumerable<TValue> GetAll(Func<TValue, bool> predicate)
        {
            return Table.Where(predicate).ToList();
        }
    }
}
