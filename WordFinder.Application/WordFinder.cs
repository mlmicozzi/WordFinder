using WordFinder.Domain.Entities;
using WordFinder.Domain.Interfaces;

namespace WordFinder.Application
{
    public class WordFinder : IWordFinder
    {
        private readonly HashSet<string> _substrings;

        public WordFinder(string[] matrixLines)
        {
            var matrix = new Matrix(matrixLines);

            _substrings = new HashSet<string>(StringComparer.Ordinal);

            foreach (var row in matrix.Rows)
            {
                AddSubstrings(row);
            }

            for (int c = 0; c < matrix.Rows.Length; c++)
            {
                var col = string.Concat(matrix.Rows.Select(r => r[c]));
                AddSubstrings(col);
            }
        }

        public IEnumerable<string> Find(IEnumerable<string> wordStream)
        {
            if (wordStream == null)
            {
                throw new ArgumentNullException(nameof(wordStream));
            }

            var uniqueWords = new HashSet<string>(wordStream.Where(w => !string.IsNullOrEmpty(w)), StringComparer.Ordinal);

            var matches = uniqueWords.Where(w => _substrings.Contains(w));

            var frequency = wordStream
                .Where(w => !string.IsNullOrEmpty(w))
                .GroupBy(w => w)
                .ToDictionary(g => g.Key, g => g.Count());

            return matches
                .OrderByDescending(w => frequency[w])
                .Take(10);
        }

        private void AddSubstrings(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                for (int len = 1; i + len <= s.Length; len++)
                {
                    _substrings.Add(s.Substring(i, len));
                }
            }
        }
    }
}
