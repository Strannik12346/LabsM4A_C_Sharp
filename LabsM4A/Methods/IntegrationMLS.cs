namespace LabsM4A
{
    public class IntegrationMLS : IEquationSolver
    {
        public (double[,], double[]) Process(int baseSize, double a, double b, double A, double B)
        {
            var matrix = new double[baseSize, baseSize];
            var vector = new double[baseSize];

            for (int eq = 0; eq < baseSize; ++eq) // Цикл по уравнениям
            {
                for (int i = 0; i < baseSize; ++i) // Цикл по коэффициентам a[i], (i + 1) берется из-за нумерации с 1
                {
                    // Коэффициент при a[i] в уравнении eq
                    matrix[eq, i] = MathHelper.Integrate(
                        x => 
                        (
                            (i + 1).d2Phi_dx_dx(x) +
                            BasicFunctions.P(x) * (i + 1).dPhi_dx(x) +
                            BasicFunctions.Q(x) * (i + 1).Phi(x)
                        ) 
                        *   // Перемножение внутри интеграла
                        (
                            (eq + 1).d2Phi_dx_dx(x) +
                            BasicFunctions.P(x) * (eq + 1).dPhi_dx(x) +
                            BasicFunctions.Q(x) * (eq + 1).Phi(x)
                        ),
                        a,  // Левая граница
                        b   // Правая граница
                    );
                }

                // Свободный член
                vector[eq] = -(MathHelper.Integrate(
                    x => 
                    (
                        0.d2Phi_dx_dx(x) +
                        BasicFunctions.P(x) * 0.dPhi_dx(x) +
                        BasicFunctions.Q(x) * 0.Phi(x) - 
                        BasicFunctions.F(x)
                    )
                    *   // Перемножение внутри интеграла
                    (
                        (eq + 1).d2Phi_dx_dx(x) +
                        BasicFunctions.P(x) * (eq + 1).dPhi_dx(x) +
                        BasicFunctions.Q(x) * (eq + 1).Phi(x)
                    ),
                    a,  // Левая граница
                    b   // Правая граница
                ));
            }

            return (matrix, vector);
        }
    }
}
