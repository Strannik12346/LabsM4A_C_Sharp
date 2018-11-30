using System;


namespace LabsM4A6
{
    static class Extensions
    {
        public static string AppendTo(this double value, int length, bool fromLeft = false)
        {
            var initialStr = double.IsNaN(value)
                ? "NaN"
                : value.ToString();

            var appendedStr = new char[length - initialStr.Length];

            for (int i = 0; i < appendedStr.Length; ++i)
                appendedStr[i] = ' ';

            if (fromLeft)
                return new string(appendedStr) + initialStr;

            return initialStr + new string(appendedStr);
        }

        public static void Out(this double[] row)
        {
            Console.WriteLine();

            int maxSize = 5;
            foreach (var item in row)
            {
                var rounded = Math.Round(item, 3).ToString();
                maxSize = Math.Max(maxSize, rounded.Length);
            }

            Console.Write("\t\t");
            for (int k = 0; k < row.Length; ++k)
            {
                Console.Write(Math.Round(row[k], 2).AppendTo(maxSize + 2));
                Console.Write(" ");
            }

            Console.WriteLine();
        }

        public static void Out(this double[,] matrix)
        {
            int maxSize = 5;
            foreach (var item in matrix)
            {
                var rounded = Math.Round(item, 3).ToString();
                maxSize = Math.Max(maxSize, rounded.Length);
            }

            for (int v = 0; v <= matrix.GetUpperBound(0); ++v)
            {
                for (int k = 0; k <= matrix.GetUpperBound(1); ++k)
                {
                    Console.Write(Math.Round(matrix[v, k], 3).AppendTo(maxSize + 2));
                    Console.Write(" ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
