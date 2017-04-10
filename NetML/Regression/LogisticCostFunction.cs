using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.LogisticRegression
{
    public class LogisticCostFunction : IRegressionFunction
    {
        public double Calculate(double[] sigmas, double[] example)
        {
            var sum = 0.0;
            for (int i = 0; i < sigmas.Length; i++)
            {
                if (i == 0)
                {
                    sum += sigmas[i];
                }
                else
                {
                    sum += sigmas[i] * example[i - 1];
                }
            }
            return 1.0 / (1.0 + Math.Pow(Math.E, sum * -1.0));
        }
    }
}
