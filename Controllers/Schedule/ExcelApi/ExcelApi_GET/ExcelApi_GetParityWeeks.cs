using EACA_API.Models.Schedule;

namespace EACA_API.Controllers.ExcelSchedule
{
    public static partial class ExcelApi
    {
        public static ParityWeeks GetParityWeeks()
        {
            var request = Service.Spreadsheets.Values.Get(SpreadsheetId, $"Чётность-Нечётность!B3:C14");

            var response = request.Execute().Values;
            var parityWeeks = new ParityWeeks();

            if (response != null && response.Count > 0)
                foreach (var row in response)
                {
                    parityWeeks.OddWeeks.Add(row[0].ToString());
                    parityWeeks.EvenWeeks.Add(row[1].ToString());
                }

            return parityWeeks;
        }
    }
}