using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EACA_API.Models.Schedule
{
    public class GroupsList
    {
        public List<GroupSchedule> Groups { get; set; } = new List<GroupSchedule>();

        public bool Contains(int groupId)
        {
            return Groups.Select(x => x.GroupId).Contains(groupId);
        }
        public string GetCellsOfGroup(int groupId)
        {
            var result = "";
            foreach (var group in Groups)
            {
                if (group.GroupId == groupId)
                    result = group.Cells;
            }
            return result;
        }
    }
}
