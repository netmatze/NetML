using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.LogisticRegression
{   
    public class TanhCostFunction : IRegressionFunction
    {
        public double Calculate(double[] firstVector, double[] secondVector)
        {
            var sum = 0.0;
            var n = firstVector.Length;
            for (int i = 0; i < firstVector.Length; i++)
            {
                sum += firstVector[i] * secondVector[i];
            }
            return Math.Tanh(1 / n * sum);
        }
    }   
}
