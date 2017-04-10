using NetML.kMeans;
using NetML.kNearestNeighbors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.RadialBasisFunctionNetwork
{
    public class RadialBasisFunctionNetwork
    {
        protected RadialBasisFunctionLayer inputRadialBasisFunctionLayer;

        protected RadialBasisFunctionLayer hiddenRadialBasisFunctionLayer;

        protected RadialBasisFunctionLayer outputRadialBasisFunctionLayer;

        public List<Tuple<int, int, double[,]>> weightMatrixes = new List<Tuple<int, int, double[,]>>();

        public void AddInputLayer(RadialBasisFunctionLayer radialBasisFunctionLayer)
        {
            inputRadialBasisFunctionLayer = radialBasisFunctionLayer;
        }

        public void AddHiddenLayer(RadialBasisFunctionLayer radialBasisFunctionLayer)
        {
            hiddenRadialBasisFunctionLayer = radialBasisFunctionLayer;
            BuildWeightMatrix(radialBasisFunctionLayer);
        }

        public void AddOutputLayer(RadialBasisFunctionLayer radialBasisFunctionLayer)
        {
            outputRadialBasisFunctionLayer = radialBasisFunctionLayer;
            BuildWeightMatrixOutputLayer(radialBasisFunctionLayer);
        }

        public void BuildWeightMatrix(RadialBasisFunctionLayer radialBasisFunctionLayer)
        {
            weightMatrixes.Add(new Tuple<int, int, double[,]>(
                0, 1, new double[radialBasisFunctionLayer.Neurons, radialBasisFunctionLayer.Neurons]));
        }

        public void BuildWeightMatrixOutputLayer(RadialBasisFunctionLayer radialBasisFunctionLayer)
        {
            weightMatrixes.Add(new Tuple<int, int, double[,]>(
                1, 2, new double[radialBasisFunctionLayer.Neurons, radialBasisFunctionLayer.Neurons]));
        }

        public void Train(double[] inputValues, double[] outputValues)
        {
            Calculate(inputValues, outputValues);
        }

        public void Train(double[] inputValues, double outputValue)
        {
            Calculate(inputValues, outputValue);
        }

        public void CalculateCentroidsAndSigmoids(List<Tuple<double[], double>> data)
        {
            var hiddenLayer = hiddenRadialBasisFunctionLayer;
            List<double[]> centroidData = new List<double[]>();
            foreach(var item in data)
            {
                centroidData.Add(item.Item1);
            }
            kMeansClustering kmeansClustering = new kMeansClustering(centroidData);
            var clusters = kmeansClustering.ClusterWithCentroid(hiddenLayer.Neurons);
            for (int i = 0; i < hiddenLayer.Neurons; i++)
            {
                foreach (var cluster in clusters[i])
                {
                    hiddenLayer.Cetroids[i] = cluster.Item3;                   
                    var values = cluster.Item2;
                    var pNearestNeighbors = FindMinimal(centroidData, cluster.Item3, hiddenLayer.P);
                    var sigmasumme = 0.0;
                    foreach(var item in pNearestNeighbors)
                    {
                        sigmasumme += CalculateEuclidDistance(item.Item2, cluster.Item3);
                    }
                    var sigma = 0.0;
                    sigma = Math.Sqrt(1.0 / hiddenLayer.P * sigmasumme);
                    hiddenLayer.Sigmoids[i] = sigma;
                }
            }
        }

        private List<Tuple<double, double[]>> FindMinimal(List<double[]> centroidData, double[] centroid, int p)
        {
            var result = new List<Tuple<double, double[]>>();
            var resultTupleList = new List<Tuple<double, double[]>>();
            foreach(var item in centroidData)
            {
                result.Add(CalculateEuclidDistanceWithValues(item, centroid));
            }            
            var minList = result.OrderBy(o => o.Item1).ToArray();
            for(int minItem = 0; minItem <= p; minItem++)
            {
                resultTupleList.Add(minList[minItem]);
            }
            return resultTupleList;
        }

        private double CalculateEuclidDistance(double[] centroidData, double[] centroid)
        {
            var distance = 0.0;
            var sum = 0.0;
            for (int i = 0; i < centroidData.Length; i++)
            {
                sum += (centroidData[i] - centroid[i]) * (centroidData[i] - centroid[i]);
            }
            distance = Math.Sqrt(sum);
            return distance;
        }

        private Tuple<double, double[]> CalculateEuclidDistanceWithValues(double[] centroidData, double[] centroid)
        {
            var distance = 0.0;
            var sum = 0.0;
            for (int i = 0; i < centroidData.Length; i++)
            {
                sum += (centroidData[i] - centroid[i]) * (centroidData[i] - centroid[i]);
            }
            distance = Math.Sqrt(sum);
            return new Tuple<double, double[]>(distance, centroidData);
        }

        public void CalculateCentroidsAndSigmoids(List<Tuple<double[], double[]>> data)
        {
            var hiddenLayer = hiddenRadialBasisFunctionLayer;
            List<double[]> centroidData = new List<double[]>();
            foreach (var item in data)
            {
                centroidData.Add(item.Item1);
            }
            kMeansClustering kmeansClustering = new kMeansClustering(centroidData);
            var clusters = kmeansClustering.ClusterWithCentroid(hiddenLayer.Neurons);
            for (int i = 0; i < hiddenLayer.Neurons; i++)
            {
                foreach (var cluster in clusters[i])
                {
                    if (cluster != null)
                    {
                        hiddenLayer.Cetroids[i] = cluster.Item3;
                        var values = cluster.Item2;
                        var pNearestNeighbors = FindMinimal(centroidData, cluster.Item3, hiddenLayer.P);
                        var sigmasumme = 0.0;
                        foreach (var item in pNearestNeighbors)
                        {
                            sigmasumme += CalculateEuclidDistance(item.Item2, cluster.Item3);
                        }
                        var sigma = 0.0;
                        sigma = Math.Sqrt(1.0 / hiddenLayer.P * sigmasumme);
                        hiddenLayer.Sigmoids[i] = sigma;
                    }
                    else
                    {
                        hiddenLayer.Sigmoids[i] = 1.0;
                    }
                }
            }
        }

        private void Calculate(double[] inputValues, double[] outputValues)
        {
            int counter = 0;
            foreach (var inputValue in inputValues)
            {
                inputRadialBasisFunctionLayer.Values[counter] = inputValue;                
                counter++;
            }
            CalculateHiddenLayer(inputRadialBasisFunctionLayer, hiddenRadialBasisFunctionLayer);
            CalculateOutputLayer(hiddenRadialBasisFunctionLayer, outputRadialBasisFunctionLayer);
            Backpropagate(outputRadialBasisFunctionLayer, hiddenRadialBasisFunctionLayer, outputValues, 0);            
        }

        private void Calculate(double[] inputValues, double outputValue)
        {
            var counter = 0;
            foreach (var inputValue in inputValues)
            {
                inputRadialBasisFunctionLayer.Values[counter] = inputValue;
                counter++;
            }
            CalculateHiddenLayer(inputRadialBasisFunctionLayer, hiddenRadialBasisFunctionLayer);
            CalculateOutputLayer(hiddenRadialBasisFunctionLayer, outputRadialBasisFunctionLayer);
            Backpropagate(outputRadialBasisFunctionLayer, hiddenRadialBasisFunctionLayer, outputValue, 0);
        }

        private void CalculateHiddenLayer(RadialBasisFunctionLayer inputRadialBasisFunctionLayer, 
            RadialBasisFunctionLayer hiddenRadialBasisFunctionLayer)
        {            
            for (int i = 0; i < hiddenRadialBasisFunctionLayer.Values.Count(); i++)
            {
                var sigma = 0.0;
                var sigmaSumme = 0.0;
                var expression = 0.0;
                for (int j = 0; j < inputRadialBasisFunctionLayer.Values.Count(); j++)
                {
                    var xj = inputRadialBasisFunctionLayer.Values[j];
                    var cj = 0.0;
                    if(hiddenRadialBasisFunctionLayer.Cetroids[i] != null)
                        cj = hiddenRadialBasisFunctionLayer.Cetroids[i][j];
                    sigmaSumme += (xj - cj) * (xj - cj);                                    
                }
                var smallSigma = hiddenRadialBasisFunctionLayer.Sigmoids[i];
                expression = -1 * sigmaSumme / 2 * smallSigma * smallSigma;
                sigma = Math.Pow(Math.E, expression);
                hiddenRadialBasisFunctionLayer.Values[i] = sigma;
            }
        }

        private void CalculateOutputLayer(RadialBasisFunctionLayer hiddenRadialBasisFunctionLayer, 
            RadialBasisFunctionLayer outputRadialBasisFunctionLayer)
        {
            double[,] weightMatrix = null;
            foreach (var matrix in weightMatrixes)
            {
                if (matrix.Item1 == hiddenRadialBasisFunctionLayer.Layer - 1)
                {
                    weightMatrix = matrix.Item3;
                }
            }
            for (int i = 0; i < outputRadialBasisFunctionLayer.Values.Count(); i++)
            {
                double summe = 0.0;
                for (int j = 0; j < hiddenRadialBasisFunctionLayer.Values.Count(); j++)
                {
                    summe += hiddenRadialBasisFunctionLayer.Values[j] * weightMatrix[j, i];                                       
                }
                outputRadialBasisFunctionLayer.Values[i] = outputRadialBasisFunctionLayer.ActivationFunction_.ActivationFunction(summe);
            }
        }

        public double[] Classify(double[] inputValues)
        {
            var counter = 0;
            foreach (var inputValue in inputValues)
            {
                inputRadialBasisFunctionLayer.Values[counter] = inputValue;
                counter++;
            }
            ClassifyHiddenLayer(inputRadialBasisFunctionLayer, hiddenRadialBasisFunctionLayer);
            ClassifyOutputLayer(hiddenRadialBasisFunctionLayer, outputRadialBasisFunctionLayer);
            return outputRadialBasisFunctionLayer.Values;
        }

        private void ClassifyHiddenLayer(RadialBasisFunctionLayer inputRadialBasisFunctionLayer, RadialBasisFunctionLayer hiddenRadialBasisFunctionLayer)
        {
            for (int i = 0; i < hiddenRadialBasisFunctionLayer.Values.Count(); i++)
            {
                var sigma = 0.0;
                var sigmaSumme = 0.0;
                var expression = 0.0;
                for (int j = 0; j < inputRadialBasisFunctionLayer.Values.Count(); j++)
                {
                    var xj = inputRadialBasisFunctionLayer.Values[j];
                    var cj = 0.0;
                    if (hiddenRadialBasisFunctionLayer.Cetroids[i] != null)
                        cj = hiddenRadialBasisFunctionLayer.Cetroids[i][j];
                    sigmaSumme += (xj - cj) * (xj - cj);
                }
                var smallSigma = hiddenRadialBasisFunctionLayer.Sigmoids[i];
                expression = -1 * sigmaSumme / 2 * smallSigma * smallSigma;
                sigma = Math.Pow(Math.E, expression);
                hiddenRadialBasisFunctionLayer.Values[i] = sigma;
            }
        }

        private void ClassifyOutputLayer(RadialBasisFunctionLayer hiddenRadialBasisFunctionLayer, RadialBasisFunctionLayer outputRadialBasisFunctionLayer)
        {
            double[,] weightMatrix = null;
            foreach (var matrix in weightMatrixes)
            {
                if (matrix.Item1 == hiddenRadialBasisFunctionLayer.Layer - 1)
                {
                    weightMatrix = matrix.Item3;
                }
            }
            for (int i = 0; i < outputRadialBasisFunctionLayer.Values.Count(); i++)
            {
                double summe = 0.0;
                for (int j = 0; j < hiddenRadialBasisFunctionLayer.Values.Count(); j++)
                {
                    summe += hiddenRadialBasisFunctionLayer.Values[j] * weightMatrix[j, i]; //j, i];                    
                }
                outputRadialBasisFunctionLayer.Values[i] = outputRadialBasisFunctionLayer.ActivationFunction_.ActivationFunction(summe);
            }
        }

        private void Backpropagate(RadialBasisFunctionLayer outputRadialBasisFunctionLayer,
           RadialBasisFunctionLayer hiddenRadialBasisFunctionLayer, double[] outputValues, int layer)
        {
            double[,] weightMatrix = null;
            foreach (var matrix in weightMatrixes)
            {
                if (matrix.Item2 == outputRadialBasisFunctionLayer.Layer)
                {
                    weightMatrix = matrix.Item3;
                    break;
                }
            }
            for (int i = 0; i < outputRadialBasisFunctionLayer.Values.Count(); i++)
            {
                for (int j = 0; j < hiddenRadialBasisFunctionLayer.Values.Count(); j++)
                {
                    var tk = (double)outputValues[i];
                    var ok = outputRadialBasisFunctionLayer.Values[i];
                    var oj = hiddenRadialBasisFunctionLayer.Values[j];
                    var sigmak = ok * (1 - ok) * (tk - ok);
                    var deltaWk = LearningRate * sigmak * oj;
                    weightMatrix[j, i] += deltaWk;
                }
            }           
        }

        private void Backpropagate(RadialBasisFunctionLayer outputRadialBasisFunctionLayer,
            RadialBasisFunctionLayer hiddenRadialBasisFunctionLayer, double outputValue, int layer)
        {
            double[,] weightMatrix = null;
            foreach (var matrix in weightMatrixes)
            {
                if (matrix.Item2 == outputRadialBasisFunctionLayer.Layer)
                {
                    weightMatrix = matrix.Item3;
                    break;
                }
            }
            for (int i = 0; i < outputRadialBasisFunctionLayer.Values.Count(); i++)
            {
                for (int j = 0; j < hiddenRadialBasisFunctionLayer.Values.Count(); j++)
                {
                    var tk = (double)outputValue;
                    var ok = outputRadialBasisFunctionLayer.Values[i];
                    var oj = hiddenRadialBasisFunctionLayer.Values[j];
                    var sigmak = ok * (1 - ok) * (tk - ok);
                    var deltaWk = LearningRate * sigmak * oj;
                    weightMatrix[j, i] += deltaWk;
                }
            }           
        }

        public void RandomFillWeightMatrix()
        {
            Random rand = new Random(100);
            foreach (var matrix in weightMatrixes)
            {
                var array = matrix.Item3;
                for (int i = 0; i <= array.GetUpperBound(0); i++)
                {
                    for (int j = 0; j <= array.GetUpperBound(1); j++)
                    {
                        array[i, j] = rand.NextDouble();
                    }
                }
            }
        }

        private double learningRate = 0.1;

        public double LearningRate
        {
            get { return learningRate; }
            set { learningRate = value; }
        }

        private int evolutions = 1000;

        public int Evolutions
        {
            get { return evolutions; }
            set { evolutions = value; }
        }
    }
}
