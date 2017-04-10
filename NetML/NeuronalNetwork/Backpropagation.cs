using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.NeuronalNetworks
{
    public class Backpropagation
    {
        private NeuronalNetwork neuronalNetwork;

        public NeuronalNetwork NeuronalNetwork_
        {
            get { return neuronalNetwork; }
            set { neuronalNetwork = value; }
        }

        private double[][] input;

        public double[][] Input
        {
            get { return input; }
            set { input = value; }
        }

        private double[][] ideal;

        public double[][] Ideal
        {
            get { return ideal; }
            set { ideal = value; }
        }

        private double learningRate;

        public double LearningRate
        {
            get { return learningRate; }
            set { learningRate = value; }
        }

        private double momentum;

        public double Momentum
        {
            get { return momentum; }
            set { momentum = value; }
        }

        public Train Backpropagate(NeuronalNetwork neuronalNetwork, 
            double[][] input, double[][] ideal, double learningRate, double momentum)
        {
            this.neuronalNetwork = neuronalNetwork;
            this.input = input;
            this.ideal = ideal;
            this.learningRate = learningRate;
            this.momentum = momentum;
            return new Train(this);
        }
    }
}
