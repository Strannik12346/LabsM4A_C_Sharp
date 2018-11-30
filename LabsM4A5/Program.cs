using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabsM4A5
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = new Task();
            task.Solve(0.01, 0.002, 0.04, 0.01);
            Console.ReadKey(true);
        }
    }
}
