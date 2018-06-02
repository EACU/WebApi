using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EACA.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace EACA.Controllers.ExcelSchedule
{
    [EnableCors("AllowAllOrigin")]
    [Route("api/[controller]")]
    public partial class ScheduleController : Controller
    {
        private GroupsList GroupsList { get; set; } = new GroupsList();
        private ParityWeeks ParityWeeks { get; set; } = ExcelApi.GetParityWeeks();

        public ScheduleController()
        {
            GroupsList.Groups = Serializer.Deserialize<Response<Group>>("groups_list.json").ListGroups; // Десериализуем список групп 
        }

        #region GET Routers

        [Route("getParityToday")]
        public CurrentWeek GetParityToday() => ParityWeeks.GetEvenOddWeekToday();

        [Route("getParityWeeks")]
        public ParityWeeks GetParityWeeks() => ParityWeeks;

        [Route("getGroupList")]
        public IEnumerable<Group> GetGroupList() => GroupsList.Groups;


        [Route("{parity}/{groupId:int}")]
        public async Task<Schedule> GetWeekScheduleGroup(string parity, int groupId)
        {
            var schedule = await GetSchedule(parity, groupId);

            return schedule;
        }

        [Route("{parity}/{groupId:int}/{day:int}")]
        public async Task<DailySchedule> GetDayScheduleGroup(string parity, int groupId, int day)
        {
            var schedule = await GetSchedule(parity, groupId);

            return schedule.WeekSchedule[day];
        }
        
        #endregion

        #region POST Routers
        [HttpPost]
        [Route("postUpdate")]
        public async Task<IActionResult> PostUpdate([FromBody]HelperScheduleChange json)
        {
            var cellsGroup = GroupsList.GetCellsOfGroup(json.GroupId);
            var numberCellsGroup = ((json.Day * 9) + 4).ToString();
            json.StartCells = cellsGroup + numberCellsGroup;

            json.Parity = json.Parity.ParityConvert();

            json.Worksheets = GroupsList.Groups.Where(x => x.GroupId == json.GroupId).FirstOrDefault().Worksheet;

            return Ok(await ExcelApi.Update(json));
        }
        #endregion
        
        private async Task<Schedule> GetSchedule(string parity, int groupId)
        {
            var listRanges = GenerateListRanges(parity, groupId);
            var schedule = await ExcelApi.GetSchedule(listRanges);
            schedule.GroupId = groupId.ToString();
            schedule.Parity = parity.ParityConvert();
            return schedule;
        }
        private List<string> GenerateListRanges(string parity, int groupId)
        {
            var listRange = new List<string>();
            foreach (var item in Week.week)
            {
                listRange.Add(GetDayRange(parity, groupId, item.Key));
            }
            return listRange;
        }

        private string GetDayRange(string parity, int groupId, int day)
        {
            int startCell = (day * 9) + 4;
            int endCell = startCell + 6;
            
            parity = parity.ParityConvert();

            var group = GroupsList.Groups.Where(x => x.GroupId == groupId).FirstOrDefault();

            return $"{group?.Worksheet}({parity})!{group?.Cells}{startCell}:{group?.Cells}{endCell}";
        }
    }
}
