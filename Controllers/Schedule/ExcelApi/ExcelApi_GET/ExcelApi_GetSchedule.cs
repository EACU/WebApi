using System.Collections.Generic;
using System.Threading.Tasks;

using EACA_API.Models.Schedule;

namespace EACA_API.Controllers.ExcelSchedule
{
    public static partial class ExcelApi
    {
        public static async Task<Schedule> GetSchedule(List<string> range)
        {
            var request = Service.Spreadsheets.Values.BatchGet(SpreadsheetId);
            request.Ranges = range;

            var response = await request.ExecuteAsync();
            var schedule = ConversionToSchedule(response);

            return schedule;
        }
    }
}