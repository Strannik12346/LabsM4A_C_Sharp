using System;
using System.Linq;

namespace LabsM4A
{
    public class DiscreteMLS : IEquationSolver
    {
        public (double[,], double[]) Process(int baseSize, double a, double b, double A, double B)
        {
            var matrix = new double[baseSize, baseSize];
            var vector = new double[baseSize];

            // Шаг, с которым ищем точки
            var h = (b - a) / baseSize;

            //  Сами точки
            var x = new double[baseSize];

            for (int i = 0; i < x.Length; ++i)
            {
                x[i] = a + h * (i + 0.5);
            }

            Console.WriteLine("Points: ");
            x.Out();

            for (int eq = 0; eq < baseSize; ++eq) // Цикл по уравнениям
            {
                for (int i = 0; i < baseSize; ++i) // Цикл по коэффициентам a[i], (i + 1) берется из-за нумерации с 1
                {
                    // Коэффициент при a[i] в уравнении eq
                    matrix[eq, i] = 2 * x
                        .Select(
                            arg =>
                            (
                                (i + 1).d2Phi_dx_dx(arg) +
                                BasicFunctions.P(arg) * (i + 1).dPhi_dx(arg) +
                                BasicFunctions.Q(arg) * (i + 1).Phi(arg)
                            )
                            *
                            (
                                (eq + 1).d2Phi_dx_dx(arg) +
                                BasicFunctions.P(arg) * (eq + 1).dPhi_dx(arg) +
                                BasicFunctions.Q(arg) * (eq + 1).Phi(arg)
                            )
                        )
                        .Sum();
                }

                // Свободный член
                vector[eq] = - 2 * x  // Подставляем точки через LINQ
                    .Select(
                        arg =>
                        (
                            0.d2Phi_dx_dx(arg) + 
                            BasicFunctions.P(arg) * 0.dPhi_dx(arg) + 
                            BasicFunctions.Q(arg) * 0.Phi(arg) -
                            BasicFunctions.F(arg)
                        )
                        *
                        (
                            (eq + 1).d2Phi_dx_dx(arg) +
                            BasicFunctions.P(arg) * (eq + 1).dPhi_dx(arg) +
                            BasicFunctions.Q(arg) * (eq + 1).Phi(arg)
                        )
                    )
                    .Sum();
            }

            return (matrix, vector);
        }
    }
}
