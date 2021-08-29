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
        TValue Get(TKey id);
        void Create(TValue entity);
        void Remove(TKey id);
        void Update(TValue entity);
    }
}
