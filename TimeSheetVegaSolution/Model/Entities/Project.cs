namespace Model.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        public Employee Employee { get; set; }
        public Client Client { get; set; }
        public bool IsRemoved { get; set; }
    }
}
