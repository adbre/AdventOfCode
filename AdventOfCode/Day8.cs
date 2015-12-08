using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace AdventOfCode
{
    [TestFixture]
    public class Day8 : Puzzle
    {
        [Test]
        [TestCase("\"\"\r\n\"abc\"\r\n\"aaa\\\"\"aaa\"\r\n\"\\x27\"", ExpectedResult = 12)]
        public int Part1(string input)
        {
            return Part1(input.ReadLines());
        }
        [Test]
        [TestCase("\"\"", ExpectedResult = 6)]
        [TestCase("\"abc\"", ExpectedResult = 9)]
        [TestCase("\"aaa\\\"aaa\"", ExpectedResult = 16)]
        public int EncodedNumberOfCharactersOfCode(string input)
        {
            var sb = new StringBuilder();
            sb.Append('"');
            foreach (var ch in input)
            {
                if (ch == '\\' || ch == '"')
                {
                    sb.Append('\\');
                }

                sb.Append(ch);
            }

            sb.Append('"');
            return sb.Length;
        }

        [Test]
        [TestCase(ExpectedResult = 1333)]
        public int AnswerPart1()
        {
            return Part1(ReadAllInputLines());
        }

        [Test]
        [TestCase(ExpectedResult = 2046)]
        public int AnswerPart2()
        {
            return ReadAllInputLines().Sum(line =>
            {
                return EncodedNumberOfCharactersOfCode(line) - line.Length;
            });
        }

        private int Part1(IEnumerable<string> lines)
        {
            return lines.Sum(Part1Calculation);
        }

        private int Part1Calculation(string line)
        {
            var numberOfCharactersOfCode = line.Length;
            var numberOfCharactersInMemory = Parse(line.Substring(1, line.Length - 2)).Length;
            return numberOfCharactersOfCode - numberOfCharactersInMemory;
        }

        private string Parse(string literal)
        {
            var result = new StringBuilder();
            for (var i = 0; i < literal.Length; i++)
            {
                var ch = literal[i];
                if (ch == '\\')
                {
                    if (i + 3 < literal.Length && literal[i + 1] == 'x' && char.IsLetterOrDigit(literal[i + 2]) && char.IsLetterOrDigit(literal[i + 3]))
                    {
                        result.Append(Encoding.ASCII.GetString(new [] { Convert.ToByte(literal.Substring(i + 2, 2), 16) }));
                        i += 3;
                        continue;
                    }

                    if (i + 1 < literal.Length)
                    {
                        var peek = literal[i + 1];
                        if (peek == '\\')
                            i++;
                        else if (peek == '"')
                        {
                            ch = peek;
                            i++;
                        }
                    }
                }

                result.Append(ch);
            }

            return result.ToString();
        }
    }
}