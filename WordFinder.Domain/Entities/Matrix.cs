namespace WordFinder.Domain.Entities
{
    public class Matrix
    {
        public string[] Rows { get; }

        public Matrix(string[] rows)
        {
            if (rows == null || rows.Length == 0)
            {
                throw new ArgumentException("Matrix must contain at least one row.");
            }

            if (rows.Length > 64)
            {
                throw new ArgumentException("Maximum matrix size is 64×64.");
            }

            if (rows.Any(r => r.Length != rows.Length))
            {
                throw new ArgumentException("Matrix must be square (rows equal columns).");
            }

            Rows = rows;
        }
    }
}
