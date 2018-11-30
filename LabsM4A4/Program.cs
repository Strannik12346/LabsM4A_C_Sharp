using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabsM4A4
{
    class Program
    {
        static void Main(string[] args)
        {
            ITask[] tasks = new ITask[]
            {
                new Explicit(),
                new ExplicitNoFictive(),
                new Implicit()
            };

            Console.WriteLine("Choose your destiny!");
            Console.WriteLine("Explicit method with fictive nodes: 0");
            Console.WriteLine("Explicit method without fictive nodes: 1");
            Console.WriteLine("Implicit method without fictive nodes: 2");

            int number = int.Parse(Console.ReadLine());

            var hAcc = 0.005;
            var accurateSolution = new Solution(
                hAcc,
                hAcc * hAcc / 6,
                tasks[number].Solve(hAcc, hAcc * hAcc / 6)
            );

            var hVariants = new[] {
                0.5,
                0.2,
                0.1,
            };

            var tauVariants = new[] {
                0.05,
                0.02,
                0.01,

                0.005,
                0.002,
                0.001,

                0.0005,
                0.0002,
                0.0001
            };

            var solutions = new List<Solution>();

            foreach (var h in hVariants)
                foreach (var tau in tauVariants)
                    if (tau <= h * h / CommonData.k)
                        solutions.Add(new Solution(h, tau, tasks[number].Solve(h, tau)));

            var deltas = new List<Delta>();
            foreach(var solution in solutions)
            {
                double maxDeltaT1 = solution.MaxDelta(accurateSolution, CommonData.t1);
                double maxDeltaT2 = solution.MaxDelta(accurateSolution, CommonData.t2);
                deltas.Add(new Delta(solution.h, solution.tau, maxDeltaT1, maxDeltaT2));
            }

            Console.WriteLine("Main table with all deltas:");
            foreach(var delta in deltas)
            {
                delta.Out();
            }

            Console.WriteLine("\nDelta groups by common h:");
            foreach(var deltaGroup in deltas.GroupBy(d => d.h))
            {
                Console.WriteLine($"\nh = {deltaGroup.Key} =>");
                foreach(var delta in deltaGroup)
                {
                    delta.Out();
                }
            }

            Console.WriteLine("\nDelta groups by common tau:");
            foreach(var deltaGroup in deltas.GroupBy(d => d.tau))
            {
                Console.WriteLine($"\ntau = {deltaGroup.Key}");
                foreach(var delta in deltaGroup)
                {
                    delta.Out();
                }
            }

            Console.ReadKey(true);
        }
    }
}
