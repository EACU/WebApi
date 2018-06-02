
using EACA.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource.UpdateRequest;
using Data = Google.Apis.Sheets.v4.Data;

namespace EACA.Controllers.ExcelSchedule
{
    public static partial class ExcelApi
    {
        public async static Task<string> Update(HelperScheduleChange json)
        {
            var value = new List<object>();
            // value.Add("Пара какая-то");
            // value.Add(" ");
            // value.Add("🍺🍻🍺");
            // value.Add("Практикум Д.С. Перевалов Ауд. 107");
            // value.Add("Основы права А.А. Пронин Ауд. 206");
            // value.Add("12");
            value.AddRange(json.Values);

            var valueRange = new ValueRange();
            valueRange.Values = new List<IList<object>>();
            valueRange.Values.Add(value);
            valueRange.MajorDimension = "COLUMNS";

            var range = $"{json.Worksheets}({json.Parity})!{json.StartCells}";

            var request = Service.Spreadsheets.Values.Update(valueRange, SpreadsheetId, range);
            request.ValueInputOption = ValueInputOptionEnum.RAW;
            request.IncludeValuesInResponse = true;

            var response = await request.ExecuteAsync();

            return JsonConvert.SerializeObject(response);
        }
    }
}