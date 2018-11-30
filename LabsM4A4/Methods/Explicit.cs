using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LabsM4A4
{
    class Explicit : ITask
    {
        public double[,] Solve(double h, double tau)
        {
            var xCount = (int)((CommonData.b - CommonData.a) / h + 1);
            var tCount = (int)(CommonData.T / tau + 1);
            var u = new double[tCount, xCount + 1];   // Еще +1 для фиктивного узла

            for (int v = 0; v < tCount; ++v)
            {
                var tv = v * tau;
                u[v, 0] = CommonData.g1(tv);          // левая граница прямоугольника
                u[v, xCount - 1] = double.NaN;        // правая граница прямоугольника
                u[v, xCount] = double.NaN;            // линия "фиктивных" узлов
            }

            for (int k = 0; k < xCount + 1; ++k)
            {
                u[0, k] = CommonData.phi;             // нижняя граница прямоугольника
            }                                         // (включая "фиктивный" узел)

            for (int v = 1; v < tCount; ++v)
            {
                var tv = tau * v;
                
                for (int k = 1; k < xCount; ++k)
                {
                    var xk = CommonData.a + h * k;
                    u[v, k] = u[v - 1, k] +
                              tau * (
                                  CommonData.k * (u[v - 1, k - 1] - 2 * u[v - 1, k] + u[v - 1, k + 1]) / (h * h) +
                                  CommonData.f
                              );
                }
                
                // расчет "фиктивного" узла на слое v
                u[v, xCount] = u[v, xCount - 2] + CommonData.g2(tv) * 2 * h;
            }

            return u;
        }
    }
}