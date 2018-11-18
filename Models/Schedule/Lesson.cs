namespace EACA_API.Models.Schedule
{
    public class Lesson
    {
        public string Time { get; set; }
        public string LessonName { get; set; }

        public Lesson(string time, string lessonName)
        {
            Time = time;
            LessonName = lessonName;
        }
    }
}