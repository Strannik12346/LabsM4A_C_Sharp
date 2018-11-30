using System;

namespace LabsM4A
{
    public class Collocations : IEquationSolver
    {
        public (double[,], double[]) Process(int baseSize, double a, double b, double A, double B)
        {
            // Шаг, с которым ищем точки коллокации
            var h = (b - a) / baseSize;

            // Точки коллокации
            var x = new double[baseSize];

            for (int i = 0; i < x.Length; ++i)
            {
                x[i] = a + h * (i + 0.5);
            }
            
            Console.WriteLine("Collocation points: ");
            x.Out();

            var matrix = new double[baseSize, baseSize];
            var vector = new double[baseSize];

            for (int eq = 0; eq < baseSize; ++eq) // по уравнениям / точкам коллокации
            {
                for (int i = 0; i < baseSize; ++i) // по коэффициентам a[i], (i + 1) берется из-за нумерации с 1
                {
                    matrix[eq, i] = 
                        (i + 1).d2Phi_dx_dx(x[eq]) +
                        BasicFunctions.P(x[eq]) * (i + 1).dPhi_dx(x[eq]) + 
                        BasicFunctions.Q(x[eq]) * (i + 1).Phi(x[eq]);
                }

                vector[eq] = -(
                    0.d2Phi_dx_dx(x[eq]) +
                    BasicFunctions.P(x[eq]) * 0.dPhi_dx(x[eq]) +
                    BasicFunctions.Q(x[eq]) * 0.Phi(x[eq]) -
                    BasicFunctions.F(x[eq])
                );
            }

            return (matrix, vector);
        }
    }
}