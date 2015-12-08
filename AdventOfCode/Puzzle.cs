using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    public abstract class Puzzle
    {
        public string ReadAllInput()
        {
            using (var stream = OpenInputStream())
            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }
        public IEnumerable<string> ReadAllInputLines()
        {
            using (var stream = OpenInputStream())
            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                    yield return reader.ReadLine();
            }
        }

        public Stream OpenInputStream()
        {
            var type = GetType();
            var resourceName = $"{type.Namespace}.puzzles.{type.Name.ToLowerInvariant()}-input.txt";

            var stream = type.Assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
                throw new InvalidOperationException("No such resource: " + resourceName);

            return stream;
        }
    }
}