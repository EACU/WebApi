using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EACA_API.Models.Schedule
{
    public class Schedule
    {
        public string GroupId { get; set; }
        public string Parity { get; set; }
        public string DateQuery { get; set; } = DateTime.Now.ToLocalTime().ToString();

        public List<DailySchedule> WeekSchedule { get ; set; } = new List<DailySchedule>();

        public Schedule()
        {

        }
    }
}
