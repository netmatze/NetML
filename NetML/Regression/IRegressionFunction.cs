using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.LogisticRegression
{
    public interface IRegressionFunction
    {
        double Calculate(double[] sigmas, double[] example);
    }
}
