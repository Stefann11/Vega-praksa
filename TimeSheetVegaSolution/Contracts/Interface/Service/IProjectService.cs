using Model.Dtos;
using Model.Entities;
using System.Collections.Generic;

namespace Contracts.Interface.Service
{
    public interface IProjectService : IService<Project, ProjectDto>
    {
        IEnumerable<ProjectDto> GetAllByPageAndQuery(string name, int page, string letter);
        public int GetNumberOfPages();
        IEnumerable<ProjectDto> GetAllByClient(int clientId);
    }
}
