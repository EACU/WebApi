namespace EACA_API.Controllers.ScheduleExtensions
{
    public static class ParityConverterExtensions
    {
        public static string ParityConverterExtension(this string parity)
        {
            parity = parity.ToLower();
            switch (parity)
            {
                case "even":
                    parity = "Чётная";
                    break;
                case "odd":
                    parity = "Нечётная";
                    break;
            }
            return parity;
        }
    }
}