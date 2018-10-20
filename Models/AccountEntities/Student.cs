namespace EACA_API.Models.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Group { get; set; }
        public bool? Headman { get; set; }

        public string IdentityId { get; set; }
        public ApiUser Identity { get; set; }
    }
}
