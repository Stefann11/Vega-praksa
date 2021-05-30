using Model.Dtos;
using Model.Entities;
using System.Collections.Generic;
using Contracts.Interface.Repository;
using Contracts.Interface.Service;
using System.Linq;

namespace BusinessLogic.Implementation
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public IEnumerable<ProjectDto> GetAll()
        {
            return _projectRepository.GetAll()
                .Select(project => new ProjectDto(project)).ToList();
        }

        public IEnumerable<ProjectDto> GetAllByPageAndQuery(string name, int page, string letter)
        {
            return _projectRepository.GetAllByPageAndQuery(name, page, letter)
                .Select(project => new ProjectDto(project)).ToList();
        }

        public ProjectDto GetById(int id)
        {
            return new ProjectDto(_projectRepository.GetById(id));
        }

        public ProjectDto Save(Project project)
        {
            return new ProjectDto(_projectRepository.Save(project));
        }

        public ProjectDto Edit(Project project)
        {
            return new ProjectDto(_projectRepository.Edit(project));
        }

        public ProjectDto Delete(Project project)
        {
            return new ProjectDto(_projectRepository.Delete(project));
        }
        
        public int GetNumberOfPages()
        {
            return _projectRepository.GetNumberOfPages();
        }

        public IEnumerable<ProjectDto> GetAllByClient(int clientId)
        {
            return _projectRepository.GetAllByClient(clientId)
                .Select(project => new ProjectDto(project)).ToList();
        }
    }
}
