using System;

namespace Model.Entities
{
    public class DailyTimeSheet
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public float Time { get; set; }
        public float Overtime { get; set; }
        public Project Project { get; set; }
        public Category Category { get; set; }
        public Employee Employee { get; set; }
        public bool IsRemoved { get; set; }
    }
}
