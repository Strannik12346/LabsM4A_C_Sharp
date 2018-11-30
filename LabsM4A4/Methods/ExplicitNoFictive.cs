using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LabsM4A4
{
    class ExplicitNoFictive : ITask
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

            for (int k = 0; k < xCount; ++k)
            {
                u[0, k] = CommonData.phi;             // нижняя граница прямоугольника
            }                                         

            for (int v = 1; v < tCount; ++v)
            {
                var tv = tau * v;
                
                for (int k = 1; k < xCount - 1; ++k)
                {
                    var xk = CommonData.a + h * k;
                    u[v, k] = u[v - 1, k] +
                              tau * (
                                  CommonData.k * (u[v - 1, k - 1] - 2 * u[v - 1, k] + u[v - 1, k + 1]) / (h * h) +
                                  CommonData.f
                              );
                }
                
                // расчет последнего узла на слое v
                u[v, xCount - 1] = u[v, xCount - 2] + CommonData.g2(tv) * h;
            }

            return u;
        }
    }
}