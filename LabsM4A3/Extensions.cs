using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabsM4A3
{
    static class Extensions
    {
        public static void Out(this double[,] matrix)
        {
            for (int v = 0; v <= matrix.GetUpperBound(0); ++v)
            {
                for (int k = 0; k <= matrix.GetUpperBound(1); ++k)
                {
                    Console.Write(Math.Round(matrix[v, k], 3));
                    Console.Write("\t");
                }
                Console.WriteLine();
            }

            Console.ReadKey(true);
        }

        public static void Out(double[] x, double[] y)
        {
            for (int i = 0; i < y.Length; ++i)
            {
                Console.WriteLine($"{Math.Round(x[i], 3)} : {Math.Round(y[i], 3)}");
            }

            Console.ReadKey(true);
        }
    }
}
