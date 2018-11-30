using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabsM4A6
{
    class Task2
    {
        private const double a = 0.01;

        private const double b = 0.02;

        private const double G1 = 0; // Граничное условие u = 0

        private const double G2 = 0; // Граничное условие du/dn = 0

        private const double G3 = 0; // Граничное условие u = 0

        private const double G4 = 0; // Граничное условие du/dn = 0

        // Начальное условие для функции
        private double u_t_0(double x) => Math.Atan(Math.Cos(Math.PI * x / a));

        // Начальне условие для производной
        private double du_dt_t_0(double x, double y) => Math.Sin(2 * Math.PI * x / a) * Math.Sin(Math.PI * y / b);

        private double[,] CountSecondLayer(double[,] firstLayer, double deltaX, double deltaY, double tau)
        {
            int xCount = (int)(a / deltaX) + 1;
            int yCount = (int)(b / deltaY) + 1;

            double[,] secondLayer = new double[yCount, xCount];

            // Граничные условия, передняя 
            // и задняя стенки
            for (int i = 0; i < xCount; ++i)
            {
                secondLayer[0, i] = G4;
                secondLayer[yCount - 1, i] = G2;
            }

            // Граничные условия, боковые стенки
            for (int j = 0; j < yCount; ++j)
            {
                secondLayer[j, 0] = G1;
                secondLayer[j, xCount - 1] = G3;
            }

            for (int j = 1; j < yCount - 1; ++j)
            {
                double yj = j * deltaY;

                for (int i = 1; i < xCount - 1; ++i)
                {
                    double xi = i * deltaX;

                    double d2u_dx2 = (firstLayer[j, i - 1] - 2 * firstLayer[j, i] + firstLayer[j, i + 1]) / (deltaX * deltaX);
                    double d2u_dy2 = (firstLayer[j - 1, i] - 2 * firstLayer[j, i] + firstLayer[j + 1, i]) / (deltaY * deltaY);

                    secondLayer[j, i] = u_t_0(xi) + du_dt_t_0(xi, yj) * tau + (d2u_dx2 + d2u_dy2) * (tau * tau);
                }
            }

            return secondLayer;
        }

        private double[,] CountNextLayerExplicit(double[,] prePreviousLayer, double[,] previousLayer, double deltaX, double deltaY, double tau)
        {
            int xCount = (int)(a / deltaX) + 1;
            int yCount = (int)(b / deltaY) + 1;

            double[,] nextLayer = new double[yCount, xCount];

            // Граничные условия, передняя 
            // и задняя стенки
            for (int i = 0; i < xCount; ++i)
            {
                nextLayer[0, i] = G4;
                nextLayer[yCount - 1, i] = G2;
            }

            // Граничные условия, боковые стенки
            for (int j = 0; j < yCount; ++j)
            {
                nextLayer[j, 0] = G1;
                nextLayer[j, xCount - 1] = G3;
            }

            for (int j = 1; j < yCount - 1; ++j)
            {
                double yj = j * deltaY;

                for (int i = 1; i < xCount - 1; ++i)
                {
                    double xi = i * deltaX;

                    double d2u_dx2 = (previousLayer[j, i - 1] - 2 * previousLayer[j, i] + previousLayer[j, i + 1]) / (deltaX * deltaX);
                    double d2u_dy2 = (previousLayer[j - 1, i] - 2 * previousLayer[j, i] + previousLayer[j + 1, i]) / (deltaY * deltaY);

                    nextLayer[j, i] = 2 * previousLayer[j, i] - prePreviousLayer[j, i] + (d2u_dx2 + d2u_dy2) * (tau * tau);
                }
            }

            return nextLayer;
        }

        public void Solve(double deltaX, double deltaY, double tau)
        {
            int xCount = (int)(a / deltaX) + 1;
            int yCount = (int)(b / deltaY) + 1;

            double[,] firstLayer = new double[yCount, xCount];

            // Граничные условия, передняя 
            // и задняя стенки
            for (int i = 0; i < xCount; ++i)
            {
                firstLayer[0, i] = G4;
                firstLayer[yCount - 1, i] = G2;
            }

            // Граничные условия, боковые стенки
            for(int j = 0; j < yCount; ++j)
            {
                firstLayer[j, 0] = G1;
                firstLayer[j, xCount - 1] = G3;
            }
            
            // Начальное условие (внутр. точки)
            for (int j = 1; j < yCount - 1; ++j)
            {
                for (int i = 1; i < xCount - 1; ++i)
                {
                    firstLayer[j, i] = u_t_0(i * deltaX);
                }
            }

            double[,] secondLayer = CountSecondLayer(firstLayer, deltaX, deltaY, tau);
            double[,] thirdLayer;

            for (var t = tau; ; t += tau)
            {
                thirdLayer = CountNextLayerExplicit(firstLayer, secondLayer, deltaX, deltaY, tau);

                thirdLayer.Out();

                firstLayer = secondLayer;
                secondLayer = thirdLayer;

                System.Threading.Thread.Sleep(300);
                Console.Clear();
            }
        }
    }
}
