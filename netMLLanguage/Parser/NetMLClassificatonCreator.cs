using NetML;
using NetML.DecisionTree;
using NetML.kNearestNeighbors;
using NetML.LogisticRegression;
using NetML.NaiveBayes;
using NetML.NeuronalNetworks;
using NetML.RadialBasisFunctionNetwork;
using NetML.RandomForest;
using NetML.SupportVectorMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace netMLLanguage.Parser
{
    public class NetMLClassificatonCreator
    {
        private Classification classification;
        private NetMLObject netMLObject;

        public NetMLClassificatonCreator(NetMLObject netMLObject)
        {
            this.netMLObject = netMLObject;
        }

        public void Create(List<Tuple<double[], double>> data)
        {
            switch(netMLObject.Algorithmus)
            {
                case "randomforest":
                    RandomForest(data);
                    break;
                case "backpropagation":
                    Backpropagation(data);
                    break;
                case "radialbasisfunction":
                    RadialBasisFunction(data);
                    break;
                case "selforganisingmap":

                    break;
                case "logisticregression":
                    LogisticRegression(data);
                    break;
                case "naivebayers":
                    Naivebayers(data);                 
                    break;
                case "dualperceptron":
                    DualPerceptron(data);
                    break;
                case "supportvectormachine":
                    SupportVectorMachine(data);
                    break;
                case "knn":
                    KNN(data);
                    break;
                case "decisiontree":
                    DecissionTree(data);
                    break;
            }
        }

        private void DualPerceptron(List<Tuple<double[], double>> data)
        {
            Kernel kernel = new LinearKernel();
            foreach (var item in netMLObject.Options)
            {
                if (item == "linearkernel")
                {
                    kernel = new LinearKernel();
                }
                else if (item == "gaussiankernel")
                {
                    kernel = new GaussianKernel(1.0);
                }
                else if (item == "polynomialkernel")
                {
                    kernel = new PolynomialKernel(1);
                }
                else if (item == "logitkernel")
                {
                    kernel = new LogitKernel();
                }
                else if (item == "tanhkernel")
                {
                    kernel = new TanhKernel();
                }
            }
            classification = new DualPerceptronClassifier(data, kernel);
        }

        private void Backpropagation(List<Tuple<double[], double>> data)
        {
            int inputneurons = 0;
            int outputneurons = 0;
            int firsthiddenlayerneurons = 0;
            int secondhiddenlayerneurons = 0;
            int evolutions = 1000;
            double learningrate = 0.5;
            bool firstHiddenLayer = false;            
            bool secondHiddenLayer = false;            
            foreach (var value in netMLObject.DoubleValues)
            {
                if (value.Key == "inputneurons")
                {
                    inputneurons = (int) value.Value;
                }
                else if (value.Key == "outputneurons")
                {
                    outputneurons = (int) value.Value;
                }
                else if (value.Key == "firsthiddenlayerneurons")
                {
                    firsthiddenlayerneurons = (int) value.Value;
                    firstHiddenLayer = true;
                }
                else if (value.Key == "secondhiddenlayerneurons")
                {
                    secondhiddenlayerneurons = (int)value.Value;
                    secondHiddenLayer = true;
                }
                else if (value.Key == "evolutions")
                {
                    evolutions = (int) value.Value;
                }
                else if (value.Key == "learningrate")
                {
                    learningrate = value.Value;
                }
            }           
            if(firstHiddenLayer && secondHiddenLayer)
            {
                classification = new NeuronalNetworkClassifier(
                    data, inputneurons, outputneurons, firsthiddenlayerneurons, secondhiddenlayerneurons, evolutions, learningrate);
            }
            else if (firstHiddenLayer)
            {
                classification = new NeuronalNetworkClassifier(
                    data, inputneurons, outputneurons, firsthiddenlayerneurons, evolutions, learningrate);
            }
            else
            {
                classification = new NeuronalNetworkClassifier(
                    data, inputneurons, outputneurons, evolutions, learningrate);
            }
        }

        private void RadialBasisFunction(List<Tuple<double[], double>> data)
        {
            int inputneurons = 0;
            int outputneurons = 0;
            int firsthiddenlayerneurons = 0;
            int evolutions = 1000;
            double learningrate = 0.5;
            foreach (var value in netMLObject.DoubleValues)
            {
                if (value.Key == "inputneurons")
                {
                    inputneurons = (int)value.Value;
                }
                else if (value.Key == "outputneurons")
                {
                    outputneurons = (int)value.Value;
                }
                else if (value.Key == "firsthiddenlayerneurons")
                {
                    firsthiddenlayerneurons = (int)value.Value;
                }                
                else if (value.Key == "evolutions")
                {
                    evolutions = (int)value.Value;
                }
                else if (value.Key == "learningrate")
                {
                    learningrate = value.Value;
                }
            }                       
            classification = new RadialBasisFunctionNetworkClassifier(
                data, inputneurons, outputneurons, firsthiddenlayerneurons, evolutions, learningrate);
        }

        private void SupportVectorMachine(List<Tuple<double[], double>> data)
        {
            Kernel kernel = new LinearKernel();
            double n = 0.0;
            double C = 0.0;
            bool nAndCSet = false;
            foreach (var item in netMLObject.Options)
            {
                if (item == "linearkernel")
                {
                    kernel = new LinearKernel();
                }
                else if (item == "gaussiankernel")
                {
                    kernel = new GaussianKernel(1.0);
                }
                else if (item == "polynomialkernel")
                {
                    kernel = new PolynomialKernel(1);
                }
                else if (item == "logitkernel")
                {
                    kernel = new LogitKernel();
                }
                else if (item == "tanhkernel")
                {
                    kernel = new TanhKernel();
                }               
            }
            foreach(var value in netMLObject.DoubleValues)
            {
                if(value.Key == "n")
                {
                    n = value.Value;
                    nAndCSet = true;
                }
                else if(value.Key == "c")
                {
                    C = value.Value;
                    nAndCSet = true;
                }
            }
            if (nAndCSet)
            {
                classification = new SVMClassifier(data, kernel, n, C);
            }
            else
            {
                classification = new SVMClassifier(data, kernel);
            }
        }

        private void LogisticRegression(List<Tuple<double[], double>> data)
        {
            IRegressionFunction regressionFunction = new LogisticCostFunction();
            foreach (var item in netMLObject.Options)
            {
                if (item == "logitcostfunction")
                {
                    regressionFunction = new LogisticCostFunction();
                }
                else if (item == "tanhcostfunction")
                {
                    regressionFunction = new TanhCostFunction();
                }
            }
            classification = new Regression(data, regressionFunction);
        }

        private void KNN(List<Tuple<double[], double>> data)
        {
            Metric metric = new EuclideMetric();
            foreach (var item in netMLObject.Options)
            {
                if (item == "euclidmetric")
                {
                    metric = new EuclideMetric();
                }
                else if (item == "manhattanmetric")
                {
                    metric = new ManhattanMetric();
                }
                else if (item == "squaredeuclidmetric")
                {
                    metric = new SquaredEuclideMetric();
                }
                else if (item == "maximummetric")
                {
                    metric = new MaximumMetric();
                }
            }
            classification = new KNearestNeighborsClassifier(data, 2, metric);
        }

        private void Naivebayers(List<Tuple<double[], double>> data)
        {
            BayesKernel kernel = new LinearBayesKernel(data);
            foreach (var item in netMLObject.Options)
            {
                if (item == "linearbayeskernel")
                {
                    kernel = new LinearBayesKernel(data);
                }
                else if (item == "gaussianbayeskernel")
                {
                    kernel = new GaussianBayesKernel(data);
                }
            }
            classification = new NaiveBayesClassifier(data, kernel);
        }

        private void DecissionTree(List<Tuple<double[], double>> data)
        {
            Splitter splitter = new ShannonEntropySplitter();
            foreach (var item in netMLObject.Options)
            {
                if (item == "shannonentropysplitter")
                {
                    splitter = new ShannonEntropySplitter();
                }
                else if (item == "giniindexsplitter")
                {
                    splitter = new GiniImpuritySplitter();
                }
            }
            classification = new DecisionTreeClassifier(data, splitter);
        }

        private void RandomForest(List<Tuple<double[], double>> data)
        {
            Splitter splitter = new ShannonEntropySplitter();
            RandomForestAlgorithm randomForestAlgorithm = new BoostingAlgorithmus();
            foreach (var item in netMLObject.Options)
            {
                if (item == "shannonentropysplitter")
                {
                    splitter = new ShannonEntropySplitter();
                }
                else if (item == "giniindexsplitter")
                {
                    splitter = new GiniImpuritySplitter();
                }
                else if (item == "bagging")
                {
                    randomForestAlgorithm = new BaggingAlgorithmus(10);
                }
                else if (item == "boosting")
                {
                    randomForestAlgorithm = new BoostingAlgorithmus();
                }
            }
            classification = new RandomForestClassifier(data, splitter, randomForestAlgorithm);
        }

        public void Train()
        {
            classification.Train();
        }

        public double Classify(double[] inputValues)
        {
            return classification.Classify(inputValues);
        }
    }
}
