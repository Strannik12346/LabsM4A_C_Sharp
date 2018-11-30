using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LabsM4A3
{
    class Task2 : ITask
    {
        private double f(double x) => 1 / x;

        private double a = 1;

        private double UA = 3;

        private double b = 2;

        private double UB = 3;

        public void Solve()
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

            int count = 150;
            double h = (b - a) / count;

            for (int i = 1; i < count; ++i)
            {
                var xk = a + i * h;
                lower.Add(xk / (h * h) - 1 / (2 * h));
                main.Add(-2 * xk / (h * h));
                upper.Add(xk / (h * h) + 1 / (2 * h));
                x.Add(xk);
                y.Add(f(xk));
            }

            lower.Add(0);
            main.Add(1);
            x.Add(b);
            y.Add(UB);

            var result = Progonka.SolveSystem (
                Progonka.UpperDiagonal(upper.ToArray()),
                Progonka.MainDiagonal(main.ToArray()),
                Progonka.LowerDiagonal(lower.ToArray()),
                Progonka.FreeVector(y.ToArray())
            );

            Extensions.Out(x.ToArray(), result.ToArray());
        }
    }
}
