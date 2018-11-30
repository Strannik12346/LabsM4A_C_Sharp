using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabsM4A4
{
    interface ITask
    {
        double[,] Solve(double h, double tau);
    }
}