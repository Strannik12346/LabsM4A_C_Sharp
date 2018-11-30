using System;

namespace LabsM4A
{
    static class MathHelper
    {
        public const double h = 0.0001;

        public const int Rounding = 9;

        public static double Integrate(Func<double, double> function, double left, double right)
        {
            double result = 0;

            while (left + h <= right)
            {
                result += h / 2 * (function(left) + function(left + h));
                left += h;
            }

            return Math.Round(result, Rounding);
        }

        public static void MakeTriangle(this (double[,] matrix, double[] vector) system)
        {
            for (int eqFirst = 0; eqFirst < system.vector.Length; ++eqFirst) // Цикл по отнимаемым
            {
                for (int eqSecond = eqFirst + 1; eqSecond < system.vector.Length; ++eqSecond) // Цикл по уменьшаемым
                {
                    var multiplier = system.matrix[eqSecond, eqFirst] / system.matrix[eqFirst, eqFirst];

                    for (int i = eqFirst; i < system.vector.Length; ++i) // Цикл по a[i]
                    {
                        system.matrix[eqSecond, i] -= system.matrix[eqFirst, i] * multiplier;
                    }

                    system.vector[eqSecond] -= system.vector[eqFirst] * multiplier;
                }
            }
        }

        public static void CleanTriangle(this (double[,] matrix, double[] vector) system)
        {
            for (int i = 0; i < system.vector.Length; ++i)
            {
                system.vector[i] = Math.Round(system.vector[i], Rounding);
                for (int j = 0; j < system.vector.Length; ++j)
                {
                    system.matrix[i, j] = Math.Round(system.matrix[i, j], Rounding);
                }
            }
        }

        public static double[] SolveTriangle(this (double[,] matrix, double[] vector) system)
        {
            var result = new double[system.vector.Length];

            for (int eq = system.vector.Length - 1; eq >= 0; --eq) // Цикл по уравнениям
            {
                result[eq] = system.vector[eq];
                for (int i = system.vector.Length - 1; i > eq; --i) // Цикл по отнимаемым значениям
                {
                    result[eq] -= system.matrix[eq, i] * result[i];
                }
                result[eq] /= system.matrix[eq, eq];
            }

            return result;
        }
    }
}
