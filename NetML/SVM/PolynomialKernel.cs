using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.SupportVectorMachine
{
    public class PolynomialKernel : Kernel
    {
        private int d;

        public PolynomialKernel(int d)
        {
            this.d = d;
        }

        public override double Calculate(double[] firstVector, double[] secondVector)
        {
            return Math.Pow(base.InnerProduct(firstVector, secondVector), d);
        }
    }
}
