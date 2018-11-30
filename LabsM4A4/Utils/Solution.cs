using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabsM4A4
{
    class Solution
    {
        public double h { get; }

        public double tau { get; }

        public double[,] matrix { get; }

        public double MaxDelta(Solution accurate, double time)
        {
            double maxDelta = 0;

            int rowHere = (int)(time / tau);
            int rowAccurate = (int)(time / accurate.tau);

            int i = 0, iMax = matrix.GetUpperBound(1);
            int j = 0, jMax = accurate.matrix.GetUpperBound(1);

            while(i <= iMax && j <= jMax)
            {
                double xHere = i * h;
                double xAccurate = j * accurate.h;

                if (xHere < xAccurate)
                {
                    ++i;
                    continue;
                }

                if (xHere > xAccurate)
                {
                    ++j;
                    continue;
                }

                var delta = Math.Abs(matrix[rowHere, i] - accurate.matrix[rowAccurate, j]);
                maxDelta = Math.Max(delta, maxDelta);
                ++i;
                ++j;
            }

            return maxDelta;
        }

        public Solution(double h, double tau, double[,] matrix)
        {
            this.h = h;
            this.tau = tau;
            this.matrix = matrix;
        }
    }
}
