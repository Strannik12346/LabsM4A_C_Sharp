using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabM4A2
{
    class Task3 : ITask
    {
        public double a { get; } = 2.2;

        public double b { get; } = 4.2;

        public (double[] UpperDiagonal, double[] MainDiagonal, double[] LowerDiagonal, double[] X, double[] Y) Process(int count)
        {
            var upper = new List<double>();
            var main = new List<double>();
            var lower = new List<double>();
            var x = new List<double>();
            var y = new List<double>();

            double h = (b - a) / count;

            // отнимаем от первой строки вторую, 
            // чтобы получить трехдиагональную систему
            var firstLine = new double[]
            {
                1 - 0.15 * h, // главная диагональ
                0.2 / h,      // верхняя диагональ
                0.05 / h,     // ненужный элемент
                0.2           // значение y[0]
            };

            var secondLine = new double[]
            {
                1 / (h * h) + 3 * (a + h) / h, // нижняя диагональ
                -2 / (h * h) + 0.5,            // главная диагональ
                1 / (h * h) - 3 * (a + h) / h, // верхняя диагональ
                -3                             // значение y[1]
            };

            var coef = firstLine[2] / secondLine[2];

            main.Add(firstLine[0] - secondLine[0] * coef);
            upper.Add(firstLine[1] - secondLine[1] * coef);
            x.Add(a);
            y.Add(firstLine[3] - secondLine[3] * coef);
            
            for (int i = 1; i < count; ++i)
            {
                var xk = a + i * h;
                lower.Add(1 / (h * h) + 3 * xk / h);
                main.Add(-2 / (h * h) + 0.5);
                upper.Add(1 / (h * h) - 3 * xk / h);
                x.Add(xk);
                y.Add(-3);
            }

            // отнимаем от последней строки предпоследнюю, 
            // чтобы получить трехдиагональную систему
            var preLastLine = new double[]
            {
                1 / (h * h) + 3 * (a + (count - 1) * h) / h, // нижняя диагональ
                -2 / (h * h) + 0.5,                          // главная диагональ
                1 / (h * h) - 3 * (a + (count - 1) * h) / h, // верхняя диагональ
                -3                                           // значение y[count - 1]
            };

            var lastLine = new double[]
            {
                1 / (2 * h),    // ненужный элемент
                -2 / h,         // нижняя диагональ
                3 / (2 * h),    // главная диагональ
                4               // значение y[count]
            };

            coef = lastLine[0] / preLastLine[0];

            lower.Add(lastLine[1] - preLastLine[1] * coef);
            main.Add(lastLine[2] - preLastLine[2] * coef);
            x.Add(b);
            y.Add(lastLine[3] - preLastLine[3] * coef);

            return (
                upper.ToArray(),
                main.ToArray(),
                lower.ToArray(),
                x.ToArray(),
                y.ToArray()
            );
        }
    }
}
