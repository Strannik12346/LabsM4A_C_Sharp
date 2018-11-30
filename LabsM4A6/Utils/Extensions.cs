using System;


namespace LabsM4A4
{
    static class Extensions
    {
        public static string AppendTo(this object value, int length, bool fromLeft = false)
        {
            var initialStr = value.ToString();
            var appendedStr = new char[length - initialStr.Length];

            for (int i = 0; i < appendedStr.Length; ++i)
                appendedStr[i] = ' ';

            if (fromLeft)
                return new string(appendedStr) + initialStr;

            return initialStr + new string(appendedStr);
        }

        public static void Out(this double[,] matrix, int? row = null)
        {
            if (row != null)
            {
                for (int k = 0; k <= matrix.GetUpperBound(1); ++k)
                {
                    Console.Write(Math.Round(matrix[row.Value, k], 2).AppendTo(5));
                    Console.Write(" ");
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.ReadKey(true);
                return;
            }
            
            for (int v = 0; v <= matrix.GetUpperBound(0); ++v)
            {
                for (int k = 0; k <= matrix.GetUpperBound(1); ++k)
                {
                    Console.Write(Math.Round(matrix[v, k], 2).AppendTo(5));
                    Console.Write(" ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            Console.ReadKey(true);
        }
    }
}
