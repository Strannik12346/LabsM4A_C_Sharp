using System;
using System.Collections.Generic;

namespace LabsM4A
{
    interface IEquationSolver
    {
        /// <summary>
        /// Функция получает систему линейных 
        /// алгебраических уравнений по задаче:
        /// y'' + p(x)y' + q(x)y = f(x)
        /// y(a) = A
        /// y(b) = B
        /// </summary>
        /// <param name="baseSize">Количество базисных функций</param>
        /// <param name="a">Левая граничная точка</param>
        /// <param name="b">Правая граничная точка</param>
        /// <param name="A">y(a)</param>
        /// <param name="B">y(b)</param>
        /// <returns>Система линейных уравнений в виде Ax = b, A : double[,], b : double[]</returns>
        (double[,], double[]) Process(int baseSize, double a, double b, double A, double B);
    }

    static class Program
    {
        static Program()
        {
            Console.WriteLine(MathHelper.Integrate(x => x * x * x, -1, 1));
            Console.WriteLine(MathHelper.Integrate(x => x, -1, 0));
            Console.WriteLine(MathHelper.Integrate(x => -x * x, -2, 2));
        }

        const double a = -1;

        const double b = 1;

        const double A = 0;

        const double B = 0;
        
        static void Main(string[] args)
        {
            Console.WriteLine("Choose the number of basis functions: ");
            var baseSize = int.Parse(Console.ReadLine());

            Console.WriteLine("Choose method:");
            Console.WriteLine("0: Collocations;");
            Console.WriteLine("1: IntegrationMLS;");
            Console.WriteLine("2: DiscreteMLS;");
            Console.WriteLine("3: Galerkin;");

            var methods = new Dictionary<string, IEquationSolver>()
            {
                { "0",  new Collocations()   },
                { "1",  new IntegrationMLS() },
                { "2",  new DiscreteMLS()    },
                { "3",  new Galerkin()       },
            };

            var choosenMethod = methods[Console.ReadLine()];
            
            (double[,] matrix, double[] vector) system = choosenMethod.Process(baseSize, a, b, A, B);
            system.Out();
            system.MakeTriangle();
            system.CleanTriangle();
            system.Out();

            var result = system.SolveTriangle();

            result.Out("Human-readable result: ");
            result.OutForDesmos("Desmos-readable result:");

            Console.ReadKey(true);
        }
    }
}
