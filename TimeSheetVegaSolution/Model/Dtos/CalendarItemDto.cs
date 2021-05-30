using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Dtos
{
    public class CalendarItemDto
    {
        public DateTime Date { get; set; }
        public float TotalHours { get; set; }
    }
}
