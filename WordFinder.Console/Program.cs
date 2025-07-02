using Finder = WordFinder.Application.WordFinder;

class Program
{
    static void Main(string[] args)
    {
        var matrix = new[]
        {
                "abcdc",
                "fgwio",
                "chill",
                "pqnsd",
                "uvdxy"
        };

        var wordStream = new List<string>
        {
            "cold",
            "wind",
            "snow",
            "chill"
        };

        var finder = new Finder(matrix);
        var result = finder.Find(wordStream);

        if (!result.Any())
        {
            Console.WriteLine("No words found.");
        }
        else
        {
            Console.WriteLine("Top 10 found words:");
            foreach (var word in result)
            {
                Console.WriteLine(word);
            }
        }
    }
}