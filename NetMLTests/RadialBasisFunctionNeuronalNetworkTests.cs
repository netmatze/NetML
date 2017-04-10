using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetML.RadialBasisFunctionNetwork;
using NetML.NeuronalNetworks;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace NetMLTests
{
    [TestClass]
    public class RadialBasisFunctionNeuronalNetworkTests
    {
        [TestMethod]
        public void XorRadialBasisFunctionNeuronalNetworkTest()
        {
            List<Tuple<double[], double>> inputData = new List<Tuple<double[], double>>();
            List<double[]> classifyData = new List<double[]>();
            foreach (var item in Enumerable.Range(0, 2))
            {
                foreach (var innerItem in Enumerable.Range(0, 2))
                {
                    var doubleValue = (item == innerItem)? 1.0 : 0.0;
                    inputData.Add(new Tuple<double[], double>(new double[] { item, innerItem }, doubleValue));
                    classifyData.Add(new double[] { item, innerItem });
                }
            }
            OneHiddenLayerNeuronCounter oneHiddenLayerNeuronCounter = new OneHiddenLayerNeuronCounter(2, 1, 4);
            RadialBasisFunctionNeuronalNetwork radialBasisFunctionNeuronalClassifier =
                new RadialBasisFunctionNeuronalNetwork(oneHiddenLayerNeuronCounter, 200000, 0.9);
            radialBasisFunctionNeuronalClassifier.Train(inputData);
            foreach (var classification in classifyData)
            {
                var result = radialBasisFunctionNeuronalClassifier.Classify(classification);
                Debug.WriteLine(string.Format("Input0: {0} Input1: {1} Result: {2}", classification[0], classification[1], result.OutputValues[0]));
            }
        }

        [TestMethod]
        public void AndRadialBasisFunctionNeuronalNetworkTest()
        {
            List<Tuple<double[], double>> inputData = new List<Tuple<double[], double>>();
            List<double[]> classifyData = new List<double[]>();
            foreach (var item in Enumerable.Range(0, 2))
            {
                foreach (var innerItem in Enumerable.Range(0, 2))
                {
                    var doubleValue = (item == 0 || innerItem == 0) ? 0.0 : 1.0;
                    inputData.Add(new Tuple<double[], double>(new double[] { item, innerItem }, doubleValue));
                    classifyData.Add(new double[] { item, innerItem });
                }
            }
            OneHiddenLayerNeuronCounter oneHiddenLayerNeuronCounter = new OneHiddenLayerNeuronCounter(2, 1, 4);
            RadialBasisFunctionNeuronalNetwork radialBasisFunctionNeuronalClassifier =
                new RadialBasisFunctionNeuronalNetwork(oneHiddenLayerNeuronCounter, 2000, 0.5);
            radialBasisFunctionNeuronalClassifier.Train(inputData);
            foreach (var classification in classifyData)
            {
                var result = radialBasisFunctionNeuronalClassifier.Classify(classification);
                Debug.WriteLine(string.Format("Input0: {0} Input1: {1} Result: {2}", classification[0], classification[1], result.OutputValues[0]));
            }
        }

        [TestMethod]
        public void AnimalRadialBasisFunctionNeuronalNetworkClassifierTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var animals = dataSetLoader.SelectNeuronalNetworkAnimals();
            for (double i = 0; i < 1; i = i + 1)
            {
                OneHiddenLayerNeuronCounter oneHiddenLayerNeuronCounter = new OneHiddenLayerNeuronCounter(16, 8, 32);
                RadialBasisFunctionNeuronalNetwork radialBasisFunctionNeuronalNetworkClassifier =
                    new RadialBasisFunctionNeuronalNetwork(oneHiddenLayerNeuronCounter, 10000, 0.5);
                radialBasisFunctionNeuronalNetworkClassifier.Train(animals);
                var animalsTest = dataSetLoader.SelectAnimals();
                var trueCounter = 0;
                var counter = 0;
                foreach (var item in animalsTest)
                {
                    var outputValue = radialBasisFunctionNeuronalNetworkClassifier.Classify(item.Item1);
                    var resultString = String.Empty;
                    double maxValue = 0.0;
                    int innerCounter = 1;
                    int maxItem = 0;
                    foreach (var value in outputValue.OutputValues)
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
                    Debug.WriteLine(string.Format("Value {0} - Predicted {1} {2} {3} {4} {5} {6} {7} = {8}",
                        item.Item2, 
                        Convert.ToDecimal(outputValue.OutputValues[0]), 
                        Convert.ToDecimal(outputValue.OutputValues[1]), 
                        Convert.ToDecimal(outputValue.OutputValues[2]),
                        Convert.ToDecimal(outputValue.OutputValues[3]),
                        Convert.ToDecimal(outputValue.OutputValues[4]),
                        Convert.ToDecimal(outputValue.OutputValues[5]),
                        Convert.ToDecimal(outputValue.OutputValues[6]),
                        (maxItem == item.Item2) ? "true" : "false"));            
                    counter++;
                }
                Debug.WriteLine(string.Format(" i = {0} Data {1} - True {2} Verhältnis: {3}", i,
                    counter.ToString(), trueCounter.ToString(), (Convert.ToDouble(trueCounter) / Convert.ToDouble(counter)).ToString()));
            }
        }

        [TestMethod]
        public void IrisRadialBasisFunctionNeuronalNetworkClassifierTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var irises = dataSetLoader.SelectNeuronalNetworkIrises();
            for (double i = 0; i < 1; i = i + 1)
            {
                OneHiddenLayerNeuronCounter oneHiddenLayerNeuronCounter = new OneHiddenLayerNeuronCounter(4, 3, 3);
                RadialBasisFunctionNeuronalNetwork radialBasisFunctionNeuronalNetworkClassifier =
                    new RadialBasisFunctionNeuronalNetwork(oneHiddenLayerNeuronCounter, 10000, 0.7);
                radialBasisFunctionNeuronalNetworkClassifier.Train(irises);
                var irisesTest = dataSetLoader.SelectIrises();
                var trueCounter = 0;
                var counter = 0;
                foreach (var item in irisesTest)
                {
                    var outputValue = radialBasisFunctionNeuronalNetworkClassifier.Classify(item.Item1);
                    var resultString = String.Empty;
                    double maxValue = 0.0;
                    int innerCounter = 1;
                    int maxItem = 0;
                    foreach (var value in outputValue.OutputValues)
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
                    Debug.WriteLine(string.Format("Value {0} - Predicted {1} {2} {3} = {4}",
                        item.Item2, 
                        Convert.ToDecimal(outputValue.OutputValues[0]),
                        Convert.ToDecimal(outputValue.OutputValues[1]), 
                        Convert.ToDecimal(outputValue.OutputValues[2]), 
                        (maxItem == item.Item2) ? "true" : "false"));
                    counter++;
                }
                Debug.WriteLine(string.Format(" i = {0} Data {1} - True {2} Verhältnis: {3}", i,
                    counter.ToString(), trueCounter.ToString(), (Convert.ToDouble(trueCounter) / Convert.ToDouble(counter)).ToString()));
            }
        }

        [TestMethod]
        public void CreditDataRadialBasisFunctionNeuronalNetworkClassifierTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var creditData = dataSetLoader.SelectCreditData();
            for (double i = 0; i < 1; i = i + 1)
            {
                OneHiddenLayerNeuronCounter oneHiddenLayerNeuronCounter = new OneHiddenLayerNeuronCounter(6, 1, 36);
                RadialBasisFunctionNeuronalNetwork radialBasisFunctionNeuronalNetworkClassifier =
                    new RadialBasisFunctionNeuronalNetwork(oneHiddenLayerNeuronCounter, 5000, 0.7);
                radialBasisFunctionNeuronalNetworkClassifier.Train(creditData);
                var creditDataTest = dataSetLoader.SelectCreditData();
                var trueCounter = 0;
                var counter = 0;
                foreach (var item in creditDataTest)
                {
                    var outputValue = radialBasisFunctionNeuronalNetworkClassifier.Classify(item.Item1);
                    var value = (outputValue.OutputValues[0] > 0.5) ? 1.0 : 0.0;
                    if (value == item.Item2)
                        trueCounter++;
                    Debug.WriteLine(string.Format("Value {0} - Predicted {1} = {2}",
                        item.Item2, Convert.ToDecimal(outputValue.OutputValues[0]), 
                        (value == item.Item2) ? "true" : "false"));
                    counter++;
                }
                Debug.WriteLine(string.Format(" i = {0} Data {1} - True {2} Verhältnis: {3}", i,
                    counter.ToString(), trueCounter.ToString(), (Convert.ToDouble(trueCounter) / Convert.ToDouble(counter)).ToString()));
            }
        }
    }
}
