using System;
using System.Linq;


namespace LabM4A2
{
    static class Extensions
    {
        public static void Out(this double[] vector)
        {
            Console.WriteLine(string.Join(", ", vector.Select(x => Math.Round(x, 3)).ToArray()) + "\n");
        }

        public static void Out(this (double[] x, double[] y) result)
        {
            var h = (result.x.Last() - result.x.First()) / (result.x.Length - 1);
            Console.WriteLine($"\nШаг алгоритма: {h}");
            Console.WriteLine("------------------------\n");
            for (int i = 0; i < result.x.Length; ++i)
            {
                var x = Math.Round(result.x[i], 3);
                var y = Math.Round(result.y[i], 3);
                Console.WriteLine($"{x}\t;\t{y}");
            }
        }
    }
}
