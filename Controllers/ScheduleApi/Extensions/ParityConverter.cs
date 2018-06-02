namespace EACA.Controllers
{
    public static class ParityConverter
    {
        public static string ParityConvert(this string parity)
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