using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.NeuronalNetworks
{
    public interface IActivationFunction
    {
        double ActivationFunction(double activationValue);
    }
}
