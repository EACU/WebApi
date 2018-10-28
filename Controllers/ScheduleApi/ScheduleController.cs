using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using EACA_API.Models;
using EACA_API.Services;

namespace EACA_API.Controllers.ExcelSchedule
{
    [EnableCors("AllowAllOrigin")]
    [Route("api/[controller]")]
    public partial class ScheduleController : Controller
    {
        private GroupsList GroupsList { get; set; } = new GroupsList();
        private ParityWeeks ParityWeeks { get; set; } = ExcelApi.GetParityWeeks();

        public ScheduleController()
        {
            GroupsList.Groups = Serializer.Deserialize<Response<Group>>("groups_list.json").ListGroups;
        }

        #region GET Routers

        /// <summary>
        /// Получение текущей недели(её четности и крайних дней)
        /// </summary>
        [HttpGet]
        [Route("getParityToday")]
        public CurrentWeek GetParityToday() => ParityWeeks.GetParityWeekToday();

        /// <summary>
        /// Получение всех четных\нечетных недель
        /// </summary>
        [HttpGet]
        [Route("getParityWeeks")]
        public ParityWeeks GetParityWeeks() => ParityWeeks;

        /// <summary>
        /// Возвращает список всех групп с привязкой листа и столбца в googlesheets
        /// </summary>
        [HttpGet]
        [Route("getGroupList")]
        public IEnumerable<Group> GetGroupList() => GroupsList.Groups;

        /// <summary>
        /// Возвращает список пар на неделю для группы
        /// </summary>
        /// <param name="parity">Четность недели(odd - нечетная, even - чётная)</param>
        /// <param name="groupId">Номер группы</param>
        [HttpGet]
        [Route("{parity}/{groupId:int}")]
        public async Task<IActionResult> GetWeekScheduleGroup(string parity, int groupId)
        {
            if (parity.ToLower() != "even" && parity.ToLower() != "odd")
                ModelState.AddModelError("parity param", "Некорректная чётность");

            if (!GroupsList.Contains(groupId))
                ModelState.AddModelError("group param", "Несуществующая группа");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var schedule = await GetSchedule(parity, groupId);

            return Ok(schedule);
        }

        /// <summary>
        /// Возвращает список пар на конкретный день для определённой группы
        /// </summary>
        /// <param name="parity">Четность недели(odd - нечетная, even - чётная)</param>
        /// <param name="groupId">Номер группы</param>
        /// <param name="day">Номер дня(0 - понедельник, 1 - вторник, ..., 6 - суббота)</param>
        [HttpGet]
        [Route("{parity}/{groupId:int}/{day:int}")]
        public async Task<IActionResult> GetDayScheduleGroup(string parity, int groupId, int day)
        {
            if (day < 0 || day > 5)
            {
                ModelState.AddModelError("day param", "Несуществующий день, проверьте корректость дня (от 0(понедельник) до 6(суббота))");
            }
            if (parity.ToLower() != "even" && parity.ToLower() != "odd")
            {
                ModelState.AddModelError("parity param", "Некорректная чётность");
            }
            if (!GroupsList.Contains(groupId))
            {
                ModelState.AddModelError("group param", "Несуществующая группа");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
 
            var schedule = await GetSchedule(parity, groupId);

            return Ok(schedule.WeekSchedule[day]);
        }

        #endregion

        #region POST Routers
        [ApiExplorerSettings(IgnoreApi = true)]
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

            foreach (var item in StaticScheduleInfo.Week)
                listRange.Add(GetDayRange(parity, groupId, item.Key));

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
