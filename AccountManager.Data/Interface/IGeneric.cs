using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Data
{
    public interface IGeneric<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Find(Guid id);
        Task Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
