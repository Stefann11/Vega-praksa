using System.Collections.Generic;

namespace Contracts.Interface.Service
{
    public interface IService<T, G>
    {
        IEnumerable<G> GetAll();
        G GetById(int id);
        G Save(T obj);
        G Edit(T obj);
        G Delete(T obj);
    }
}
