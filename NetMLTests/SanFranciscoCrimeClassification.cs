using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetML.DecisionTree;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Globalization;
using NetML.RandomForest;
using NetML.SupportVectorMachine;
using NetML.NaiveBayes;
using NetML.kNearestNeighbors;
using NetML.NeuronalNetworks;

namespace NetMLTests
{
    [TestClass]
    public class SanFranciscoCrimeClassification
    {
        [TestMethod]
        public void SanFranciscoCrimeClassificationTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var crimes = dataSetLoader.SelectCrimes();
            RandomForestClassifier decisionTreeClassifier =
            new RandomForestClassifier(crimes, new ShannonEntropySplitter(), new BaggingAlgorithmus(1));
            decisionTreeClassifier.Train();
            var crimeTests = dataSetLoader.SelectCrimes();
            var trueCounter = 0;
            var counter = 0;
            foreach (var item in crimeTests)
            {
                var outputValue = decisionTreeClassifier.Classify(item.Item1);
                if (outputValue == item.Item2)
                    trueCounter++;
                Debug.WriteLine(string.Format("Value {0} - Predicted {1} = {2}",
                    item.Item2, outputValue, (outputValue == item.Item2) ? "true" : "false"));
                counter++;
            }
            Debug.WriteLine(string.Format("Data {0} - True {1} Verhältnis: {2}",
                counter.ToString(), trueCounter.ToString(), (Convert.ToDouble(trueCounter) / Convert.ToDouble(counter)).ToString()));
        }

        [TestMethod]
        public void SanFranciscoCrimeSVMClassificationDataSetTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var crimes = dataSetLoader.SelectCrimes();
            Kernel kernel = new GaussianKernel(0.9);
            SVMClassifier svmClassifier = 
                new SVMClassifier(crimes, kernel);
            svmClassifier.Train();
            var crimeTests = dataSetLoader.SelectCrimes();
            var trueCounter = 0;
            var counter = 0;           
            foreach (var item in crimeTests)
            {
                var outputValue = svmClassifier.Classify(item.Item1);
                if (outputValue == item.Item2)
                    trueCounter++;
                Debug.WriteLine(string.Format("Value {0} - Predicted {1} = {2}",
                    item.Item2, outputValue, (outputValue == item.Item2) ? "true" : "false"));
                counter++;
            }
            Debug.WriteLine(string.Format("Data {0} - True {1} Verhältnis: {2}",
                counter.ToString(), trueCounter.ToString(), (Convert.ToDouble(trueCounter) / Convert.ToDouble(counter)).ToString()));
        }

        [TestMethod]
        public void SanFranciscoCrimeClassificationTestDataSetTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var crimes = dataSetLoader.SelectNeuronalNetworkCrimes();
            //DecisionTreeClassifier decisionTreeClassifier =
            //new DecisionTreeClassifier(crimes, new ShannonEntropySplitter());
            NeuronalNetworkClassifier neuronalNetworkClassifier =
               new NeuronalNetworkClassifier(crimes, 2, 38, 2, 5000, 0.1);
            //Kernel kernel = new LinearKernel();
            //NaiveBayesClassifier naiveBayes =
            //        new NaiveBayesClassifier(crimes);
            neuronalNetworkClassifier.Train();
            var crimeTests = dataSetLoader.SelectCrimes();
            var trueCounter = 0;
            var counter = 0;
            foreach (var item in crimeTests)
            {
                var outputValue = neuronalNetworkClassifier.ClassifiyMultibleResultValue(item.Item1);
                var resultString = String.Empty;
                double maxValue = 0.0;
                int innerCounter = 0;
                int maxItem = 0;
                foreach (var value in outputValue)
                {
                    if (value > maxValue)
                    {
                        maxValue = value;
                        maxItem = innerCounter;
                    }
                    innerCounter++;
                }
                if (maxItem == item.Item2)
                    trueCounter++;
                Debug.WriteLine(string.Format("Value {0} - Predicted {1} = {2}",
                    item.Item2, maxItem, (maxItem == item.Item2) ? "true" : "false"));
                counter++;
            }
            Debug.WriteLine(string.Format("Data {0} - True {1} Verhältnis: {2}",
                counter.ToString(), trueCounter.ToString(), (Convert.ToDouble(trueCounter) / Convert.ToDouble(counter)).ToString()));
        }
    }
}
