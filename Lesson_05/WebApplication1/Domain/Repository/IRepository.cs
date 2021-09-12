using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IRepository<TKey, TValue>
        where TKey : struct
        where TValue : BaseEntity<TKey>
    {
        IEnumerable<TValue> GetAll();
        IEnumerable<TValue> GetAll(Func<TValue, bool> predicate);
        TValue Get(TKey id);
        void Create(TValue entity);
        void Remove(TKey id);
        void Update(TValue entity);
    }
}
