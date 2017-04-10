using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.SupportVectorMachine
{
    public class LogitKernel : Kernel
    {
        public override double Calculate(double[] firstVector, double[] secondVector)
        {
            var summe = 0.0;
            for (int i = 0; i < firstVector.Length; i++)
            {
                if (i == 0)
                {
                    summe += firstVector[i];
                }
                else
                {
                    summe += firstVector[i] * secondVector[i - 1];
                }
            }
            return 1.0 / (1.0 + Math.Pow(Math.E, summe * -1.0));
        }        
    }
}
