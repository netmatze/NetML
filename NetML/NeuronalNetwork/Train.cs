using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.NeuronalNetworks
{
    public class Train
    {
        private double error;

        public double Error
        {
            get { return error; }
            set { error = value; }
        }

        private Backpropagation backpropagation;

        public Train(Backpropagation backpropagation)
        {
            this.backpropagation = backpropagation;
        }

        public void Iteration()
        {
            
        }
    }
}
