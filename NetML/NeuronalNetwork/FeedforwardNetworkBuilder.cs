using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.NeuronalNetworks
{
    public class FeedforwardNetworkBuilder
    {
        public FeedforwardNeuronalNetwork CreateNeuronalNetwork(
            int inputNeurons, int outputNeurons, int evolutions = 1000, double learningRate = 0.5)
        {
            var neuronCounter = new NeuronCounter(inputNeurons, outputNeurons);
            return new FeedforwardNeuronalNetwork(neuronCounter, evolutions, learningRate);
        }

        public FeedforwardNeuronalNetwork CreateNeuronalNetwork(
            int inputNeurons, int outputNeurons, int firstHiddenLayerNeurons, 
            int evolutions = 1000, double learningRate = 0.5)
        {
            var neuronCounter = new OneHiddenLayerNeuronCounter(inputNeurons, outputNeurons, firstHiddenLayerNeurons);
            return new FeedforwardNeuronalNetwork(neuronCounter, evolutions, learningRate);
        }

        public FeedforwardNeuronalNetwork CreateNeuronalNetwork(
            int inputNeurons, int outputNeurons, int firstHiddenLayerNeurons, 
            int secondHiddenLayerNeurons, int evolutions = 1000, double learningRate = 0.5)
        {
            var neuronCounter = new TwoHiddenLayerNeuronCounter(inputNeurons, outputNeurons, firstHiddenLayerNeurons, secondHiddenLayerNeurons);
            return new FeedforwardNeuronalNetwork(neuronCounter, evolutions, learningRate);
        }
    }
}
