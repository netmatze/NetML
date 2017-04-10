using NetML.NeuronalNetworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.RadialBasisFunctionNetwork
{
    public class RadialBasisFunctionLayer
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

        private double[][] cetroids;

        public double[][] Cetroids
        {
            get { return cetroids; }
        }

        private double[] sigmoids;

        public double[] Sigmoids
        {
            get { return sigmoids; }
            set { sigmoids = value; }
        }

        private int p;

        public int P
        {
            get { return p; }            
        }

        private IActivationFunction activationFunction_;

        internal IActivationFunction ActivationFunction_
        {
            get { return activationFunction_; }
            set { activationFunction_ = value; }
        }

        public RadialBasisFunctionLayer(int neurons, int layer)
        {
            this.layer = layer;
            this.neurons = neurons;
            this.values = new double[neurons];
            this.cetroids = new double[neurons][];
            this.sigmoids = new double[neurons];
            this.p = 2;
            this.activationFunction_ = new SigmoidActivationFunction();
        }

        public RadialBasisFunctionLayer(IActivationFunction activationFunction, int neurons, int layer)
            : this(neurons, layer)
        {
            this.activationFunction_ = activationFunction;
        }

        public RadialBasisFunctionLayer(IActivationFunction activationFunction, int neurons, int layer, int p)
            : this(activationFunction, neurons, layer)
        {
            this.p = p;
        }
    }
}
