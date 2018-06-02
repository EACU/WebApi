using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EACA.Models
{
    public class DailySchedule
    {
        public string Day { get; set; }
        public List<string> Lessons { get; set; } = new List<string>();

        public DailySchedule(int day)
        {
            Day = Week.week[day];
        }


    }
}
