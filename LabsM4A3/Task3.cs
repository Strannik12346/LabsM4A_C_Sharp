using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LabsM4A3
{
    class Task3 : ITask
    {
        private double k(int x) => x;

        private double f(int x) => 1 / x;

        private double a = 1;

        private double UA = 3;

        private double b = 2;

        private double UB = 3;

        private double tau = 0.05;

        private double h = 0.01;

        private double phi(double x) => (UB - UA) * (x - a) / (b - a) + UA;


        public void Solve()
        {
            var xCount = (int)((b - a) / h + 1);
            var tCount = 201; // 0 * tau ... 200 * tau
            var u = new double[tCount, xCount];
            
            for (int v = 0; v < tCount; ++v)
            {
                u[v, 0] = UA;          // левая граница прямоугольника
                u[v, xCount - 1] = UB; // правая граница прямоугольника
            }

            for (int k = 0; k < xCount; ++k)
            {
                var xk = a + h * k;
                u[0, k] = phi(xk); // нижняя граница прямоугольника
            }
            
            for (int v = 1; v < tCount; ++v)
            {
                var tv = tau * v;
                
                for (int k = 1; k < xCount - 1; ++k)
                {
                    var xk = a + h * k;
                    u[v, k] = 1 / (1 / tau - 1 / h) *
                              (
                                (1 - Math.Exp(-tv)) / xk + 
                                u[v - 1, k] / tau - 
                                u[v, k - 1] / h +
                                (u[v - 1, k + 1] - 2 * u[v - 1, k] + u[v - 1, k + 1]) * xk / (h * h)
                              );
                }
            }

            u.Out();
        }
    }
}