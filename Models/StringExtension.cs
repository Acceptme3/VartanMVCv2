namespace VartanMVCv2.Models
{
    public static class StringExtension
    {
        public static List<string> SplitString(this string str, string input, string separator)
        {
            string[] substrings = input.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < substrings.Length; i++)
            {
                substrings[i] = substrings[i].Replace(" ", "");
            }
            List<string> result = substrings.ToList<string>();
            return result;
        }
    }
}
