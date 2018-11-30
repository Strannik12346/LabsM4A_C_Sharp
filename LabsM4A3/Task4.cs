using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LabsM4A3
{
    class Task4 : ITask
    {
        private double a = -1;
         
        private double b = 1;

        private double h = 0.2;

        private double tau = 0.04;

        private double k = 0.5;

        private double T = 0.4;

        private double phi(double x) => x * x;

        private double g1 = 1;

        private double g2 = 1;

        private double f(double x, double t) => x;

        public void Solve()
        {
            var xCount = (int)((b - a) / h + 1);
            var tCount = 201; // 0 * tau ... 200 * tau
            var u = new double[tCount, xCount];

            for (int v = 0; v < tCount; ++v)
            {
                u[v, 0] = g1;          // левая граница прямоугольника
                u[v, xCount - 1] = g2; // правая граница прямоугольника
            }

            for (int k = 0; k < xCount; ++k)
            {
                var xk = a + h * k;
                u[0, k] = phi(xk); // нижняя граница прямоугольника
            }

            for (int v = 1; v < tCount; ++v)
            {
                var tv = tau * v;

                var upper = new List<double>();
                var main = new List<double>();
                var lower = new List<double>();
                var y = new List<double>();

                upper.Add(0);
                main.Add(1);
                y.Add(u[v, 0]);

                for (int k = 1; k < xCount - 1; ++k)
                {
                    var xk = a + k * h;
                    lower.Add(-k / (h * h));
                    main.Add(2 * k / (h * h));
                    upper.Add(1 / tau - k / (h * h));
                    y.Add(f(xk, tv) + u[v - 1, k] / tau);
                }

                lower.Add(0);
                main.Add(1);
                y.Add(u[v, xCount - 1]);

                var result = Progonka.SolveSystem(
                    Progonka.UpperDiagonal(upper.ToArray()),
                    Progonka.MainDiagonal(main.ToArray()),
                    Progonka.LowerDiagonal(lower.ToArray()),
                    Progonka.FreeVector(y.ToArray())
                );

                for (int i = 0; i < result.Length; ++i)
                {
                    u[v, i] = result[i];
                }
            }

            u.Out();
        }
    }
}
