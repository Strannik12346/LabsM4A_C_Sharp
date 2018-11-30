using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LabM4A2
{
    static class StepChanging
    {
        public static (double[] X, double[] Y) ChooseStep(ITask task, int count, double accuracy)
        {
            int recursion = 0;
            var accuracyReached = true;
            double[] previousResult = null, result = null, x = null;

            do
            {
                accuracyReached = true;

                var dIIIagonalSystem = task.Process(count);
                previousResult = Progonka.SolveSystem(
                    Progonka.UpperDiagonal(dIIIagonalSystem.UpperDiagonal),
                    Progonka.MainDiagonal(dIIIagonalSystem.MainDiagonal),
                    Progonka.LowerDiagonal(dIIIagonalSystem.LowerDiagonal),
                    Progonka.FreeVector(dIIIagonalSystem.Y)
                );

                count *= 2;

                dIIIagonalSystem = task.Process(count);
                result = Progonka.SolveSystem(
                    Progonka.UpperDiagonal(dIIIagonalSystem.UpperDiagonal),
                    Progonka.MainDiagonal(dIIIagonalSystem.MainDiagonal),
                    Progonka.LowerDiagonal(dIIIagonalSystem.LowerDiagonal),
                    Progonka.FreeVector(dIIIagonalSystem.Y)
                );

                for (int i = 0; i < previousResult.Length; ++i)
                {
                    accuracyReached = accuracyReached && (Math.Abs(previousResult[i] - result[2 * i]) < accuracy);
                }

                if (recursion++ > 10)
                {
                    throw new InvalidOperationException();
                }

                x = dIIIagonalSystem.X;
            }
            while (!accuracyReached);

            return (x, result);
        }
    }
}