using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EACA_API.Models
{
    public class GroupsList
    {
        public List<Group> Groups { get; set; } = new List<Group>();

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
