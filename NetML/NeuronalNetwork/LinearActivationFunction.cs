using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.NeuronalNetworks
{
    public class LinearActivationFunction : IActivationFunction
    {
        public double ActivationFunction(double activationValue)
        {
            if (activationValue > 0)
                return 1.0;
            else
                return 0.0;
        }
    }
}
