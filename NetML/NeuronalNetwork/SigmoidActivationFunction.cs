using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.NeuronalNetworks
{
    public class SigmoidActivationFunction : IActivationFunction
    {
        public double ActivationFunction(double activationValue)
        {
            return 1 / (1 + Math.Exp((-1) * activationValue));
        }
    }
}
