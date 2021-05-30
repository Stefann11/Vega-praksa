namespace Model.Entities
{
    public class Employee
    {
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
