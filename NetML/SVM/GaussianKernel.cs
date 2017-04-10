using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.SupportVectorMachine
{
    // Radial basis function kernel (Gaussian Kernel)
    // K(x, xx) = Math.Exp( -1 * Math.Pow(Norm(x - xx),2) / 2 * Math.Pow(sigma,2));

    public class GaussianKernel : Kernel
    {
        private double sigma;

        public GaussianKernel(double sigma)
        {
            this.sigma = sigma;
        }

        public override double Calculate(double[] firstVector, double[] secondVector)
        {
            return Math.Exp(-1 * Math.Pow(Norm(Subtract(firstVector, secondVector)), 2) 
                / 2 * Math.Pow(sigma, 2));
        }

        protected double[] Subtract(double[] firstVector, double[] secondVector)
        {
            var subtract = new double[firstVector.Length];
            for (int i = 0; i < firstVector.Length; i++)
            {
                subtract[i] = firstVector[i] - secondVector[i];
            }
            return subtract;
        }

        protected double Norm(double[] vector)
        {
            var norm = 0.0;
            for (int i = 0; i < vector.Length; i++)
            {
                norm += Math.Pow(vector[i],2);
            }
            return Math.Sqrt(norm);
        }
    }
}
