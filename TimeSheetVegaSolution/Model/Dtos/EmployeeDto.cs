using Model.Entities;

namespace Model.Dtos
{
    public class EmployeeDto
    {
        public EmployeeDto(Employee employee)
        {
            Id = employee.Id;
            Name = employee.Name;
            Username = employee.Username;
            Email = employee.Email;
            Password = employee.Password;
            HoursPerWeek = employee.HoursPerWeek;
            EmployeeStatus = employee.EmployeeStatus;
            Role = employee.Role;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public float HoursPerWeek { get; set; }
        public EmployeeStatus EmployeeStatus { get; set; }
        public Role Role { get; set; }
    }
}
