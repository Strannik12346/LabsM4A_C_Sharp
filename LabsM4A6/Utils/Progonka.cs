using System;
using System.Linq;


namespace LabsM4A4
{
    static class Progonka
    {
        #region Diagonals

        public static double[] UpperDiagonal(double[] diagonal) =>
            new double[] { double.NaN }.Concat(diagonal).ToArray();

        public static double[] MainDiagonal(double[] diagonal) =>
            new double[] { double.NaN }.Concat(diagonal).ToArray();

        public static double[] LowerDiagonal(double[] diagonal) =>
            new double[] { double.NaN, double.NaN }.Concat(diagonal).ToArray();

        public static double[] FreeVector(double[] vector) =>
            new double[] { double.NaN }.Concat(vector).ToArray();

        private static double[] Result(double[] result) =>
            result.Where((element, index) => index > 0).ToArray();

        #endregion 

        #region Solution

        private static (double[], double[]) DirectPass(
            double[] upperDiagonal,
            double[] mainDiagonal,
            double[] lowerDiagonal,
            double[] vector
        )
        {
            var n = mainDiagonal.Length - 1;

            var alpha = new double[n + 1];
            var beta = new double[n + 1];

            alpha[2] = -upperDiagonal[1] / mainDiagonal[1];
            beta[2] = vector[1] / mainDiagonal[1];

            for (int i = 2; i < n; ++i)
            {
                alpha[i + 1] = (-upperDiagonal[i]) / (mainDiagonal[i] + lowerDiagonal[i] * alpha[i]);
                beta[i + 1] = (-lowerDiagonal[i] * beta[i] + vector[i]) / (mainDiagonal[i] + lowerDiagonal[i] * alpha[i]);
            }

            return (alpha, beta);
        }

        private static double[] InversePass(
            double[] upperDiagonal,
            double[] mainDiagonal,
            double[] lowerDiagonal,
            double[] vector,
            double[] alpha,
            double[] beta
        )
        {
            var n = mainDiagonal.Length - 1;
            var result = new double[n + 1];

            result[n] = (-lowerDiagonal[n] * beta[n] + vector[n]) / (mainDiagonal[n] + lowerDiagonal[n] * alpha[n]);

            for (int i = n - 1; i > 0; --i)
            {
                result[i] = alpha[i + 1] * result[i + 1] + beta[i + 1];
            }

            return result;
        }

        public static double[] SolveSystem(
            double[] upperDiagonal,
            double[] mainDiagonal,
            double[] lowerDiagonal,
            double[] vector
        )
        {
            (double[] alpha, double[] beta) = DirectPass(upperDiagonal, mainDiagonal, lowerDiagonal, vector);
            return Result(InversePass(upperDiagonal, mainDiagonal, lowerDiagonal, vector, alpha, beta));
        }

        #endregion

        #region Tests

        public static void Test()
        {
            double[] upper = new double[] { -1, -1, 2, -4 };
            double[] main = new double[] { 2, 8, 12, 18, 10 };
            double[] lower = new double[] { -3, -5, -6, -5 };
            double[] vector = new double[] { -25, 72, -69, -156, 20 };

            var result = SolveSystem(
                UpperDiagonal(upper),
                MainDiagonal(main),
                LowerDiagonal(lower),
                FreeVector(vector)
            );

            var trueResult = new double[] { -10, 5, -2, -10, -3 };

            if (!trueResult.Zip(result, (x, trueX) => Math.Abs(x - trueX) < 0.0001).All(val => val))
            {
                throw new InvalidOperationException();
            }

            Console.WriteLine("System solution test passed . . . Ok!");
        }

        #endregion
    }
}
