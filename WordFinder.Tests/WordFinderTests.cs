using Finder = WordFinder.Application.WordFinder;
using WordFinder.Domain.Entities;
using WordFinder.Domain.Interfaces;

namespace WordFinder.Tests
{
    public class WordFinderTests
    {
        [Fact]
        public void Matrix_NullRows_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Matrix(null));
        }

        [Fact]
        public void Matrix_EmptyRows_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Matrix(new string[0]));
        }

        [Fact]
        public void Matrix_NonSquare_ThrowsArgumentException()
        {
            var rows = new[] { "ABC", "DEF", "G" };
            Assert.Throws<ArgumentException>(() => new Matrix(rows));
        }

        [Fact]
        public void Matrix_TooLarge_ThrowsArgumentException()
        {
            var rows = Enumerable.Repeat("A".PadRight(65, 'A'), 65).ToArray();
            Assert.Throws<ArgumentException>(() => new Matrix(rows));
        }

        [Fact]
        public void Find_NoMatches_ReturnsEmpty()
        {
            var matrix = new[] { "ABC", "DEF", "GHI" };
            IWordFinder finder = new Finder(matrix);
            var result = finder.Find(new[] { "X", "Y", "Z" });
            Assert.Empty(result);
        }

        [Fact]
        public void Find_SingleMatch_ReturnsThatWord()
        {
            var matrix = new[] { "ABC", "DEF", "GHI" };
            IWordFinder finder = new Finder(matrix);
            var result = finder.Find(new[] { "BC", "XY" }).ToList();
            Assert.Single(result);
            Assert.Equal("BC", result[0]);
        }

        [Fact]
        public void Find_RepeatedWords_CountedOnce()
        {
            var matrix = new[] { "ABCD", "EFGH", "IJKL", "MNOP" };
            IWordFinder finder = new Finder(matrix);

            var stream = new[] { "ABC", "ABC", "XYZ" };
            var result = finder.Find(stream).ToList();

            Assert.Single(result);
            Assert.Equal("ABC", result[0]);
        }

        [Fact]
        public void Find_Top10_ByFrequency()
        {
            var matrix = new[] { "ABCD", "EFGH", "IJKL", "MNOP" };
            IWordFinder finder = new Finder(matrix);

            var stream = new[] { "ABC", "ABC", "ABC", "JKL", "JKL", "EFG", "XYZ" };
            var result = finder.Find(stream).ToList();

            Assert.Equal(3, result.Count);
            Assert.Equal("ABC", result[0]);
            Assert.Equal("JKL", result[1]);
            Assert.Equal("EFG", result[2]);
        }

        [Fact]
        public void Find_MoreThan10_UsesTop10()
        {
            var matrix = new[]
            {
                "ABCDEFGHIJ",
                "KLMNOPQRST",
                "UVWXYZABCD",
                "EFGHIJKLMN",
                "OPQRSTUVWX",
                "YZABCDEFGH",
                "IJKLMNOPQR",
                "STUVWXYZAB",
                "CDEFGHIJKL",
                "MNOPQRSTUV"
            };
            IWordFinder finder = new Finder(matrix);

            var valid = new List<string>();
            for (int i = 0; i < 12; i++)
                valid.Add(((char)('A' + i)).ToString());

            var stream = valid.SelectMany((w, i) => Enumerable.Repeat(w, i + 1));
            var result = finder.Find(stream).ToList();

            Assert.Equal(10, result.Count);

            Assert.DoesNotContain("A", result);
            Assert.DoesNotContain("B", result);
        }

        [Fact]
        public void Find_NullStream_ThrowsArgumentNull()
        {
            var matrix = new[] { "ABC", "DEF", "GHI" };
            IWordFinder finder = new Finder(matrix);
            Assert.Throws<ArgumentNullException>(() => finder.Find(null));
        }
    }
}