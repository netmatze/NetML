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

namespace NetMLTests
{
    [TestClass]
    public class DecissionTreeTests
    {
        [TestMethod]
        public void DecisionTreeTest()
        {
            List<Tuple<double[], double>> data =
                new List<Tuple<double[], double>>();
            data.Add(new Tuple<double[], double>(new double[] { 0.0, 3.0, 0.0, 0.0 }, 40.0));
            data.Add(new Tuple<double[], double>(new double[] { 0.0, 3.0, 1.0, 1.0 }, 50.0));
            data.Add(new Tuple<double[], double>(new double[] { 1.0, 3.0, 1.0, 1.0 }, 50.0));          
            DecisionTreeClassifier decisionTreeClassifier =
                new DecisionTreeClassifier(data, new ShannonEntropySplitter());
            decisionTreeClassifier.Train();
            var result = decisionTreeClassifier.Classify(new double[] { 0.0, 3.0, 0.0, 0.0 });
            result = decisionTreeClassifier.Classify(new double[] { 0.0, 3.0, 1.0, 1.0 });
            result = decisionTreeClassifier.Classify(new double[] { 1.0, 3.0, 0.0, 0.0 });
            result = decisionTreeClassifier.Classify(new double[] { 1.0, 0.0, 0.0, 0.0 });
        }

        [TestMethod]
        public void AnimalDecisionTreeTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var animals = dataSetLoader.SelectAnimals();
            DecisionTreeClassifier decisionTreeClassifier =
            new DecisionTreeClassifier(animals, new ShannonEntropySplitter());
            decisionTreeClassifier.Train();
            var animalsTest = dataSetLoader.SelectAnimals();
            var trueCounter = 0;
            var counter = 0;
            foreach (var item in animalsTest)
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
        public void AnimalDecisionTreeTrainAndClassify8020Test()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var animals = dataSetLoader.SelectTrainingAnimals(90);
            DecisionTreeClassifier decisionTreeClassifier =
            new DecisionTreeClassifier(animals, new ShannonEntropySplitter());
            decisionTreeClassifier.Train();
            var animalsTest = dataSetLoader.SelectSelectingAnimals(10);
            var trueCounter = 0;
            var counter = 0;
            foreach (var item in animalsTest)
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
        public void IrisDecisionTreeTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var irises = dataSetLoader.SelectIrises();
            DecisionTreeClassifier decisionTreeClassifier =
            new DecisionTreeClassifier(irises, new ShannonEntropySplitter());
            decisionTreeClassifier.Train();
            var animalsTest = dataSetLoader.SelectIrises();
            var trueCounter = 0;
            var counter = 0;
            foreach (var item in animalsTest)
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
        public void IrisDecisionTreeTrainAndClassify8020Test()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var irises = dataSetLoader.SelectTrainingIrises(90);
            DecisionTreeClassifier decisionTreeClassifier =
            new DecisionTreeClassifier(irises, new ShannonEntropySplitter());
            decisionTreeClassifier.Train();
            var animalsTest = dataSetLoader.SelectSelectingIrises(10);
            var trueCounter = 0;
            var counter = 0;
            foreach (var item in animalsTest)
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
        public void TennisDecisionTreeTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var tennis = dataSetLoader.SelectTennis();
            DecisionTreeClassifier decisionTreeClassifier =
            new DecisionTreeClassifier(tennis, new ShannonEntropySplitter());
            decisionTreeClassifier.Train();
            var tennisTest = dataSetLoader.SelectTennis();
            var trueCounter = 0;
            var counter = 0;
            foreach (var item in tennisTest)
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
        public void ComputerVendorsDecisionTreeTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var computerVendors = dataSetLoader.SelectComputerVendors();
            DecisionTreeClassifier decisionTreeClassifier =
            new DecisionTreeClassifier(computerVendors, new ShannonEntropySplitter());
            decisionTreeClassifier.Train();
            var computerVendorsTest = dataSetLoader.SelectComputerVendors();
            var trueCounter = 0;
            var counter = 0;
            foreach (var item in computerVendorsTest)
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
        public void ComputerVendorsDecisionTreeTrainAndClassify8020Test()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var computerVendors = dataSetLoader.SelectTrainingComputerVendors(90);
            DecisionTreeClassifier decisionTreeClassifier =
            new DecisionTreeClassifier(computerVendors, new ShannonEntropySplitter());
            decisionTreeClassifier.Train();
            var computerVendorsTest = dataSetLoader.SelectSelectingComputerVendors(10);
            var trueCounter = 0;
            var counter = 0;
            foreach (var item in computerVendorsTest)
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
        public void MushroomDecisionTreeTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var mushroom = dataSetLoader.SelectMushroom();
            DecisionTreeClassifier decisionTreeClassifier =
            new DecisionTreeClassifier(mushroom, new ShannonEntropySplitter());
            decisionTreeClassifier.Train();
            var mushroomTest = dataSetLoader.SelectMushroom();
            var trueCounter = 0;
            var counter = 0;
            foreach (var item in mushroomTest)
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
        public void CreditDataDecisionTreeTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var creditData = dataSetLoader.SelectCreditData();
            DecisionTreeClassifier decisionTreeClassifier =
            new DecisionTreeClassifier(creditData, new ShannonEntropySplitter());
            decisionTreeClassifier.Train();
            var creditDataTest = dataSetLoader.SelectCreditData();
            var trueCounter = 0;
            var counter = 0;
            foreach (var item in creditDataTest)
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
        public void MushroomDecisionTreeTrainAndClassify8020Test()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var mushroom = dataSetLoader.SelectTrainingMushroom(80);
            DecisionTreeClassifier decisionTreeClassifier =
            new DecisionTreeClassifier(mushroom, new ShannonEntropySplitter());
            decisionTreeClassifier.Train();
            var mushroomTest = dataSetLoader.SelectSelectingMushroom(20);
            var trueCounter = 0;
            var counter = 0;
            foreach (var item in mushroomTest)
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
        public void IrisBoostingTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var irises = dataSetLoader.SelectIrises();
            for (double i = 0; i < 1; i = i + 1)
            {
                BoostingAlgorithmus boostingAlgorithmus =
                    new BoostingAlgorithmus(10);
                boostingAlgorithmus.Train(irises, new ShannonEntropySplitter());
                var irisesTest = dataSetLoader.SelectIrises();
                var trueCounter = 0;
                var counter = 0;
                foreach (var item in irisesTest)
                {
                    var outputValue = boostingAlgorithmus.Classify(item.Item1);
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
        public void IrisBoostingTrainAndClassify8020Test()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var irises = dataSetLoader.SelectTrainingIrises(90);
            for (double i = 0; i < 1; i = i + 1)
            {
                BoostingAlgorithmus boostingAlgorithmus =
                    new BoostingAlgorithmus(10);
                boostingAlgorithmus.Train(irises, new ShannonEntropySplitter());
                var irisesTest = dataSetLoader.SelectSelectingIrises(10);
                var trueCounter = 0;
                var counter = 0;
                foreach (var item in irisesTest)
                {
                    var outputValue = boostingAlgorithmus.Classify(item.Item1);
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
        public void AnimalBaggingTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var animals = dataSetLoader.SelectAnimals();
            for (double i = 0; i < 1; i = i + 1)
            {
                BaggingAlgorithmus baggingAlgorithmus =
                    new BaggingAlgorithmus(20);
                baggingAlgorithmus.Train(animals, new ShannonEntropySplitter());
                var animalsTest = dataSetLoader.SelectAnimals();
                var trueCounter = 0;
                var counter = 0;
                foreach (var item in animalsTest)
                {
                    var outputValue = baggingAlgorithmus.Classify(item.Item1);
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
        public void AnimalBoostingTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var animals = dataSetLoader.SelectAnimals();
            for (double i = 0; i < 1; i = i + 1)
            {
                BoostingAlgorithmus boostingAlgorithmus =
                    new BoostingAlgorithmus(10);
                boostingAlgorithmus.Train(animals, new ShannonEntropySplitter());
                var animalsTest = dataSetLoader.SelectAnimals();
                var trueCounter = 0;
                var counter = 0;
                foreach (var item in animalsTest)
                {
                    var outputValue = boostingAlgorithmus.Classify(item.Item1);
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
        public void AnimalBoostingTrainAndClassify8020Test()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var animals = dataSetLoader.SelectTrainingAnimals(90);
            for (double i = 0; i < 1; i = i + 1)
            {
                BoostingAlgorithmus boostingAlgorithmus =
                    new BoostingAlgorithmus(10);
                boostingAlgorithmus.Train(animals, new ShannonEntropySplitter());
                var animalsTest = dataSetLoader.SelectSelectingAnimals(10);
                var trueCounter = 0;
                var counter = 0;
                foreach (var item in animalsTest)
                {
                    var outputValue = boostingAlgorithmus.Classify(item.Item1);
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
