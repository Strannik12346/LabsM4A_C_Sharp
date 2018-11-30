using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabsM4A3
{
    class Program
    {
        static void Main(string[] args)
        {
            double a = 0.00000000000001;
            Console.WriteLine(Math.Round(a, 3));

            Console.WriteLine("Choose the task: 2 - 4");

            ITask[] tasks = new ITask[]
            {
                null,
                null,
                new Task2(),
                new Task3(),
                new Task4()
            };

            int taskNumber = int.Parse(Console.ReadLine());

            tasks[taskNumber].Solve();

            Console.ReadKey(true);
        }
    }
}
