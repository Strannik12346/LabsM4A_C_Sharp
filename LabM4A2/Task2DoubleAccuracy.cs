using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabM4A2
{
    class Task2DoubleAccuracy : ITask
    {
        public double P(double x) => 0.5 * (1 - 0.4 * x * x);

        public double Q(double x) => Math.Exp(-x) * (9 + x);

        public double F(double x) => 10 * Math.Sin(x);

        public double a { get; } = 0;

        public double b { get; } = 1.5;

        public double UA { get; } = 0;

        public double UB { get; } = 4;

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
            y.Add(UA);

            double h = (b - a) / count;
            for (int i = 1; i < count; ++i)
            {
                var xk = a + i * h;
                lower.Add(1 / (h * h) - P(xk) / (2 * h));
                main.Add(-2 / (h * h) + Q(xk));
                upper.Add(1 / (h * h) + P(xk) / (2 * h));
                x.Add(xk);
                y.Add(F(xk));
            }

            lower.Add(0);
            main.Add(1);
            x.Add(b);
            y.Add(UB);

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