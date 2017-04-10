using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using NetML.NeuronalNetworks;
using System.Collections.Generic;

namespace NetMLTests
{
    [TestClass]
    public class NeuronalNetworkTests
    {
        public void TrainClassification(FeedforwardNetwork feedforwardNetwork,
           List<Tuple<double[], double[]>> trainingData, int epochen = 5000)
        {
            for (int i = 0; i < epochen; i++)
            {
                foreach (var trainingDataItem in trainingData)
                {
                    double[] inputValue = trainingDataItem.Item1;
                    double[] outputValue = trainingDataItem.Item2;
                    feedforwardNetwork.Train(inputValue, outputValue);
                    Debug.WriteLine(" InputValue {0} = CalculatedOutput {1} - OutputValue: {2} ",
                        trainingDataItem.Item1[0],
                        feedforwardNetwork.outputFeedforwardLayer.Values[0],
                        trainingDataItem.Item2[0]
                        );                    
                }
            }
        }

        [TestMethod]
        public void XorNeuronalNetworkClassifierTest()
        {
            List<Tuple<double[], double>> xorValues = new List<Tuple<double[], double>>();
            xorValues.Add(new Tuple<double[], double>(new double[] { 0.0, 0.0 }, 1.0));
            xorValues.Add(new Tuple<double[], double>(new double[] { 1.0, 0.0 }, 0.0));
            xorValues.Add(new Tuple<double[], double>(new double[] { 0.0, 1.0 }, 0.0));
            xorValues.Add(new Tuple<double[], double>(new double[] { 1.0, 1.0 }, 1.0));
            NeuronalNetworkClassifier neuronalNetworkClassifier =
                new NeuronalNetworkClassifier(xorValues, 2, 4, 2, 500, 0.7, NeuronalNetworkMode.Cascade);
            neuronalNetworkClassifier.Train();
            var trueCounter = 0;
            var counter = 0;
            foreach (var item in xorValues)
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

        [TestMethod]
        public void AnimalNeuronalNetworkClassifierTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var animals = dataSetLoader.SelectNeuronalNetworkAnimals();          
            for (double i = 0; i < 1; i = i + 1)
            {               
                NeuronalNetworkClassifier neuronalNetworkClassifier =
                    new NeuronalNetworkClassifier(animals, 16, 7, 16, 900, 0.1, NeuronalNetworkMode.Standard);
                neuronalNetworkClassifier.Train();
                var animalsTest = dataSetLoader.SelectAnimals();
                var trueCounter = 0;
                var counter = 0;
                foreach (var item in animalsTest)
                {
                    var outputValue = neuronalNetworkClassifier.ClassifiyMultibleResultValue(item.Item1);
                    var resultString = String.Empty;
                    double maxValue = 0.0;
                    int innerCounter = 1;
                    int maxItem = 0;
                    foreach(var value in outputValue)
                    {
                        if(value > maxValue)
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
                Debug.WriteLine(string.Format(" i = {0} Data {1} - True {2} Verhältnis: {3}", i,
                    counter.ToString(), trueCounter.ToString(), (Convert.ToDouble(trueCounter) / Convert.ToDouble(counter)).ToString()));
            }
        }
    

        [TestMethod]
        public void MushroomsNeuronalNetworkClassifierTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var mushroom = dataSetLoader.SelectNeuronalNetworksMushroom();          
            for (double i = 0; i < 1; i = i + 1)
            {               
                NeuronalNetworkClassifier neuronalNetworkClassifier =
                    new NeuronalNetworkClassifier(mushroom, 21, 2, 21, 50, 0.2);
                neuronalNetworkClassifier.Train();
                var mushroomTest = dataSetLoader.SelectMushroom();
                var trueCounter = 0;
                var counter = 0;
                foreach (var item in mushroomTest)
                {
                    var outputValue = neuronalNetworkClassifier.ClassifiyMultibleResultValue(item.Item1);
                    var resultString = String.Empty;
                    double maxValue = 0.0;
                    int innerCounter = 0;
                    int maxItem = 0;
                    foreach(var value in outputValue)
                    {
                        if(value > maxValue)
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
                Debug.WriteLine(string.Format(" i = {0} Data {1} - True {2} Verhältnis: {3}", i,
                    counter.ToString(), trueCounter.ToString(), (Convert.ToDouble(trueCounter) / Convert.ToDouble(counter)).ToString()));
            }
        }

        [TestMethod]
        public void MushroomsNeuronalNetworkMachineTrainAndClassify8020Test()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var mushroom = dataSetLoader.SelectNeuronalNetworksTrainingMushroom(80);
            for (double i = 0; i < 1; i = i + 1)
            {
                NeuronalNetworkClassifier neuronalNetworkClassifier =
                    new NeuronalNetworkClassifier(mushroom, 21, 2, 21, 50, 0.2);
                neuronalNetworkClassifier.Train();
                var mushroomTest = dataSetLoader.SelectNeuronalNetworksSelectingMushroom(20);
                var trueCounter = 0;
                var counter = 0;
                foreach (var item in mushroomTest)
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
                Debug.WriteLine(string.Format(" i = {0} Data {1} - True {2} Verhältnis: {3}", i,
                    counter.ToString(), trueCounter.ToString(), (Convert.ToDouble(trueCounter) / Convert.ToDouble(counter)).ToString()));
            }
        }
    }
}
