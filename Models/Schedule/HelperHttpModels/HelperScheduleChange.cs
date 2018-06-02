using System;
using System.Collections.Generic;

namespace EACA.Models
{
    public class HelperScheduleChange
    {
        public int GroupId { get; set; }
        public string Parity { get; set; }
        public List<string> Values { get; set; } = new List<string>();
        public int Day { get; set; }
        public string StartCells { get; set; }
        public string Worksheets { get; set; }
    }
}
