using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabsM4A6
{
    class Task1
    { 
        // Краевое условие
        private const double u_0_t = 0;

        // Краевое условие
        private const double u_L_t = 0;

        // Начальное условие
        private double u_x_0(double x)
            => (x <= L / 2)
               ? 2 * deltaU * x / L
               : 2 * deltaU * (1 - x / L);

        private const double L = 0.1;

        private const double deltaU = 0.001;

        private const double E = 110_000_000_000;

        private const double p = 4300;

        private double[] CountSecondLayer(double[] firstLayer, double h, double tau)
        {
            int xCount = (int)(L / h) + 1;
            var secondLayer = new double[xCount];

            // Краевые условия
            secondLayer[0] = 0;
            secondLayer[xCount - 1] = 0;

            // Через ряд МакЛорена
            for(int i = 1; i < xCount - 1; ++i)
            {
                var d2_u_dx2 = (firstLayer[i - 1] - 2 * firstLayer[i] + firstLayer[i + 1]) / (h * h);
                secondLayer[i] = firstLayer[i] +
                                 0 /* начальная скорость */ +
                                 tau * tau / 2 * p / E * d2_u_dx2;
            }

            return secondLayer;
        }

        private double[] CountNextLayerExplicit(double[] prePreviousLayer, double[] previousLayer, double h, double tau)
        {
            int xCount = (int)(L / h) + 1;
            double[] nextLayer = new double[xCount];

            // Краевые условия
            nextLayer[0] = u_0_t;
            nextLayer[xCount - 1] = u_L_t;

            for (int k = 1; k < xCount - 1; ++k)
            {
                nextLayer[k] = 2 * previousLayer[k] - 
                               prePreviousLayer[k] +
                               (previousLayer[k + 1] - 2 * previousLayer[k] + previousLayer[k - 1]) *
                               p * (tau * tau) / (E * h * h);
            }

            return nextLayer;
        }

        public void Solve(double h, double tau)
        {
            int xCount = (int)(L / h) + 1;

            double[] firstLayer = new double[xCount];

            // Краевые условия
            firstLayer[0] = u_0_t;
            firstLayer[xCount - 1] = u_L_t;

            // Начальное условие
            for (int i = 1; i < xCount - 1; ++i)
            {
                firstLayer[i] = u_x_0(i * h);
            }

            double[] secondLayer = CountSecondLayer(firstLayer, h, tau);
            double[] thirdLayer;

            for (var t = tau; ; t += tau)
            {
                thirdLayer = CountNextLayerExplicit(firstLayer, secondLayer, h, tau);

                thirdLayer.Select(x => Math.Round(x * 1000, 2)).ToArray().Out();
 
                firstLayer = secondLayer;
                secondLayer = thirdLayer;

                System.Threading.Thread.Sleep(100);
                Console.Clear();
            }
        }
    }
}