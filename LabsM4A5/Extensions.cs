using System;


namespace LabsM4A5
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

        public static void Out(this double[,] matrix, int? row = null)
        {
            int maxSize = 5;
            foreach(var item in matrix)
            {
                var rounded = Math.Round(item, 2).ToString();
                maxSize = Math.Max(maxSize, rounded.Length);
            }

            if (row != null)
            {
                for (int k = 0; k <= matrix.GetUpperBound(1); ++k)
                {
                    Console.Write(Math.Round(matrix[row.Value, k], 2).AppendTo(5));
                    Console.Write(" ");
                }

                Console.WriteLine();
                Console.WriteLine();
                return;
            }
            
            for (int v = 0; v <= matrix.GetUpperBound(0); ++v)
            {
                for (int k = 0; k <= matrix.GetUpperBound(1); ++k)
                {
                    Console.Write(Math.Round(matrix[v, k], 2).AppendTo(maxSize + 2));
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
