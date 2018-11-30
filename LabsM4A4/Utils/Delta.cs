using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LabsM4A4
{
    class Delta
    {
        public double h { get; }

        public double tau { get; }

        public double maxDeltaT1 { get; }
        
        public double maxDeltaT2 { get; }

        public void Out()
        {

            var rH = Math.Round(h, 4);
            var rTau = Math.Round(tau, 4);
            var rDelta1 = Math.Round(maxDeltaT1, 4);
            var rDelta2 = Math.Round(maxDeltaT2, 4);

            if (maxDeltaT1 > 1 || maxDeltaT2 > 1)
            {
                Console.WriteLine($"h:\t{rH}\t|\ttau:\t{rTau}\t\t|\t deltas:\tERROR!!!");
                return;
            }

            Console.WriteLine($"h:\t{rH}\t|\ttau:\t{rTau}\t\t|\t deltas:\t{rDelta1}\tand  {rDelta2}");
        }

        public Delta(double h, double tau, double maxDeltaT1, double maxDeltaT2)
        {
            this.h = h;
            this.tau = tau;
            this.maxDeltaT1 = maxDeltaT1;
            this.maxDeltaT2 = maxDeltaT2;
        }
    }
}
