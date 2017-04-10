using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace NetML.NeuronalNetworks
{
    public class FeedforwardNeuronalNetwork
    {
        private NeuronalCounterMode mode;

        public NeuronalCounterMode Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        private NeuronCounter neuronCounter;
        private int evolutions;
        private double learningRate;
        private NeuronalNetwork neuronalNetwork = new FeedforwardNetwork();
        private IActivationFunction activationFunction = new SigmoidActivationFunction();
        private NeuronalNetworkMode neuronalNetworkMode;
        private int inputValuesCount;
        private int outputValuesCount;

        public IActivationFunction ActivationFunction
        {
            get { return activationFunction; }
            set { activationFunction = value; }
        }

        public FeedforwardNeuronalNetwork(NeuronCounter neuronCounter, int evolutions = 1000, 
            double learningRate = 0.5)
        {
            this.mode = NeuronalCounterMode.ZeroHiddenLayer;
            this.neuronCounter = neuronCounter;
            this.evolutions = evolutions;
            this.learningRate = learningRate;
            this.inputValuesCount = neuronCounter.InputNeuronCount;
            this.outputValuesCount = neuronCounter.OutputNeuronCount;
            this.neuronalNetwork.AddInputLayer(new FeedforwardLayer(activationFunction, neuronCounter.InputNeuronCount, 0));
            this.neuronalNetwork.AddOutputLayer(new FeedforwardLayer(activationFunction, neuronCounter.OutputNeuronCount, 1));
            this.neuronalNetwork.LearningRate = learningRate;
            this.neuronalNetwork.Evolutions = evolutions;
            this.neuronalNetwork.NeuronalNetworkMode = NeuronalNetworkMode.Standard;
            this.neuronalNetwork.RandomFillWeightMatrix();
        }

        public FeedforwardNeuronalNetwork(OneHiddenLayerNeuronCounter neuronCounter, int evolutions = 1000, double learningRate = 0.5, 
            NeuronalNetworkMode neuronalNetworkMode = NeuronalNetworkMode.Standard)
        {
            this.mode = NeuronalCounterMode.OneHiddenLayer;
            this.neuronalNetworkMode = neuronalNetworkMode;
            this.neuronCounter = neuronCounter;
            this.evolutions = evolutions;
            this.learningRate = learningRate;
            this.inputValuesCount = neuronCounter.InputNeuronCount;
            this.outputValuesCount = neuronCounter.OutputNeuronCount;
            neuronalNetwork = NeuronalNetworkModeFactory.CreateInstance(neuronalNetworkMode);
            this.neuronalNetwork.NeuronalNetworkMode = neuronalNetworkMode;
            this.neuronalNetwork.AddInputLayer(new FeedforwardLayer(activationFunction, neuronCounter.InputNeuronCount, 0));
            this.neuronalNetwork.AddHiddenLayer(new FeedforwardLayer(activationFunction, neuronCounter.FirstLayerHiddenNeuronCount, 1));
            this.neuronalNetwork.AddOutputLayer(new FeedforwardLayer(activationFunction, neuronCounter.OutputNeuronCount, 2));
            this.neuronalNetwork.LearningRate = learningRate;
            this.neuronalNetwork.Evolutions = evolutions;            
            this.neuronalNetwork.RandomFillWeightMatrix();
        }

        public FeedforwardNeuronalNetwork(TwoHiddenLayerNeuronCounter neuronCounter, int evolutions = 1000, double learningRate = 0.5,
            NeuronalNetworkMode neuronalNetworkMode = NeuronalNetworkMode.Standard)
        {
            this.mode = NeuronalCounterMode.TwoHiddenLayer;
            this.neuronalNetworkMode = neuronalNetworkMode;
            this.neuronCounter = neuronCounter;
            this.evolutions = evolutions;
            this.learningRate = learningRate;
            this.inputValuesCount = neuronCounter.InputNeuronCount;
            this.outputValuesCount = neuronCounter.OutputNeuronCount;
            neuronalNetwork = NeuronalNetworkModeFactory.CreateInstance(neuronalNetworkMode);
            this.neuronalNetwork.NeuronalNetworkMode = neuronalNetworkMode;
            this.neuronalNetwork.AddInputLayer(new FeedforwardLayer(activationFunction, neuronCounter.InputNeuronCount, 0));
            this.neuronalNetwork.AddHiddenLayer(new FeedforwardLayer(activationFunction, neuronCounter.FirstLayerHiddenNeuronCount, 1));
            this.neuronalNetwork.AddHiddenLayer(new FeedforwardLayer(activationFunction, neuronCounter.SecondLayerHiddenNeuronCount, 2));
            this.neuronalNetwork.AddOutputLayer(new FeedforwardLayer(activationFunction, neuronCounter.OutputNeuronCount, 3));
            this.neuronalNetwork.LearningRate = learningRate;
            this.neuronalNetwork.Evolutions = evolutions;
            
            this.neuronalNetwork.RandomFillWeightMatrix();
        }

        public FeedforwardNeuronalNetwork(ThirdHiddenLayerNeuronCounter neuronCounter, int evolutions = 1000, double learningRate = 0.5,
            NeuronalNetworkMode neuronalNetworkMode = NeuronalNetworkMode.Standard)
        {
            this.mode = NeuronalCounterMode.ThreeHiddenLayer;
            this.neuronalNetworkMode = neuronalNetworkMode;
            this.neuronCounter = neuronCounter;
            this.evolutions = evolutions;
            this.learningRate = learningRate;
            this.inputValuesCount = neuronCounter.InputNeuronCount;
            this.outputValuesCount = neuronCounter.OutputNeuronCount;
            neuronalNetwork = NeuronalNetworkModeFactory.CreateInstance(neuronalNetworkMode);
            this.neuronalNetwork.NeuronalNetworkMode = neuronalNetworkMode;
            this.neuronalNetwork.AddInputLayer(new FeedforwardLayer(activationFunction, neuronCounter.InputNeuronCount, 0));
            this.neuronalNetwork.AddHiddenLayer(new FeedforwardLayer(activationFunction, neuronCounter.FirstLayerHiddenNeuronCount, 1));
            this.neuronalNetwork.AddHiddenLayer(new FeedforwardLayer(activationFunction, neuronCounter.SecondLayerHiddenNeuronCount, 2));
            this.neuronalNetwork.AddHiddenLayer(new FeedforwardLayer(activationFunction, neuronCounter.ThirdLayerHiddenNeuronCount, 3));
            this.neuronalNetwork.AddOutputLayer(new FeedforwardLayer(activationFunction, neuronCounter.OutputNeuronCount, 4));
            this.neuronalNetwork.LearningRate = learningRate;
            this.neuronalNetwork.Evolutions = evolutions;
            this.neuronalNetwork.NeuronalNetworkMode = neuronalNetworkMode;
            this.neuronalNetwork.RandomFillWeightMatrix();
        }

        public void Train(double[] inputValues, double[] outputValues)
        {
            for (int i = 0; i < evolutions; i++)
            {
                neuronalNetwork.Train(inputValues, outputValues);
            }
        }

        private double[] ConvertDoubleToBinaryDoubleArray(double value, int valueCount)
        {
            double[] inputValueConverted = new double[valueCount];
            int tempValue = (int)value;
            for (int i = valueCount; i >= 0; i--)
            {
                var binaryValue = (int) Math.Pow(2,i);
                if (tempValue / binaryValue > 0)
                {
                    inputValueConverted[i] = 1.0;
                    tempValue -= binaryValue;
                }                
            }
            return inputValueConverted;
        }

        private double[] ConvertDoubleToDoubleArray(double value, int valueCount)
        {
            double[] inputValueConverted = new double[valueCount];
            int tempValue = (int)value;
            for (int i = valueCount; i >= 1; i--)
            {
                if (tempValue / i > 0)
                {
                    inputValueConverted[i - 1] = 1.0;
                    tempValue -= i;
                }
            }
            return inputValueConverted;           
        }

        public void ConvertAndTrain(List<double[]> inputValues, List<double[]> outputValues)
        {
            for (int j = 0; j < evolutions; j++)
            {
                for (int i = 0; i < inputValues.Count; i++)
                {                    
                    var inputValue = inputValues[i][0];
                    var inputValuesConverted = ConvertDoubleToDoubleArray(inputValue, this.inputValuesCount);
                    var outputValue = outputValues[i][0];
                    var outputValuesConverted = ConvertDoubleToDoubleArray(outputValue, this.outputValuesCount);
                    neuronalNetwork.Train(inputValuesConverted, outputValuesConverted);
                    //Debug.WriteLine(" InputValue {0} = CalculatedOutput {1} - OutputValue: {2} ",
                    //    inputValues[i][0],
                    //    feedforwardNetwork.outputFeedforwardLayer.Values[0],
                    //    outputValues[i][0]
                    //    );
                }
            }
        }

        public void Train(List<double[]> inputValues, List<double[]> outputValues)
        {
            for (int j = 0; j < evolutions; j++)
            {
                for (int i = 0; i < inputValues.Count; i++)
                {
                    neuronalNetwork.Train(inputValues[i], outputValues[i]);
                    //Debug.WriteLine(" InputValue {0} = CalculatedOutput {1} - OutputValue: {2} ",
                    //    inputValues[i][0],
                    //    feedforwardNetwork.outputFeedforwardLayer.Values[0],
                    //    outputValues[i][0]
                    //    );    
                }
            }
        }

        public void Train(List<Tuple<double[], double[]>> data)
        {
            for (int j = 0; j < evolutions; j++)
            {
                foreach (var item in data)
                {
                    neuronalNetwork.Train(item.Item1, item.Item2);
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
            for (int j = 0; j < evolutions; j++)
            {
                foreach (var item in data)
                {
                    neuronalNetwork.Train(item.Item1, item.Item2);
                    //Debug.WriteLine(" InputValue {0} = CalculatedOutput {1} - OutputValue: {2} ",
                    //    item.Item1[0],
                    //    feedforwardNetwork.outputFeedforwardLayer.Values[0],
                    //    item.Item2
                    //    );
                }
            }
        }

        public void Train(double[] inputValues, int[] outputValues)
        {
            for (int i = 0; i < evolutions; i++)
            {
                neuronalNetwork.Train(inputValues, outputValues);
            }
        }

        public void Train(int[] inputValues, int[] outputValues)
        {
            for (int i = 0; i < evolutions; i++)
            {
                neuronalNetwork.Train(inputValues, outputValues);
            }
        }

        public FeedforwardNeuronalNetworkResult<double> Classify(double[] inputValues)
        {
            var classifier = neuronalNetwork.Classify(inputValues);
            FeedforwardNeuronalNetworkResult<double> feedforwardNeuronalNetworkResult = new FeedforwardNeuronalNetworkResult<double>();
            feedforwardNeuronalNetworkResult.OutputValues = classifier;
            for (int i = 0; i < classifier.Length; i++)
            {
                var actualResult = classifier[i];
                if(feedforwardNeuronalNetworkResult.MaxValue < actualResult)
                {
                    feedforwardNeuronalNetworkResult.MaxValue = actualResult;
                    feedforwardNeuronalNetworkResult.MaxValueOutputPosition = i;
                }
            }
            return feedforwardNeuronalNetworkResult;
        }

        public FeedforwardNeuronalNetworkResult<int> Classify(int[] inputValues)
        {
            return null;
        }

        public FeedforwardNeuronalNetworkResult<double> Predict(double[] inputValues)
        {
            var classifier = neuronalNetwork.Classify(inputValues);
            FeedforwardNeuronalNetworkResult<double> feedforwardNeuronalNetworkResult =
                new FeedforwardNeuronalNetworkResult<double>();
            feedforwardNeuronalNetworkResult.OutputValues = classifier;
            feedforwardNeuronalNetworkResult.PredictedValue = classifier[0];
            return feedforwardNeuronalNetworkResult;
        }

        public FeedforwardNeuronalNetworkResult<double> Predict(int[] inputValues)
        {
            return null;
        }
    }
}
