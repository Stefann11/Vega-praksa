using Model.Entities;
using System.Collections.Generic;

namespace Contracts.Interface.Repository
{
    public interface IClientRepository : IRepository<Client>
    {
        IEnumerable<Client> GetAllByPageAndQuery(string name, int page, string letter);
    }
}
