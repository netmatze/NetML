using NetML.NeuronalNetworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.RadialBasisFunctionNetwork
{
    public class RadialBasisFunctionNeuronalNetwork
    {
        private NeuronCounter neuronCounter;
        private int evolutions;
        private double learningRate;
        private RadialBasisFunctionNetwork radialBasisFunctionNetwork = new RadialBasisFunctionNetwork();
        private IActivationFunction activationFunction = new SigmoidActivationFunction();
        private int inputValuesCount;
        private int outputValuesCount;

        public IActivationFunction ActivationFunction
        {
            get { return activationFunction; }
            set { activationFunction = value; }
        }

        public RadialBasisFunctionNeuronalNetwork(OneHiddenLayerNeuronCounter neuronCounter, int evolutions = 1000, 
            double learningRate = 0.5)
        {            
            this.neuronCounter = neuronCounter;
            this.evolutions = evolutions;
            this.learningRate = learningRate;
            this.inputValuesCount = neuronCounter.InputNeuronCount;
            this.outputValuesCount = neuronCounter.OutputNeuronCount;
            this.radialBasisFunctionNetwork.AddInputLayer(new RadialBasisFunctionLayer(activationFunction, neuronCounter.InputNeuronCount, 0));
            this.radialBasisFunctionNetwork.AddHiddenLayer(new RadialBasisFunctionLayer(activationFunction, neuronCounter.FirstLayerHiddenNeuronCount, 1));
            this.radialBasisFunctionNetwork.AddOutputLayer(new RadialBasisFunctionLayer(activationFunction, neuronCounter.OutputNeuronCount, 1));
            this.radialBasisFunctionNetwork.LearningRate = learningRate;
            this.radialBasisFunctionNetwork.Evolutions = evolutions;
            this.radialBasisFunctionNetwork.RandomFillWeightMatrix();
        }

        public void Train(List<Tuple<double[], double[]>> data)
        {
            radialBasisFunctionNetwork.CalculateCentroidsAndSigmoids(data);
            for (int j = 0; j < evolutions; j++)
            {
                foreach (var item in data)
                {
                    radialBasisFunctionNetwork.Train(item.Item1, item.Item2);
                    //Debug.WriteLine(" InputValue {0} = CalculatedOutput {1} - OutputValue: {2} ",
                    //    item.Item1[0],
                    //    feedforwardNetwork.outputFeedforwardLayer.Values[0],
                    //    item.Item2[0]
                    //    );
                }
            }
        }

        public void Train(List<Tuple<double[], double>> data)
        {
            radialBasisFunctionNetwork.CalculateCentroidsAndSigmoids(data);
            for (int j = 0; j < evolutions; j++)
            {
                foreach (var item in data)
                {
                    radialBasisFunctionNetwork.Train(item.Item1, item.Item2);
                    //Debug.WriteLine(" InputValue {0} = CalculatedOutput {1} - OutputValue: {2} ",
                    //    item.Item1[0],
                    //    feedforwardNetwork.outputFeedforwardLayer.Values[0],
                    //    item.Item2
                    //    );
                }
            }
        }

        public void Train(double[] inputValues, double[] outputValues)
        {
            for (int i = 0; i < evolutions; i++)
            {
                radialBasisFunctionNetwork.Train(inputValues, outputValues);
            }
        }

        public FeedforwardNeuronalNetworkResult<double> Classify(double[] inputValues)
        {
            var classifier = radialBasisFunctionNetwork.Classify(inputValues);
            FeedforwardNeuronalNetworkResult<double> feedforwardNeuronalNetworkResult = new FeedforwardNeuronalNetworkResult<double>();
            feedforwardNeuronalNetworkResult.OutputValues = classifier;
            for (int i = 0; i < classifier.Length; i++)
            {
                var actualResult = classifier[i];
                if (feedforwardNeuronalNetworkResult.MaxValue < actualResult)
                {
                    feedforwardNeuronalNetworkResult.MaxValue = actualResult;
                    feedforwardNeuronalNetworkResult.MaxValueOutputPosition = i;
                }
            }
            return feedforwardNeuronalNetworkResult;
        }
    }
}
