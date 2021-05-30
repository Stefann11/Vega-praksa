using Model.Entities;
using System;

namespace Model.Dtos
{
    public class DailyTimeSheetDto
    {
        public DailyTimeSheetDto(DailyTimeSheet dailyTimeSheet)
        {
            Id = dailyTimeSheet.Id;
            Date = dailyTimeSheet.Date;
            Description = dailyTimeSheet.Description;
            Time = dailyTimeSheet.Time;
            Overtime = dailyTimeSheet.Overtime;
            Project = dailyTimeSheet.Project;
            Category = dailyTimeSheet.Category;
            Employee = dailyTimeSheet.Employee;
            IsRemoved = dailyTimeSheet.IsRemoved;
        }

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
