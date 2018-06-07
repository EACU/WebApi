using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EACA.Models
{
    public class DailySchedule
    {
        public string Day { get; set; }
        public List<Lesson> Lessons { get; set; } = new List<Lesson>();

        public DailySchedule(int day)
        {
            Day = StaticScheduleInfo.Week[day];
        }

    }
}
