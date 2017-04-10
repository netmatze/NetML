using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.NeuronalNetworks
{
    public class FeedforwardLayer
    {
        private int layer;

        public int Layer
        {
            get { return layer; }           
        }

        private int neurons;

        public int Neurons
        {
            get { return neurons; }           
        }

        private double[] values;

        public double[] Values
        {
            get { return values; }            
        }

        private double[] sigmas;

        public double[] Sigmas
        {
            get { return sigmas; }
        }

        private IActivationFunction activationFunction_;

        internal IActivationFunction ActivationFunction_
        {
            get { return activationFunction_; }
            set { activationFunction_ = value; }
        }

        public FeedforwardLayer(int neurons, int layer)
        {
            this.layer = layer;
            this.neurons = neurons;
            this.values = new double[neurons];
            this.sigmas = new double[neurons];
            this.activationFunction_ = new LinearActivationFunction();
        }

        public FeedforwardLayer(IActivationFunction activationFunction, int neurons, int layer)
            : this(neurons, layer)
        {            
            this.activationFunction_ = activationFunction;
        }
    }
}
