using System;
using System.Collections.Generic;


namespace LabM4A2
{
    class Task1 : ITask
    {
        // Math.Cos(16) < Math.Sin(16);
        public double a { get; } = Math.Cos(16);

        public double b { get; } = Math.Sin(16);

        public double A { get; } = 0;

        public double B { get; } = 0;

        public (double[] UpperDiagonal, double[] MainDiagonal, double[] LowerDiagonal, double[] X, double[] Y) Process(int count)
        {
            var upper = new List<double>();
            var main = new List<double>();
            var lower = new List<double>();
            var x = new List<double>();
            var y = new List<double>();

            upper.Add(0);
            main.Add(1);
            x.Add(a);
            y.Add(A);

            double h = (b - a) / count;
            for (int i = 1; i < count; ++i)
            {
                var xk = a + i * h;
                lower.Add(a / (h * h));
                main.Add(1 - b * xk * xk - 2 * a / (h * h));
                upper.Add(a / (h * h));
                x.Add(xk);
                y.Add(-1);
            }

            lower.Add(0);
            main.Add(1);
            x.Add(b);
            y.Add(B);

            return (
                upper.ToArray(),
                main.ToArray(),
                lower.ToArray(),
                x.ToArray(),
                y.ToArray()
            );
        }
    }
}
