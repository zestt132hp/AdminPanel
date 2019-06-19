using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Data
{
    interface IRepository<T>:IDisposable where T: class
    {
        Task<IEnumerable<T>> GetList();
        Task<T> Get(Guid id);
        Task<bool> Create(T item);
        Task<T> Update(T item);
        Task<bool> Delete(Guid id);
        void Save();
    }
}
