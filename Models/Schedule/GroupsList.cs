using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EACA.Models
{
    public class GroupsList
    {
        public List<Group> Groups { get; set; } = new List<Group>();

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
