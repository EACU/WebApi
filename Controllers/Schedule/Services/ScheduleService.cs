using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EACA_API.Controllers.ExcelSchedule;
using EACA_API.Controllers.ScheduleExtensions;
using EACA_API.Helpers;
using EACA_API.Models.Schedule;

namespace EACA_API.Controllers.ScheduleApi.Services
{
    public class ScheduleService : IScheduleService
    {
        private GroupsList GroupsList { get; set; } = new GroupsList();
        private ParityWeeks ParityWeeks { get; set; } = ExcelApi.GetParityWeeks();

        public ScheduleService()
        {
            GroupsList.Groups = Serializer.Deserialize<Response<GroupSchedule>>("groups_list.json").ListGroups;
        }

        public GroupsList GetGroupsList() => GroupsList;
        public ParityWeeks GetParityWeeks() => ParityWeeks;

        public async Task<Schedule> GetSchedule(int groupId, string parity)
        {
            var listRanges = GenerateListRanges(groupId, parity );
            var schedule = await ExcelApi.GetSchedule(listRanges);

            schedule.GroupId = groupId.ToString();
            schedule.Parity = parity.ParityConverterExtension();

            return schedule;
        }

        private List<string> GenerateListRanges(int groupId, string parity )
        {
            var listRange = new List<string>();

            foreach (var item in StaticScheduleInfo.Week)
                listRange.Add(ExcelDayRange(groupId, parity, item.Key));

            return listRange;
        }

        private string ExcelDayRange(int groupId, string parity, int day)
        {
            int startCell = (day * 9) + 4;
            int endCell = startCell + 6;

            parity = parity.ParityConverterExtension();

            var group = GroupsList.Groups.Where(x => x.GroupId == groupId).FirstOrDefault();

            return $"{group?.Worksheet}({parity})!{group?.Cells}{startCell}:{group?.Cells}{endCell}";
        }
    }
}
