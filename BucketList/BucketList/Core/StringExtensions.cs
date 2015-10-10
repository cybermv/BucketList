namespace BucketList.Core
{
    using System.Collections.Generic;
    using System.Text;

    public static class StringExtensions
    {
        public static string[] SafeSplit(this string current, char separator)
        {
            List<string> parts = new List<string>();

            StringBuilder str = new StringBuilder();
            char doubleQuote = '"';
            bool skipWhitespace = false;

            for (int i = 0; i < current.Length + 1; i++)
            {
                if (i == current.Length ||
                    (current[i] == separator && !skipWhitespace))
                {
                    string part = str.ToString().Trim();
                    str.Clear();
                    if (part != "")
                    {
                        parts.Add(part);
                    }

                    if (i == current.Length)
                    {
                        break;
                    }
                }
                else
                {
                    str.Append(current[i]);

                    if (current[i] == doubleQuote)
                    {
                        skipWhitespace = !skipWhitespace;
                    }
                }
            }

            return parts.ToArray();
        }
    }
}