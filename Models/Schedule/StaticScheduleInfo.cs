using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EACA.Models
{
    public static class StaticScheduleInfo
    {
        public static Dictionary<int, string> Week = new Dictionary<int, string>()
        {
            [0]= "Понедельник",
            [1]= "Вторник",
            [2]= "Среда",
            [3]= "Четверг",
            [4]= "Пятница",
            [5]= "Суббота"
        };
        public static List<string> TimeLessons { get; set; } = new List<string>()
        {
            "9:00 - 10:35",
            "10:45 - 12:20",
            "13:00 - 14:35",
            "14:45 - 16:20",
            "16:30 - 18:05",
            "18:15 - 19:50",
        };
    }
}
