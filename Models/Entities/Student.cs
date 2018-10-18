using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EACA_API.Models.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Group { get; set; }

        public string IdentityId { get; set; }
        public ApiUser Identity { get; set; }
    }
}
