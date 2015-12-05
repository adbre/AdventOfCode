using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public static class StringExtensions
    {
        public static IEnumerable<string> ReadLines(this string input)
        {
            using (var stringReader = new StringReader(input))
            {
                string line;
                while ((line = stringReader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        public static IEnumerable<string> ReadLines(this string input, bool skipEmpty)
        {
            return ReadLines(input).Where(line => !skipEmpty || !string.IsNullOrEmpty(line));
        }
    }
}