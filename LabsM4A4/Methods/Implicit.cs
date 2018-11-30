using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LabsM4A4
{
    class Implicit : ITask
    {
        public double[,] Solve(double h, double tau)
        {
            var xCount = (int)((CommonData.b - CommonData.a) / h + 1);
            var tCount = (int)(CommonData.T / tau + 1);
            var u = new double[tCount, xCount];

            for (int v = 0; v < tCount; ++v)
            {
                var tv = v * tau;
                u[v, 0] = CommonData.g1(tv);          // левая граница прямоугольника
                u[v, xCount - 1] = double.NaN;        // правая граница прямоугольника
            }

            for (int k = 0; k < xCount ; ++k)
            {
                u[0, k] = CommonData.phi;             // нижняя граница прямоугольника
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
                    var xk = CommonData.a + k * h;
                    lower.Add(-CommonData.k / (h * h));
                    main.Add(1 / tau + 2 * CommonData.k / (h * h));
                    upper.Add(-CommonData.k / (h * h));
                    y.Add(CommonData.f + u[v - 1, k] / tau);
                }

                // сюда пихаем аппроксимацию из производной
                lower.Add(-1 / h);
                main.Add(1 / h);
                y.Add(CommonData.g2(tv));

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

            return u;
        }
    }
}
