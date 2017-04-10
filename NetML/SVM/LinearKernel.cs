using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.SupportVectorMachine
{
    public class LinearKernel : Kernel
    {
        public override double Calculate(double[] firstVector, double[] secondVector)
        {
            return base.InnerProduct(firstVector, secondVector);
        }
    }
}
