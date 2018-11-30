using System;

namespace LabsM4A
{
    static class BasicFunctions
    {
        public const double a = -0.961; // sin(17)

        public const double b = -0.275; // cos(17)

        /// <summary>
        /// i-я базисная функция
        /// </summary>
        /// <param name="i"> Номер функции </param>
        /// <param name="x"> Аргумент функции </param>
        /// <returns></returns>
        public static double Phi(this int i, double x)
        {
            if (i == 0)
            {
                return 0;
            }

            return Math.Pow(x, i) * (1 - x * x);
        }

        /// <summary>
        /// Первая производная i-й базисной функции
        /// </summary>
        /// <param name="i"> Номер функции </param>
        /// <param name="x"> Аргумент функции </param>
        /// <returns></returns>
        public static double dPhi_dx(this int i, double x)
        {
            if (i == 0)
            {
                return 0;
            }

            return Math.Pow(x, -1 + i) * (i - (2 + i) * x * x);
        }

        /// <summary>
        /// Вторая производная i-й базисной функции
        /// </summary>
        /// <param name="i"> Номер функции </param>
        /// <param name="x"> Аргумент функции </param>
        /// <returns></returns>
        public static double d2Phi_dx_dx(this int i, double x)
        {
            if (i == 0)
            {
                return 0;
            }

            return (-1 - i) * Math.Pow(x, -2 + i) * (1 + (2 + i) * x * x);
        }

        /// <summary>
        /// p(x) в уравнении y'' + p(x)y' + q(x)y = f(x)
        /// </summary>
        /// <param name="x">Аргумент функции</param>
        /// <returns></returns>
        public static double P(double x)
        {
            return 0;
        }

        /// <summary>
        /// q(x) в уравнении y'' + p(x)y' + q(x)y = f(x)
        /// </summary>
        /// <param name="x">Аргумент функции</param>
        /// <returns></returns>
        public static double Q(double x)
        {
            return (1 + b * x * x) / a;
        }

        /// <summary>
        /// f(x) в уравнении y'' + p(x)y' + q(x)y = f(x)
        /// </summary>
        /// <param name="x">Аргумент функции</param>
        /// <returns></returns>
        public static double F(double x)
        {
            return -1 / a;
        }
    }
}
