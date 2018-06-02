
using System.Collections.Generic;
using System.Threading.Tasks;
using EACA.Models;
using Google.Apis.Sheets.v4.Data;

namespace EACA.Controllers.ExcelSchedule
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