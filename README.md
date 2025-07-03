# WordFinder

This solution finds the top 10 most repeated words from a hard-coded word stream that appear in a square matrix up to 64×64 characters.

## Structure

- **WordFinder.Application**  
  - WordFinder.cs
- **WordFinder.Console**  
  - Program.cs  
- **WordFinder.Domain**  
  - Interfaces/IWordFinder.cs  
  - Entities/Matrix.cs
- **WordFinder.Tests**  
  - WordFinderTests
## How it works

1. Matrix class validates that the input is square and no larger than 64×64.  
2. WordFinder class builds a set of all horizontal and vertical substrings of the matrix.  
3. Calling Find with a list of words will:  
   - remove duplicate words in the input stream  
   - keep only those that appear in the matrix substrings set  
   - order them by frequency in the original stream  
   - return up to 10 words (or an empty list if none match)

## How to run

1. Open a command prompt in the WordFinder.Console folder  
2. Build the project with dotnet build 
3. Run the console app with dotnet run  
4. You will see the list of found words printed to the console
