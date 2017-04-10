using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.NeuronalNetworks
{
    public class TangensHyperbolicusActivationFunction : IActivationFunction
    {
        public double ActivationFunction(double activationValue)
        {
            //return (Math.Exp(2 * activationValue) - 1) / (Math.Exp(2 * activationValue) + 1);
            return 1 - 2 / (Math.Exp((2 * activationValue)) + 1);
        }
    }
}
