using System.Collections.Generic;

namespace Contracts.Interface.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        T Save(T obj);
        T Edit(T obj);
        T Delete(T obj);
    }
}
