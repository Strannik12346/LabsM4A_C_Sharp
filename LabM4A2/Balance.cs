using System;


namespace LabM4A2
{
    static class Balance
    {
        #region Polynom

        public static Func<double, double> Q(double[] x, double[] y, int i)
        {
            var n = x.Length;

            return delegate (double arg)
            {
                var result = 1.0;
                for (int j = 0; j < n; ++j)
                {
                    if (j == i) continue;

                    result *= ((arg - x[j]) / (x[i] - x[j]));
                }

                return result;
            };
        }

        public static Func<double, double> Interpolate(double[] x, double[] y)
        {
            var n = x.Length;

            return delegate (double arg)
            {
                var result = 0.0;

                for (int i = 0; i < n; ++i)
                {
                    var Qi = Q(x, y, i);
                    result += y[i] * Qi(arg);
                }

                return result;
            };
        }

        #endregion

        #region Tests

        public static void Test()
        {
            var x = new double[] { 1, 2, 3, 4,  5,  6,  7,  8 };
            var y = new double[] { 2, 4, 6, 8, 10, 12, 14, 16 };

            var n = x.Length;

            var interpolationFunction = Interpolate(x, y);

            for (int i = 0; i < n; ++i)
            {
                if ((Math.Abs(interpolationFunction(x[i]) - y[i])) > 0.0001)
                {
                    throw new InvalidOperationException();
                }
            }

            Console.WriteLine("Interpolation tests passed . . . Ok!");
        }

        #endregion
    }
}