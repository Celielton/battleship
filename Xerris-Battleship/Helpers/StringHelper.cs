using System.Text.RegularExpressions;

namespace Xerris_Battleship.Helpers
{
    public static class StringHelper
    {
        public static bool IsValidPosition(this string value)
        {
            var regex = new Regex(@"^([A-Ha-h]+[1-7]+\-?){3}$");
            return !string.IsNullOrEmpty(value) && regex.IsMatch(value);
        }

        public static bool IsValiShot(this string value)
        {
            var regex = new Regex(@"^[A-Ha-h]{1}[1-7]{1}$");
            return !string.IsNullOrEmpty(value) && regex.IsMatch(value);
        }
    }
}
