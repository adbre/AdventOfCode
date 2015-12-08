using System.Security.Cryptography;
using System.Text;
using NUnit.Framework;

namespace AdventOfCode
{
    [TestFixture]
    public class Day4
    {
        [Test]
        [TestCase("abcdef", ExpectedResult = 609043)]
        [TestCase("pqrstuv", ExpectedResult = 1048970)]
        [TestCase("bgvyzdsv", ExpectedResult = 254575, TestName = "Answer Part1")]
        public int Part1(string input)
        {
            return LowestPositiveInteger(input, 5);
        }

        [Test]
        [TestCase("bgvyzdsv", ExpectedResult = 1038736, TestName = "Answer Part2")]
        public int Part2(string input)
        {
            return LowestPositiveInteger(input, 6);
        }

        private int LowestPositiveInteger(string input, int leadingZeroes)
        {
            var expectedPrefix = new string('0', leadingZeroes);
            using (var md5 = MD5.Create())
            {
                var lowestPositiveInteger = 0;
                string hash;
                do
                {
                    lowestPositiveInteger++;

                    var bytes = Encoding.ASCII.GetBytes(input + lowestPositiveInteger.ToString());
                    bytes = md5.ComputeHash(bytes);

                    var sb = new StringBuilder();
                    foreach (var b in bytes)
                    {
                        sb.Append(b.ToString("X2"));
                    }

                    hash = sb.ToString();
                } while (!hash.StartsWith(expectedPrefix));

                return lowestPositiveInteger;
            }
        }
    }
}