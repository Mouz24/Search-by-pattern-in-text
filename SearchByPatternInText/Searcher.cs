using System;
using System.Collections.Generic;

#pragma warning disable CA1304
#pragma warning disable S1643
#pragma warning disable S2259

namespace SearchByPatternInText
{
    public static class Searcher
    {
        /// <summary>
        /// Searches the pattern string inside the text and collects information about all hit positions in the order they appear.
        /// </summary>
        /// <param name="text">Source text.</param>
        /// <param name="pattern">Source pattern text.</param>
        /// <param name="overlap">Flag to overlap:
        /// if overlap flag is true collect every position overlapping included,
        /// if false no overlapping is allowed (next search behind).</param>
        /// <returns>List of positions of occurrence of the pattern string in the text, if any and empty otherwise.</returns>
        /// <exception cref="ArgumentException">Thrown if text or pattern is null.</exception>
        public static int[] SearchPatternString(this string text, string pattern, bool overlap)
        {
            if (text is null || pattern is null)
            {
                throw new ArgumentException($"{text},{pattern}");
            }

            string temp1 = null;
            List<int> res = new List<int>();
            if (!overlap)
            {
                for (int i = 0; i < text.Length; i++)
                {
                    if (char.ToUpper(text[i]) == pattern[0] || char.ToLower(text[i]) == pattern[0])
                    {
                        for (int j = i; j < pattern.Length + i && j < text.Length; j++)
                        {
                            temp1 += text[j].ToString();
                        }

                        if (temp1[^1] == pattern[^1])
                        {
                            if (temp1.ToLower() == pattern.ToLower())
                            {
                                res.Add(i + 1);
                            }
                            else
                            {
                                continue;
                            }
                        }

                        temp1 = null;
                    }
                }
            }
            else
            {
                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] == pattern[0])
                    {
                        for (int j = i; j < pattern.Length + i && j < text.Length; j++)
                        {
                            temp1 += text[j].ToString();
                        }

                        if (temp1 == pattern)
                        {
                            res.Add(i + 1);
                        }

                        temp1 = null;
                    }
                }
            }

            return res.ToArray();
        }
    }
}
