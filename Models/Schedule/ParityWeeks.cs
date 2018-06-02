
using System;
using System.Collections.Generic;

namespace EACA.Models
{
    public class ParityWeeks
    {
        public List<string> EvenWeeks { get; set; } = new List<string>();
        public List<string> OddWeeks { get; set; } = new List<string>();

        public CurrentWeek GetEvenOddWeekToday()
        {
            var weekInfo = new CurrentWeek();
            
            var day = DateTime.Today;
            while(day.DayOfWeek != System.DayOfWeek.Monday)
                day = day.AddDays(-1);

            var formatingDate = day.ToString("dd.MM.yyyy");
            
            foreach (var item in EvenWeeks)
                if (item.Contains(formatingDate))
                    CreateCurrentWeek(weekInfo, item, "Чётная неделя");

            foreach (var item in OddWeeks)
                if (item.Contains(formatingDate))
                    CreateCurrentWeek(weekInfo, item, "Нечётная неделя");

            return weekInfo;
        }

        private static void CreateCurrentWeek(CurrentWeek weekInfo, string item, string parity) 
        {
            var temp = item.Split("–"); // [0] - начало недели, [1] - конец недели
            weekInfo.StartWeek = temp[0].Trim();
            weekInfo.EndWeek = temp[1].Trim();
            weekInfo.StatusParity = parity;
        }
    }
}
