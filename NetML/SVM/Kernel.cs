using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.SupportVectorMachine
{
    public abstract class Kernel
    {
        public abstract double Calculate(double[] firstVector, double[] secondVector);

        protected double InnerProduct(double[] firstVector, double[] secondVector)
        {
            var innerProduct = 0.0;
            for (int i = 0; i < firstVector.Length; i++)
            {
                innerProduct += firstVector[i] * secondVector[i];
            }
            return innerProduct;
        }
    }
}
