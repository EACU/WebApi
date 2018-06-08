using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EACA.Models.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Group { get; set; }

        public string IdentityId { get; set; }
        public AppUser Identity { get; set; }
    }
}
