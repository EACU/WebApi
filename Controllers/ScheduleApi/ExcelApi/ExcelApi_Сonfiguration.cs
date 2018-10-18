using System.IO;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;


namespace EACA_API.Controllers.ExcelSchedule
{
    public static partial class ExcelApi
    {
        static string ApplicationName = "Schedule EACA";
        static string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static GoogleCredential Credential { get; set; } = CreateCredential();
        static SheetsService Service { get; set; } = CreateService(Credential);
        static string SpreadsheetId { get; set; } = "1bcSQ__8xK1C4HF6qW_gqVbYFRyy7dTVNm6jT-9Z_ZFc";

        private static GoogleCredential CreateCredential()
        {
            GoogleCredential credential;

            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);

            return credential;
        }

        private static SheetsService CreateService(GoogleCredential credential)
        {
            return new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }


    }
}
