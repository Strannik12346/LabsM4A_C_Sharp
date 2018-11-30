using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LabsM4A5
{
    class Task
    {
        private const double A = 0.18;

        private const double B = 0.065;

        private const double h = 0.002;

        private const double R = 0.025;

        private const double P = 7;

        private const double E = 40;

        private const double v = 0.3;

        private const double W = 0;

        private const double D = E * h * h * h / (12 * (1 - v * v));

        private double Approximate(double value) => double.IsNaN(value) ? 0 : value;

        // Применяет к каждой строке неявную схему начально-краевой задачи
        private double[,] SolveEachRow(double[,] previous, double deltaX, double deltaY, double tau)
        {
            var yCount = previous.GetUpperBound(0) + 1;
            var xCount = previous.GetUpperBound(1) + 1;
            var next = previous.Clone() as double[,];

            // Итерируемся по строкам
            for (int j = 1; j < yCount - 1; ++j)
            {
                var yj = deltaY * j;
                
                if (yj <= R)
                {
                    // Там, где есть вырез, составляем 2 системы

                    #region leftPart

                    var endIndex = 0;

                    var upper = new List<double>();
                    var main = new List<double>();
                    var lower = new List<double>();
                    var free = new List<double>();

                    // Краевое условие
                    upper.Add(0);
                    main.Add(1);
                    free.Add(W);

                    // Вбиваем прогоночные коэффициенты
                    // до первого NaN (значение до него - граница)
                    for (int i = 1; i < xCount - 1; ++i)
                    {
                        if (double.IsNaN(previous[j, i + 1]))
                        {
                            endIndex = i;
                            break;
                        }

                        upper.Add(-1 / (deltaX * deltaX));
                        main.Add(1 + 2 / (deltaX * deltaX));
                        lower.Add(-1 / (deltaX * deltaX));

                        var lambda2 = (
                                Approximate(previous[j - 1, i]) - 
                                2 * Approximate(previous[j, i]) + 
                                Approximate(previous[j + 1, i])
                            ) / (deltaY * deltaY);

                        free.Add(previous[j, i] + tau / 2 * (lambda2 - P / D));
                    }

                    // Краевое условие
                    main.Add(1);
                    lower.Add(0);
                    free.Add(W);

                    var resultRow = Progonka.SolveSystem(
                        Progonka.UpperDiagonal(upper.ToArray()),
                        Progonka.MainDiagonal(main.ToArray()),
                        Progonka.LowerDiagonal(lower.ToArray()),
                        Progonka.FreeVector(free.ToArray())
                    );

                    for (int i = 0; i <= endIndex; ++i)
                    {
                        next[j, i] = resultRow[i];
                    }

                    #endregion

                    #region rightPart

                    // Отражаем симметрично индексы
                    var startIndex = previous.GetUpperBound(1) - endIndex;

                    upper.Clear();
                    main.Clear();
                    lower.Clear();
                    free.Clear();

                    // Краевое условие
                    upper.Add(0);
                    main.Add(1);
                    free.Add(W);

                    // Вбиваем прогоночные коэффициенты
                    // до первого NaN (значение до него - граница)
                    for (int i = startIndex + 1; i < xCount - 1; ++i)
                    {
                        upper.Add(-1 / (deltaX * deltaX));
                        main.Add(1 + 2 / (deltaX * deltaX));
                        lower.Add(-1 / (deltaX * deltaX));

                        var lambda2 = (
                                Approximate(previous[j - 1, i]) - 
                                2 * Approximate(previous[j, i]) + 
                                Approximate(previous[j + 1, i])
                            ) / (deltaY * deltaY);

                        free.Add(previous[j, i] + tau / 2 * (lambda2 - P / D));
                    }

                    // Краевое условие
                    main.Add(1);
                    lower.Add(0);
                    free.Add(W);

                    resultRow = Progonka.SolveSystem(
                        Progonka.UpperDiagonal(upper.ToArray()),
                        Progonka.MainDiagonal(main.ToArray()),
                        Progonka.LowerDiagonal(lower.ToArray()),
                        Progonka.FreeVector(free.ToArray())
                    );

                    for (int i = startIndex, k = 0; i < xCount; ++i, ++k)
                    {
                        next[j, i] = resultRow[k];
                    }

                    #endregion
                }
                else
                {
                    // Там, где вырез закончился, составляем 1 ур-е

                    var upper = new List<double>();
                    var main = new List<double>();
                    var lower = new List<double>();
                    var free = new List<double>();

                    // Краевое условие
                    upper.Add(0);
                    main.Add(1);
                    free.Add(W);

                    // Вбиваем прогоночные коэффициенты
                    for (int i = 1; i < xCount - 1; ++i)
                    {
                        upper.Add(-1 / (deltaX * deltaX));
                        main.Add(1 + 2 / (deltaX * deltaX));
                        lower.Add(-1 / (deltaX * deltaX));

                        var lambda2 = (
                                Approximate(previous[j - 1, i]) - 
                                2 * Approximate(previous[j, i]) +
                                Approximate(previous[j + 1, i])
                            ) / (deltaY * deltaY);

                        free.Add(previous[j, i] + tau / 2 * (lambda2 - P / D));
                    }

                    // Краевое условие
                    main.Add(1);
                    lower.Add(0);
                    free.Add(W);

                    var resultRow = Progonka.SolveSystem(
                        Progonka.UpperDiagonal(upper.ToArray()),
                        Progonka.MainDiagonal(main.ToArray()),
                        Progonka.LowerDiagonal(lower.ToArray()),
                        Progonka.FreeVector(free.ToArray())
                    );

                    // Переписываем решение системы
                    for(int i = 0; i < xCount; ++i)
                    {
                        next[j, i] = resultRow[i];
                    }
                }
            }

            return next;
        }

        // Применяет к каждому столбцу неявную схему начально-краевой задачи
        private double[,] SolveEachColumn(double[,] previous, double deltaX, double deltaY, double tau)
        {
            var yCount = previous.GetUpperBound(0) + 1;
            var xCount = previous.GetUpperBound(1) + 1;
            var next = previous.Clone() as double[,];

            // Итерируемся по столбцам
            for (int i = 1; i < xCount - 1; ++i)
            {
                var xi = deltaX * i;

                // Везде составляем одно уравнение
                var upper = new List<double>();
                var main = new List<double>();
                var lower = new List<double>();
                var free = new List<double>();

                // Цикл, чтобы найти границу (первый не NaN)
                int startIndex;
                for (startIndex = 0; double.IsNaN(previous[startIndex, i]); ++startIndex) ;
                
                // Краевое условие
                upper.Add(0);
                main.Add(1);
                free.Add(W);
                
                // Вбиваем прогоночные коэффициенты
                for (int j = startIndex + 1; j < yCount - 1; ++j)
                {
                    upper.Add(-1 / (deltaY * deltaY));
                    main.Add(1 + 2 / (deltaY * deltaY));
                    lower.Add(-1 / (deltaY * deltaY));

                    var lambda1 = (
                            Approximate(previous[j, i - 1]) - 
                            2 * Approximate(previous[j, i]) +
                            Approximate(previous[j, i + 1])
                        ) / (deltaX * deltaX);

                    free.Add(previous[j, i] + tau / 2 * (lambda1 - P / D));
                }

                // Краевое условие
                main.Add(1);
                lower.Add(0);
                free.Add(W);

                var resultRow = Progonka.SolveSystem(
                    Progonka.UpperDiagonal(upper.ToArray()),
                    Progonka.MainDiagonal(main.ToArray()),
                    Progonka.LowerDiagonal(lower.ToArray()),
                    Progonka.FreeVector(free.ToArray())
                );

                // Переписываем решение системы
                for (int j = startIndex, k = 0; j < yCount; ++j, ++k)
                {
                    next[j, i] = resultRow[k];
                }
            }

            return next;
        }

        // Установим начальные значения сетки
        private double GetInitialValue(double xi, double yj, (double X, double Y) center)
        {
            double squareDistance = (center.X - xi) * (center.X - xi) + (center.Y - yj) * (center.Y - yj);

            return squareDistance < R * R
                ? double.NaN
                : 0;
        }

        // Проверим точность
        private double GetDifference(double[,] before, double[,] after)
        {
            int xCount = before.GetUpperBound(1) + 1;
            int yCount = before.GetUpperBound(0) + 1;

            double maxDifference = 0;
            
            for (int i = 0; i < xCount; ++i)
            {
                for (int j = 0; j < yCount; ++j)
                {
                    if (double.IsNaN(before[j, i]) && double.IsNaN(after[j, i])) continue;

                    maxDifference = Math.Max(
                        maxDifference, 
                        Math.Abs(before[j, i] - after[j, i])
                    );
                }
            }

            return maxDifference;
        }

        public void Solve(double deltaX, double deltaY, double tau, double accuracy)
        {
            int xCount = (int)(A / deltaX) + 1;
            int yCount = (int)(B / deltaY) + 1;

            var center = (A / 2, 0.0); // координаты центра окружности

            double[,] initialMatrix = new double[yCount, xCount];
            
            for (int i = 0; i < xCount; ++i)
            {
                var xi = i * deltaX;

                for (int j = 0; j < yCount; ++j)
                {
                    var yj = j * deltaY;

                    initialMatrix[j, i] = GetInitialValue(xi, yj, center);
                }
            }

            double[,] thisLayerMatrix = initialMatrix, middleLayerMatrix, nextLayerMatrix;

            while (true)
            {
                middleLayerMatrix = SolveEachRow(thisLayerMatrix, deltaX, deltaY, tau);
                nextLayerMatrix = SolveEachColumn(middleLayerMatrix, deltaX, deltaY, tau);

                thisLayerMatrix.Out();

                Console.ReadKey(true);

                if (GetDifference(thisLayerMatrix, nextLayerMatrix) < accuracy)
                {
                    thisLayerMatrix = nextLayerMatrix;
                    break;
                }

                thisLayerMatrix = nextLayerMatrix;
            }
            
            thisLayerMatrix.Out();
            Console.ReadKey(true);
        }
    }
}