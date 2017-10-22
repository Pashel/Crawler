namespace Crawler2.Extensions
{
    public static class StringExtansion
    {
        public static int? ToInt(this string str)
        {
            int result;
            if (!int.TryParse(str, out result)) {
                return null;
            }
            return result;
        }
    }
}
