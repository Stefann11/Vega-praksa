using Model.Entities;

namespace Model.Dtos
{
    public class ProjectDto
    {
        public ProjectDto(Project project)
        {
            Id = project.Id;
            Name = project.Name;
            Description = project.Description;
            ProjectStatus = project.ProjectStatus;
            Employee = project.Employee;
            Client = project.Client;
            IsRemoved = project.IsRemoved;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        public Employee Employee { get; set; }
        public Client Client { get; set; }
        public bool IsRemoved { get; set; }
    }
}
