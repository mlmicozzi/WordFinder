namespace WordFinder.Domain.Interfaces
{
    public interface IWordFinder
    {
        IEnumerable<string> Find(IEnumerable<string> wordStream);
    }
}
