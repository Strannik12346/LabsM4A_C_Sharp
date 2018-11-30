using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabM4A2
{
    class Task4 : ITask
    {
        public double a { get; } = 0;

        public double b { get; } = 3;

        public double c { get; } = 1.815;

        public double K(double x) => x < c ? 0.3 : 1.2;

        public double Q(double x) => x < c ? 5.6 : 2;

        public double F(double x) => 9 * x * (3.5 - x);

        public (double[] UpperDiagonal, double[] MainDiagonal, double[] LowerDiagonal, double[] X, double[] Y) Process(int count)
        {
            var upper = new List<double>();
            var main = new List<double>();
            var lower = new List<double>();
            var x = new List<double>();
            var y = new List<double>();

            double h = (b - a) / count;

            main.Add(K(a) / h + 0.5);
            upper.Add(-K(a) / h);
            x.Add(a);
            y.Add(0);

            for (int i = 1; i < count; ++i)
            {
                var xk = a + i * h;
                lower.Add(- K(xk) / (h * h));
                main.Add(2 * K(xk) + Q(xk));
                upper.Add(-K(xk) / (h * h));
                x.Add(xk);
                y.Add(F(xk));
            }

            lower.Add(-K(b) / h);
            main.Add(K(b) / h + 0.5);
            x.Add(b);
            y.Add(0);

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
