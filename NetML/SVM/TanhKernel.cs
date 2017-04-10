using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.SupportVectorMachine
{
    public class TanhKernel : Kernel
    {
        public override double Calculate(double[] firstVector, double[] secondVector)
        {
            var summe = 0.0;
            var n = firstVector.Length;
            for (int i = 0; i < firstVector.Length; i++)
            {
                summe += firstVector[i] * secondVector[i];
            }
            return Math.Tanh(1 / n * summe);
        }        
    }
}
