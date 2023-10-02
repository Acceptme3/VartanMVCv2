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

        public static string RemoveSubstring(this string str, string inputString, string substring)
        {
            // Проверка на пустую строку или пустой фрагмент
            if (string.IsNullOrEmpty(inputString) || string.IsNullOrEmpty(substring))
            {
                return inputString;
            }

            // Удаление фрагмента из строки
            string result = inputString.Replace(substring, string.Empty);

            return result;
        }
    }


}
