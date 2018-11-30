using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabM4A2
{
    interface ITask
    {
        (double[] UpperDiagonal, double[] MainDiagonal, double[] LowerDiagonal, double[] X, double[] Y) Process(int count);
    }
}
