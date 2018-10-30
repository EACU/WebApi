using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using EACA_API.Models;
using EACA_API.Controllers.ScheduleApi.Services;

namespace EACA_API.Controllers.ExcelSchedule
{
    [EnableCors("AllowAllOrigin")]
    [Route("api/[controller]")]
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        /// <summary>
        /// Получение текущей недели(её четности и крайних дней)
        /// </summary>
        [HttpGet]
        [Route("getParityToday")]
        public CurrentWeek GetParityToday() => _scheduleService.GetParityWeeks().GetParityWeekToday();

        /// <summary>
        /// Получение всех четных\нечетных недель
        /// </summary>
        [HttpGet]
        [Route("getParityWeeks")]
        public ParityWeeks GetParityWeeks() => _scheduleService.GetParityWeeks();

        /// <summary>
        /// Возвращает список всех групп с привязкой листа и столбца в googlesheets
        /// </summary>
        [HttpGet]
        [Route("getGroupList")]
        public IEnumerable<Group> GetGroupList() => _scheduleService.GetGroupsList().Groups;

        /// <summary>
        /// Возвращает список пар на неделю для группы
        /// </summary>
        /// <param name="groupId">Номер группы</param>
        /// <param name="parity">Четность недели(odd - нечетная, even - чётная)</param>
        [HttpGet]
        [Route("{groupId:int}/{parity}")]
        public async Task<IActionResult> GetWeekScheduleGroup(int groupId, string parity)
        {
            if (parity.ToLower() != "even" && parity.ToLower() != "odd")
                Helpers.Errors.AddErrorToModelState("parity_params", "Некорректная чётность", ModelState);

            if (!_scheduleService.GetGroupsList().Contains(groupId))
                Helpers.Errors.AddErrorToModelState("group_params", "Несуществующая группа", ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var schedule = await _scheduleService.GetSchedule(groupId, parity);

            return Ok(schedule);
        }

        /// <summary>
        /// Возвращает список пар на конкретный день для определённой группы
        /// </summary>
        /// <param name="groupId">Номер группы</param>
        /// <param name="parity">Четность недели(odd - нечетная, even - чётная)</param>
        /// <param name="day">Номер дня(0 - понедельник, 1 - вторник, ..., 6 - суббота)</param>
        [HttpGet]
        [Route("{groupId:int}/{parity}/{day:int}")]
        public async Task<IActionResult> GetDayScheduleGroup(int groupId, string parity, int day)
        {
            if (day < 0 || day > 5)
            {
                Helpers.Errors.AddErrorToModelState("day_params", "Несуществующий день, проверьте корректость дня (от 0(понедельник) до 6(суббота))", ModelState);
            }
            if (parity.ToLower() != "even" && parity.ToLower() != "odd")
            {
                Helpers.Errors.AddErrorToModelState("parity_params", "Некорректная чётность", ModelState);
            }
            if (!_scheduleService.GetGroupsList().Contains(groupId))
            {
                Helpers.Errors.AddErrorToModelState("group_params", "Несуществующая группа", ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
 
            var schedule = await _scheduleService.GetSchedule(groupId, parity);

            return Ok(schedule.WeekSchedule[day]);
        }
    }
}
