using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace NetML.NeuronalNetworks
{
    public class FeedforwardNetwork : NeuronalNetwork
    {
        private int hiddenLayerCounter = 1;
        private FeedforwardLayer lastFeedforwardLayer;

        public override void AddInputLayer(FeedforwardLayer feedforwardLayer)
        {
            base.inputFeedforwardLayer = feedforwardLayer;
        }

        public override void AddHiddenLayer(FeedforwardLayer feedforwardLayer)
        {
            base.hiddenFeedforwardLayers.Add(feedforwardLayer);
            lastFeedforwardLayer = feedforwardLayer;
            if(hiddenLayerCounter == 1)
                BuildWeightMatrix(feedforwardLayer);
            else
                BuildWeightMatrix(feedforwardLayer, base.hiddenFeedforwardLayers[hiddenLayerCounter - hiddenLayerCounter]);
            hiddenLayerCounter++;
        }

        public override void AddOutputLayer(FeedforwardLayer feedforwardLayer)
        {
            base.outputFeedforwardLayer = feedforwardLayer;
            BuildWeightMatrixOutputLayer(feedforwardLayer);
        }

        public override void BuildWeightMatrix(FeedforwardLayer feedforwardLayer)
        {
            weightMatrixes.Add(new Tuple<int,int,double[,]>(
                hiddenLayerCounter - 1, hiddenLayerCounter, new double[inputFeedforwardLayer.Neurons, feedforwardLayer.Neurons]));
        }

        public override void BuildWeightMatrix(FeedforwardLayer feedforwardLayer, FeedforwardLayer previousfeedforwardLayer)
        {
            weightMatrixes.Add(new Tuple<int, int, double[,]>(
                hiddenLayerCounter - 1, hiddenLayerCounter, new double[previousfeedforwardLayer.Neurons, feedforwardLayer.Neurons]));
        }

        public override void BuildWeightMatrixOutputLayer(FeedforwardLayer feedforwardLayer)
        {
            weightMatrixes.Add(new Tuple<int, int, double[,]>(
                hiddenLayerCounter - 1, hiddenLayerCounter, new double[lastFeedforwardLayer.Neurons, outputFeedforwardLayer.Neurons]));
        }

        public override void FillWeightMatrix()
        {
            var counter = 0;
            foreach (var matrix in weightMatrixes)
            {
                var array = matrix.Item3;
                if (counter == 0)
                {
                    array[0, 0] = 1;
                    array[0, 1] = 0.5;
                    array[1, 0] = 1;
                    array[1, 1] = 0.5;
                }
                else
                {
                    for (int i = 0; i <= array.GetUpperBound(0); i++)
                    {
                        for (int j = 0; j <= array.GetUpperBound(1); j++)
                        {
                            if (i == 0)
                                array[i, j] = 1.0;
                            else
                                array[i, j] = 0.5;
                        }
                    }
                }
                counter++;
            }
        }

        public override void RandomFillWeightMatrix()
        {
            Random rand = new Random(100);
            foreach(var matrix in weightMatrixes)
            {
                var array = matrix.Item3;
                for(int i = 0; i <= array.GetUpperBound(0); i++)
                {
                    for(int j = 0; j <= array.GetUpperBound(1); j++)
                    {
                        array[i, j] = rand.NextDouble();
                    }
                }
            }
        }
        
        public override double[] Classify(int[] inputValues)
        {
            int counter = 0;
            foreach (var inputValue in inputValues)
            {
                inputFeedforwardLayer.Values[counter] = inputValue;
                counter++;
            }
            var startFeedforwardLayer = inputFeedforwardLayer;
            foreach (var layer in hiddenFeedforwardLayers)
            {
                ClassifyLayer(startFeedforwardLayer, layer);
                startFeedforwardLayer = layer;
            }
            ClassifyLayer(startFeedforwardLayer, outputFeedforwardLayer);          
            return outputFeedforwardLayer.Values;
        }

        public override double[] Classify(double[] inputValues)
        {
            int counter = 0;
            foreach (var inputValue in inputValues)
            {
                inputFeedforwardLayer.Values[counter] = inputValue;
                counter++;
            }
            var startFeedforwardLayer = inputFeedforwardLayer;
            foreach (var layer in hiddenFeedforwardLayers)
            {
                ClassifyLayer(startFeedforwardLayer, layer);
                startFeedforwardLayer = layer;
            }
            ClassifyLayer(startFeedforwardLayer, outputFeedforwardLayer);           
            return outputFeedforwardLayer.Values;
        }

        private void ClassifyLayer(FeedforwardLayer startfeedforwardLayer, FeedforwardLayer endfeedforwardLayer)
        {
            double[,] weightMatrix = null;
            foreach (var matrix in weightMatrixes)
            {
                if (matrix.Item1 == startfeedforwardLayer.Layer)
                {
                    weightMatrix = matrix.Item3;
                }
            }
            for (int i = 0; i < endfeedforwardLayer.Values.Count(); i++)
            {
                double summe = 0.0;
                for (int j = 0; j < startfeedforwardLayer.Values.Count(); j++)
                {
                    summe += startfeedforwardLayer.Values[j] * weightMatrix[j, i]; //j, i];                    
                }
                endfeedforwardLayer.Values[i] = endfeedforwardLayer.ActivationFunction_.ActivationFunction(summe);
            }            
        }

        private void Calculate(double[] inputValues, int[] outputValues)
        {
            int counter = 0;
            foreach (var inputValue in inputValues)
            {
                inputFeedforwardLayer.Values[counter] = inputValue;
                counter++;
            }
            var startFeedforwardLayer = inputFeedforwardLayer;           
            foreach (var layer in hiddenFeedforwardLayers)
            {
                CalculateLayer(startFeedforwardLayer, layer);                
                startFeedforwardLayer = layer;
            }
            CalculateLayer(startFeedforwardLayer, outputFeedforwardLayer);
            Backpropagate(outputFeedforwardLayer, startFeedforwardLayer, outputValues, 0);
        }

        private void Calculate(double[] inputValues, double[] outputValues)
        {
            int counter = 0;
            foreach (var inputValue in inputValues)
            {
                inputFeedforwardLayer.Values[counter] = inputValue;
                counter++;
            }
            var startFeedforwardLayer = inputFeedforwardLayer;
            foreach (var layer in hiddenFeedforwardLayers)
            {
                CalculateLayer(startFeedforwardLayer, layer);                
                startFeedforwardLayer = layer;
            }
            CalculateLayer(startFeedforwardLayer, outputFeedforwardLayer);
            Backpropagate(outputFeedforwardLayer, startFeedforwardLayer, outputValues, 0);            
        }

        private void Calculate(double[] inputValues, double outputValue)
        {
            int counter = 0;
            foreach (var inputValue in inputValues)
            {
                inputFeedforwardLayer.Values[counter] = inputValue;
                counter++;
            }
            var startFeedforwardLayer = inputFeedforwardLayer;
            foreach (var layer in hiddenFeedforwardLayers)
            {
                CalculateLayer(startFeedforwardLayer, layer);                
                startFeedforwardLayer = layer;
            }
            CalculateLayer(startFeedforwardLayer, outputFeedforwardLayer);
            Backpropagate(outputFeedforwardLayer, startFeedforwardLayer, outputValue, 0);            
        }

        private void Calculate(int[] inputValues, int[] outputValues)
        {
            int counter = 0;
            foreach (var inputValue in inputValues)
            {
                inputFeedforwardLayer.Values[counter] = inputValue;
                counter++;
            }
            var startFeedforwardLayer = inputFeedforwardLayer;
            foreach (var layer in hiddenFeedforwardLayers)
            {
                CalculateLayer(startFeedforwardLayer, layer);             
                startFeedforwardLayer = layer;
            }
            CalculateLayer(startFeedforwardLayer, outputFeedforwardLayer);
            Backpropagate(outputFeedforwardLayer, startFeedforwardLayer, outputValues, 0);            
        }

        private void CalculateLayer(FeedforwardLayer startfeedforwardLayer, FeedforwardLayer endfeedforwardLayer)
        {
            double[,] weightMatrix = null;
            foreach(var matrix in weightMatrixes)
            {
                if(matrix.Item1 == startfeedforwardLayer.Layer)
                {
                    weightMatrix = matrix.Item3;
                }
            }
            for (int i = 0; i < endfeedforwardLayer.Values.Count(); i++)
            {
                double summe = 0.0;
                for(int j = 0; j < startfeedforwardLayer.Values.Count(); j++)
                {                   
                    summe += startfeedforwardLayer.Values[j] * weightMatrix[j, i]; //j, i];                                        
                }
                endfeedforwardLayer.Values[i] = endfeedforwardLayer.ActivationFunction_.ActivationFunction(summe);
            }
        }

        private void Backpropagate(FeedforwardLayer startfeedforwardLayer,
            FeedforwardLayer endfeedforwardLayer, int[] outputValues, int layer)
        {
            //FeedforwardLayer endfeedforwardLayer = hiddenFeedforwardLayers[0];
            double[,] weightMatrix = null;
            foreach (var matrix in weightMatrixes)
            {
                if (matrix.Item2 == startfeedforwardLayer.Layer)
                {
                    weightMatrix = matrix.Item3;
                    break;
                }
            }
            for (int i = 0; i < startfeedforwardLayer.Values.Count(); i++)
            {
                for(int j = 0; j < endfeedforwardLayer.Values.Count(); j++)
                {                   
                    var tk = (double)outputValues[i];
                    var ok = startfeedforwardLayer.Values[i];
                    var oj = endfeedforwardLayer.Values[j];
                    var sigmak = ok * (1 - ok) * (tk - ok);
                    startfeedforwardLayer.Sigmas[i] = sigmak;
                    var deltaWk = LearningRate * sigmak * oj;                                  
                    weightMatrix[j, i] += deltaWk;
                }
            }
            if (endfeedforwardLayer.Layer > 0)
            {
                var inputFeedforwardLayer = this.inputFeedforwardLayer;
                if (hiddenFeedforwardLayers.Count > 1)
                {
                    var hiddenLayerCounter = hiddenFeedforwardLayers.Count - 2;
                    inputFeedforwardLayer = hiddenFeedforwardLayers[hiddenLayerCounter];
                }
                Backpropagate(inputFeedforwardLayer, endfeedforwardLayer, startfeedforwardLayer, ++layer);
            }
        }

        private void Backpropagate(FeedforwardLayer startfeedforwardLayer,
            FeedforwardLayer endfeedforwardLayer, double[] outputValues, int layer)
        {
            //FeedforwardLayer endfeedforwardLayer = hiddenFeedforwardLayers[0];
            double[,] weightMatrix = null;
            foreach (var matrix in weightMatrixes)
            {
                if (matrix.Item2 == startfeedforwardLayer.Layer)
                {
                    weightMatrix = matrix.Item3;
                    break;
                }
            }
            for (int i = 0; i < startfeedforwardLayer.Values.Count(); i++)
            {
                for (int j = 0; j < endfeedforwardLayer.Values.Count(); j++)
                {
                    var tk = (double)outputValues[i];
                    var ok = startfeedforwardLayer.Values[i];
                    var oj = endfeedforwardLayer.Values[j];
                    var sigmak = ok * (1 - ok) * (tk - ok);
                    startfeedforwardLayer.Sigmas[i] = sigmak;
                    var deltaWk = LearningRate * sigmak * oj;
                    weightMatrix[j, i] += deltaWk;
                }
            }
            if (endfeedforwardLayer.Layer > 0)
            {
                var inputFeedforwardLayer = this.inputFeedforwardLayer;
                if (hiddenFeedforwardLayers.Count > 1)
                {
                    var hiddenLayerCounter = hiddenFeedforwardLayers.Count - 1;
                    inputFeedforwardLayer = hiddenFeedforwardLayers[hiddenLayerCounter];
                }                    
                Backpropagate(inputFeedforwardLayer, endfeedforwardLayer, startfeedforwardLayer, ++layer);
            }
        }

        private void Backpropagate(FeedforwardLayer startfeedforwardLayer,
            FeedforwardLayer endfeedforwardLayer, double outputValue, int layer)
        {
            //FeedforwardLayer endfeedforwardLayer = hiddenFeedforwardLayers[0];
            double[,] weightMatrix = null;
            foreach (var matrix in weightMatrixes)
            {
                if (matrix.Item2 == startfeedforwardLayer.Layer)
                {
                    weightMatrix = matrix.Item3;
                    break;
                }
            }
            for (int i = 0; i < startfeedforwardLayer.Values.Count(); i++)
            {
                for (int j = 0; j < endfeedforwardLayer.Values.Count(); j++)
                {
                    var tk = (double)outputValue;
                    var ok = startfeedforwardLayer.Values[i];
                    var oj = endfeedforwardLayer.Values[j];
                    var sigmak = ok * (1 - ok) * (tk - ok);
                    startfeedforwardLayer.Sigmas[i] = sigmak;
                    var deltaWk = LearningRate * sigmak * oj;
                    weightMatrix[j, i] += deltaWk;
                }
            }
            if (endfeedforwardLayer.Layer > 0)
            {
                var inputFeedforwardLayer = this.inputFeedforwardLayer;
                if (hiddenFeedforwardLayers.Count > 1)
                {
                    var hiddenLayerCounter = hiddenFeedforwardLayers.Count - 1;
                    inputFeedforwardLayer = hiddenFeedforwardLayers[hiddenLayerCounter];
                }
                Backpropagate(inputFeedforwardLayer, endfeedforwardLayer, startfeedforwardLayer, ++layer);
            }
        }

        // 0 - 1 - 2
        private void Backpropagate(FeedforwardLayer inputfeedforwardLayer,
            FeedforwardLayer hiddenfeedforwardLayer, FeedforwardLayer outputfeedforwardLayer, int layer)
        {            
            double[,] weightMatrix = null;
            double[,] outputMatrix = null;
            foreach (var matrix in weightMatrixes)
            {
                if (matrix.Item2 == hiddenfeedforwardLayer.Layer)
                {
                    weightMatrix = matrix.Item3;
                }
                else if (matrix.Item2 == outputfeedforwardLayer.Layer)
                {
                    outputMatrix = matrix.Item3;
                }
            }
            for (int i = 0; i < inputfeedforwardLayer.Values.Count(); i++)
            {
                for (int j = 0; j < hiddenfeedforwardLayer.Values.Count(); j++)
                {                   
                    var oj = hiddenfeedforwardLayer.Values[j];
                    var oi = inputfeedforwardLayer.Values[i];
                    var sumSigma = 0.0;
                    for(int outputNeuronCount = 0; outputNeuronCount < outputfeedforwardLayer.Sigmas.Length; outputNeuronCount++)
                    {
                        sumSigma += outputfeedforwardLayer.Sigmas[outputNeuronCount] * outputMatrix[j, outputNeuronCount];
                    }
                    var sigmaj = oj * (1 - oj) * sumSigma;
                    hiddenfeedforwardLayer.Sigmas[j] = sigmaj;
                    var deltaWk = LearningRate * sigmaj * oi;                                        
                    weightMatrix[i, j] += deltaWk;
                }
            }
            if (hiddenFeedforwardLayers.Count > layer)
            {
                var inputFeedforwardLayer = this.inputFeedforwardLayer;
                var hiddenFeedforwardLayersCounter = hiddenFeedforwardLayers.Count;
                if(hiddenFeedforwardLayersCounter - 1 == layer)
                    inputFeedforwardLayer = this.inputFeedforwardLayer;
                else
                {
                    inputFeedforwardLayer = hiddenFeedforwardLayers[hiddenFeedforwardLayersCounter - layer - 2]; 
                }
                var hiddenfeedforwardLayer1 = hiddenFeedforwardLayers[hiddenFeedforwardLayersCounter - layer - 1];
                var hiddenfeedforwardLayer2 = hiddenFeedforwardLayers[hiddenFeedforwardLayersCounter - layer];
                Backpropagate(inputFeedforwardLayer, hiddenfeedforwardLayer1, hiddenfeedforwardLayer2, ++layer);
            }
        }

        private double SummeDeltaK(FeedforwardLayer startfeedforwardLayer, int counter, int fromHiddenNeuron)
        {
            double[,] weightMatrix = null;
            foreach (var matrix in weightMatrixes)
            {
                if (matrix.Item2 == startfeedforwardLayer.Layer)
                {
                    weightMatrix = matrix.Item3;
                    break;
                }
            }
            double summe = 0.0;
            var wjk = weightMatrix[counter, fromHiddenNeuron];
            var deltak = startfeedforwardLayer.Sigmas[fromHiddenNeuron];
            summe += wjk * deltak;
            return summe;
        }

        public void ShowLayersWithMatrix()
        {            
            double[,] weightMatrix = null;
            foreach (var matrix in weightMatrixes)
            {
                if (matrix.Item1 == inputFeedforwardLayer.Layer)
                {
                    weightMatrix = matrix.Item3;
                    break;
                }
            }
            for (int i = 0; i <= weightMatrix.GetUpperBound(0); i++)
            {
                Debug.WriteLine(string.Format("Input to Hidden{0}",i));
                for (int j = 0; j <= weightMatrix.GetUpperBound(1); j++)
                {
                    Debug.WriteLine(string.Format(" From Input{0} to Hidden{1} : {2} ", i, j, weightMatrix[i, j]));
                }
            }           
            foreach (var hiddenFeedforwardLayer in hiddenFeedforwardLayers)
            {                
                foreach (var matrix in weightMatrixes)
                {
                    if (matrix.Item1 == hiddenFeedforwardLayer.Layer)
                    {
                        weightMatrix = matrix.Item3;
                        break;
                    }
                }
                for (int i = 0; i <= weightMatrix.GetUpperBound(0); i++)
                {
                    Debug.WriteLine(string.Format("Hidden{0} to Output", i));
                    for (int j = 0; j <= weightMatrix.GetUpperBound(1); j++)
                    {
                        Debug.WriteLine(string.Format(" From Hidden{0} to Output{1} : {2} ", i, j, weightMatrix[i, j]));
                    }
                }
                Debug.WriteLine("");
            }            
            Debug.WriteLine("");
        }

        public override void Train(int[] inputValues, int[] outputValues)
        {
            Calculate(inputValues, outputValues);
        }

        public override void Train(double[] inputValues, int[] outputValues)
        {
            Calculate(inputValues, outputValues);
        }

        public override void Train(double[] inputValues, double[] outputValues)
        {            
            Calculate(inputValues, outputValues);
        }

        public override void Train(double[] inputValues, double outputValue)
        {
            Calculate(inputValues, outputValue);
        }

        public override void Train(List<double[]> inputValues, List<double[]> outputValues)
        {
            for (int i = 0; i < inputValues.Count; i++)
            {
                Calculate(inputValues[i], outputValues[i]);
            }
        }

        public override void Reset()
        {
            
        }

        public override double[] ComputeOutputs(double[] input)
        {
            return null;
        }
    }
}
