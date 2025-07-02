namespace WordFinder.Domain.Entities
{
    public class WordMatch
    {
        public string Word { get; }
        public int Row { get; }
        public int Col { get; }
        public bool IsVertical { get; }

        public WordMatch(string word, int row, int col, bool isVertical)
        {
            Word = word;
            Row = row;
            Col = col;
            IsVertical = isVertical;
        }
    }
}
