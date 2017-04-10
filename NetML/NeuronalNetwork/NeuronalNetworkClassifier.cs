using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.NeuronalNetworks
{
    public class NeuronalNetworkClassifier : Classification
    {
        private FeedforwardNeuronalNetwork feedforwardNeuronalNetwork;
        private List<Tuple<double[], double>> dataOneOutput;
        private List<Tuple<double[], double[]>> data;

        public NeuronalNetworkClassifier(List<Tuple<double[], double>> data,
            int inputNeurons, int outputNeurons, int evolutions = 1000, double learningRate = 0.5)
        {
            var neuronCounter = new NeuronCounter(inputNeurons, outputNeurons);
            this.dataOneOutput = data;
            this.feedforwardNeuronalNetwork = new FeedforwardNeuronalNetwork(neuronCounter, evolutions, learningRate);
        }

        public NeuronalNetworkClassifier(List<Tuple<double[], double>> data,
            int inputNeurons, int outputNeurons, int firstHiddenLayerNeurons,
            int evolutions = 1000, double learningRate = 0.5, NeuronalNetworkMode neuronalNetworkMode = NeuronalNetworkMode.Standard)
        {
            var neuronCounter = new OneHiddenLayerNeuronCounter(inputNeurons, outputNeurons, firstHiddenLayerNeurons);
            this.dataOneOutput = data;
            this.feedforwardNeuronalNetwork = new FeedforwardNeuronalNetwork(neuronCounter, evolutions, learningRate, neuronalNetworkMode);
        }

        public NeuronalNetworkClassifier(List<Tuple<double[], double>> data,
            int inputNeurons, int outputNeurons, int firstHiddenLayerNeurons,
            int secondHiddenLayerNeurons, int evolutions = 1000, double learningRate = 0.5,
            NeuronalNetworkMode neuronalNetworkMode = NeuronalNetworkMode.Standard)
        {
            var neuronCounter = new TwoHiddenLayerNeuronCounter(inputNeurons, outputNeurons, firstHiddenLayerNeurons, secondHiddenLayerNeurons);
            this.dataOneOutput = data;
            this.feedforwardNeuronalNetwork = new FeedforwardNeuronalNetwork(neuronCounter, evolutions, learningRate, neuronalNetworkMode);
        }

        public NeuronalNetworkClassifier(List<Tuple<double[], double[]>> data,
            int inputNeurons, int outputNeurons, int evolutions = 1000, double learningRate = 0.5)
        {
            var neuronCounter = new NeuronCounter(inputNeurons, outputNeurons);
            this.data = data;
            this.feedforwardNeuronalNetwork = new FeedforwardNeuronalNetwork(neuronCounter, evolutions, learningRate);
        }

        public NeuronalNetworkClassifier(List<Tuple<double[], double[]>> data,
            int inputNeurons, int outputNeurons, int firstHiddenLayerNeurons,
            int evolutions = 1000, double learningRate = 0.5,
            NeuronalNetworkMode neuronalNetworkMode = NeuronalNetworkMode.Standard)
        {
            var neuronCounter = new OneHiddenLayerNeuronCounter(inputNeurons, outputNeurons, firstHiddenLayerNeurons);
            this.data = data;
            this.feedforwardNeuronalNetwork = new FeedforwardNeuronalNetwork(neuronCounter, evolutions, learningRate, neuronalNetworkMode);
        }

        public NeuronalNetworkClassifier(List<Tuple<double[], double[]>> data,
            int inputNeurons, int outputNeurons, int firstHiddenLayerNeurons,
            int secondHiddenLayerNeurons, int evolutions = 1000, double learningRate = 0.5,
            NeuronalNetworkMode neuronalNetworkMode = NeuronalNetworkMode.Standard)
        {
            var neuronCounter = new TwoHiddenLayerNeuronCounter(inputNeurons, outputNeurons, firstHiddenLayerNeurons, secondHiddenLayerNeurons);
            this.data = data;
            this.feedforwardNeuronalNetwork = new FeedforwardNeuronalNetwork(neuronCounter, evolutions, learningRate, neuronalNetworkMode);
        }

        public NeuronalNetworkClassifier(List<Tuple<double[], double[]>> data,
          int inputNeurons, int outputNeurons, int firstHiddenLayerNeurons,
          int secondHiddenLayerNeurons, int thirdHiddenLayerNeurons, int evolutions = 1000, double learningRate = 0.5,
          NeuronalNetworkMode neuronalNetworkMode = NeuronalNetworkMode.Standard)
        {
            var neuronCounter = new ThirdHiddenLayerNeuronCounter(inputNeurons, outputNeurons, firstHiddenLayerNeurons, secondHiddenLayerNeurons, thirdHiddenLayerNeurons);
            this.data = data;
            this.feedforwardNeuronalNetwork = new FeedforwardNeuronalNetwork(neuronCounter, evolutions, learningRate, neuronalNetworkMode);
        }

        public override void Train()
        {
            if (data != null)
            {
                this.feedforwardNeuronalNetwork.Train(data);
            }
            else if(dataOneOutput != null)
            {
                this.feedforwardNeuronalNetwork.Train(dataOneOutput);
            }
        }

        public override double Classify(double[] inputValues)
        {
            var result = this.feedforwardNeuronalNetwork.Classify(inputValues);
            return result.PredictedValue;
        }

        public double[] ClassifiyMultibleResultValue(double[] inputValues)
        {
            var result = this.feedforwardNeuronalNetwork.Classify(inputValues);
            return result.OutputValues;
        }
    }
}
