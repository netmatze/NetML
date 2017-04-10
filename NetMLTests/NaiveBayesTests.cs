using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetML.DecisionTree;
using System.Collections.Generic;
using System.Diagnostics;
using NetML.SupportVectorMachine;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using NetML.NaiveBayes;
using System.Globalization;

namespace NetMLTests
{
    [TestClass]
    public class NaiveBayesTests
    {
        [TestMethod]
        public void NaiveBayesAnimalTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var animals = dataSetLoader.SelectAnimals();
            for (double i = 0; i < 1; i = i + 1)
            {
                NaiveBayesClassifier naiveBayes =
                    new NaiveBayesClassifier(animals);
                var animalsTest = dataSetLoader.SelectAnimals();
                var trueCounter = 0;
                var counter = 0;
                foreach (var item in animalsTest)
                {
                    var outputValue = naiveBayes.Classify(item.Item1);
                    if (outputValue == item.Item2)
                        trueCounter++;
                    Debug.WriteLine(string.Format("Value {0} - Predicted {1} = {2}",
                        item.Item2, outputValue, (outputValue == item.Item2) ? "true" : "false"));
                    counter++;
                }
                Debug.WriteLine(string.Format(" i = {0} Data {1} - True {2} Verhältnis: {3}", i,
                    counter.ToString(), trueCounter.ToString(), (Convert.ToDouble(trueCounter) / Convert.ToDouble(counter)).ToString()));
            }
        }

        [TestMethod]
        public void CreditDataNaiveBayesTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var creditData = dataSetLoader.SelectCreditData();
            NaiveBayesClassifier naiveBayes =
                new NaiveBayesClassifier(creditData);
            var creditDataTest = dataSetLoader.SelectCreditData();
            var trueCounter = 0;
            var counter = 0;
            foreach (var item in creditDataTest)
            {
                var outputValue = naiveBayes.Classify(item.Item1);
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
        public void NaiveBayesGaussianTest()
        {
            Tuple<double[], double> tuple1 = new Tuple<double[], double>(new double[] { 5.0, 1.0, 1.0 }, 0);
            Tuple<double[], double> tuple2 = new Tuple<double[], double>(new double[] { 1.0, 5.0, 1.0 }, 1);
            Tuple<double[], double> tuple3 = new Tuple<double[], double>(new double[] { 1.0, 1.0, 5.0 }, 2);
            List<Tuple<double[], double>> movies =
                new List<Tuple<double[], double>>() { tuple1, tuple2, tuple3 };
            NaiveBayesClassifier naiveBayes =
               new NaiveBayesClassifier(movies, new GaussianBayesKernel(movies));
            var result = naiveBayes.Classify(new double[] { 5.0, 1.0, 1.0 });
            result = naiveBayes.Classify(new double[] { 1.0, 5.0, 1.0 });
            result = naiveBayes.Classify(new double[] { 1.0, 1.0, 5.0 });
            result = naiveBayes.Classify(new double[] { 0.5, 9.0, 0.1 });
            result = naiveBayes.Classify(new double[] { 0.4, 0.2, 2.1 });
            result = naiveBayes.Classify(new double[] { 0.3, 0.3, 2.5 });
        }

        [TestMethod]
        public void NaiveBayesIrisTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var irisis = dataSetLoader.SelectIrises();
            for (double i = 0; i < 1; i = i + 1)
            {                
                NaiveBayesClassifier naiveBayes =
                    new NaiveBayesClassifier(irisis, new LinearBayesKernel(irisis));
                var irisesTests = dataSetLoader.SelectIrises();
                var trueCounter = 0;
                var counter = 0;
                foreach (var item in irisesTests)
                {
                    var outputValue = naiveBayes.Classify(item.Item1);
                    if (outputValue == item.Item2)
                        trueCounter++;
                    Debug.WriteLine(string.Format("Value {0} - Predicted {1} = {2}",
                        item.Item2, outputValue, (outputValue == item.Item2) ? "true" : "false"));
                    counter++;
                }
                Debug.WriteLine(string.Format(" i = {0} Data {1} - True {2} Verhältnis: {3}", i,
                    counter.ToString(), trueCounter.ToString(), (Convert.ToDouble(trueCounter) / Convert.ToDouble(counter)).ToString()));
            }
        }

        [TestMethod]
        public void NaiveBayesMushroomTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var mushroom = dataSetLoader.SelectMushroom();
            for (double i = 0; i < 1; i = i + 1)
            {
                NaiveBayesClassifier naiveBayes =
                    new NaiveBayesClassifier(mushroom, new LinearBayesKernel(mushroom));
                var mushroomTests = dataSetLoader.SelectMushroom();
                var trueCounter = 0;
                var counter = 0;
                foreach (var item in mushroomTests)
                {
                    var outputValue = naiveBayes.Classify(item.Item1);
                    if (outputValue == item.Item2)
                        trueCounter++;
                    Debug.WriteLine(string.Format("Value {0} - Predicted {1} = {2}",
                        item.Item2, outputValue, (outputValue == item.Item2) ? "true" : "false"));
                    counter++;
                }
                Debug.WriteLine(string.Format(" i = {0} Data {1} - True {2} Verhältnis: {3}", i,
                    counter.ToString(), trueCounter.ToString(), (Convert.ToDouble(trueCounter) / Convert.ToDouble(counter)).ToString()));
            }
        }

        [TestMethod]
        public void NaiveBayesMushroomTrainAndClassify8020Test()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var mushroom = dataSetLoader.SelectTrainingMushroom(80);
            for (double i = 0; i < 1; i = i + 1)
            {
                NaiveBayesClassifier naiveBayes =
                    new NaiveBayesClassifier(mushroom, new LinearBayesKernel(mushroom));
                var mushroomTests = dataSetLoader.SelectSelectingMushroom(20);
                var trueCounter = 0;
                var counter = 0;
                foreach (var item in mushroomTests)
                {
                    var outputValue = naiveBayes.Classify(item.Item1);
                    if (outputValue == item.Item2)
                        trueCounter++;
                    Debug.WriteLine(string.Format("Value {0} - Predicted {1} = {2}",
                        item.Item2, outputValue, (outputValue == item.Item2) ? "true" : "false"));
                    counter++;
                }
                Debug.WriteLine(string.Format(" i = {0} Data {1} - True {2} Verhältnis: {3}", i,
                    counter.ToString(), trueCounter.ToString(), (Convert.ToDouble(trueCounter) / Convert.ToDouble(counter)).ToString()));
            }
        }
    }
}
