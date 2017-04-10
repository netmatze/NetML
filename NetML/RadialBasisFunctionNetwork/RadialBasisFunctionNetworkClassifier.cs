using NetML.NeuronalNetworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.RadialBasisFunctionNetwork
{
    public class RadialBasisFunctionNetworkClassifier : Classification
    {
        private RadialBasisFunctionNeuronalNetwork radialBasisFunctionNeuronalNetwork;
        private List<Tuple<double[], double>> dataOneOutput;
        private List<Tuple<double[], double[]>> data;

        public RadialBasisFunctionNetworkClassifier(List<Tuple<double[], double>> data,
            int inputNeurons, int outputNeurons, int firstHiddenLayerNeurons,
            int evolutions = 1000, double learningRate = 0.5)
        {
            var neuronCounter = new OneHiddenLayerNeuronCounter(inputNeurons, outputNeurons, firstHiddenLayerNeurons);
            this.dataOneOutput = data;
            this.radialBasisFunctionNeuronalNetwork = new RadialBasisFunctionNeuronalNetwork(neuronCounter, evolutions, learningRate);
        }

        public RadialBasisFunctionNetworkClassifier(List<Tuple<double[], double[]>> data,
            int inputNeurons, int outputNeurons, int firstHiddenLayerNeurons,
            int evolutions = 1000, double learningRate = 0.5)
        {
            var neuronCounter = new OneHiddenLayerNeuronCounter(inputNeurons, outputNeurons, firstHiddenLayerNeurons);
            this.data = data;
            this.radialBasisFunctionNeuronalNetwork = new RadialBasisFunctionNeuronalNetwork(neuronCounter, evolutions, learningRate);
        }

        public override void Train()
        {
            if (data != null)
            {
                this.radialBasisFunctionNeuronalNetwork.Train(data);
            }
            else if (dataOneOutput != null)
            {
                this.radialBasisFunctionNeuronalNetwork.Train(dataOneOutput);
            }
        }

        public override double Classify(double[] inputValues)
        {
            var result = this.radialBasisFunctionNeuronalNetwork.Classify(inputValues);
            return result.PredictedValue;
        }

        public double[] ClassifiyMultibleResultValue(double[] inputValues)
        {
            var result = this.radialBasisFunctionNeuronalNetwork.Classify(inputValues);
            return result.OutputValues;
        }
    }
}
