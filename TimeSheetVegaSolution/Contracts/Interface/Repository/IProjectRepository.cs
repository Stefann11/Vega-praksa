using Model.Entities;
using System.Collections.Generic;

namespace Contracts.Interface.Repository
{
    public interface IProjectRepository : IRepository<Project>
    {
        IEnumerable<Project> GetAllByPageAndQuery(string name, int page, string letter);
        public int GetNumberOfPages();
        IEnumerable<Project> GetAllByClient(int clientId);
    }
}
