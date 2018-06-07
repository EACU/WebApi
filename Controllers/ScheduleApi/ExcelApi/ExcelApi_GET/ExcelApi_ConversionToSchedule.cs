
using System.Collections.Generic;
using System.Threading.Tasks;
using EACA.Models;
using Google.Apis.Sheets.v4.Data;

namespace EACA.Controllers.ExcelSchedule
{
    public static partial class ExcelApi
    {
        private static Schedule ConversionToSchedule(BatchGetValuesResponse response)
        {
            IList<ValueRange> values = response.ValueRanges;

            var temp = new Schedule();

            int j = 0;
            foreach (var row in values)
            {
                temp.WeekSchedule.Add(new DailySchedule(j));
                for (int i = 0; i < row.Values.Count; i++)
                {
                    if (row.Values[i].Count != 0)
                        temp.WeekSchedule[j].Lessons.Add(new Lesson(StaticScheduleInfo.TimeLessons[i], row.Values[i][0].ToString()));
                    else
                        temp.WeekSchedule[j].Lessons.Add(new Lesson(StaticScheduleInfo.TimeLessons[i], " "));
                }
                j++;
            }
            return temp;
        }
    }
}