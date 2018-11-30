using System;

namespace LabsM4A
{
    static class Extensions
    {
        const int Rounding = 3;

        public static void Out(this double[] vector)
        {
            foreach(var item in vector)
            {
                var rounded = Math.Round(item, Rounding);
                Console.Write($"{rounded};  ");
            }
            Console.WriteLine();
        }

        public static void Out(this double[] vector, string message)
        {
            Console.WriteLine(message);
            vector.Out();
        }

        public static void OutForDesmos(this double[] result)
        {
            string output = "";
            for (int i = 0; i < result.Length; ++i)
            {
                var rounded = Math.Round(result[i], Rounding);
                output += $"({rounded}) \\cdot {i.Phi()}+";
            }
            Console.WriteLine(output.Replace(',', '.'));
        }

        public static void OutForDesmos(this double[] result, string message)
        {
            Console.WriteLine(message);
            result.OutForDesmos();
        }

        public static void Out(this (double[,] matrix, double[] vector) system)
        {
            Console.WriteLine("System: ");
            for (int eq = 0; eq < system.vector.Length; ++eq)
            {
                double rounded;
                for (int i = 0; i < system.vector.Length; ++i)
                {
                    rounded = Math.Round(system.matrix[eq, i], Rounding);
                    Console.Write($"{rounded}\t\t");
                }
                rounded = Math.Round(system.vector[eq], Rounding);
                Console.Write($"|\t\t{rounded}\n\n");
            }
        }

        public static string Phi(this int i)
        {
            return $"x^{i} \\cdot (1 - x^2)";
        }
    }
}