using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetML.DecisionTree;
using System.Collections.Generic;
using System.Diagnostics;
using NetML.SupportVectorMachine;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Globalization;

namespace NetMLTests
{
    [TestClass]
    public class SVMTests
    {
        [TestMethod]
        public void AnimalSupportVectorMachineClassifierTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var animals = dataSetLoader.SelectAnimals();
            for (double i = 0; i < 1; i = i + 1)
            {
                Kernel kernel = new LinearKernel();
                SVMClassifier animalSVMClassifier = new SVMClassifier(animals, kernel, 0.001, 10.0);
                animalSVMClassifier.Train();
                var animalsTest = dataSetLoader.SelectAnimals();
                var trueCounter = 0;
                var counter = 0;
                foreach (var item in animalsTest)
                {
                    var outputValue = animalSVMClassifier.Classify(item.Item1);
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
        public void AnimalSupportVectorMachineTrainAndClassify8020Test()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var animals = dataSetLoader.SelectTrainingAnimals(80);
            for (double i = 0; i < 1; i = i + 1)
            {
                Kernel kernel = new LinearKernel();
                SVMClassifier animalSVMClassifier = new SVMClassifier(animals, kernel, 0.001, 10.0);
                animalSVMClassifier.Train();
                var animalsTest = dataSetLoader.SelectSelectingAnimals(20);
                var trueCounter = 0;
                var counter = 0;
                foreach (var item in animalsTest)
                {
                    var outputValue = animalSVMClassifier.Classify(item.Item1);
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
        public void IrisSupportVectorMachineClassifierTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var irises = dataSetLoader.SelectIrises();
            for (double i = 0; i < 1; i = i + 1)
            {
                Kernel kernel = new LinearKernel();
                SVMClassifier animalSVMClassifier = new SVMClassifier(irises, kernel, 0.001, 10.0);
                animalSVMClassifier.Train();
                var irisesTest = dataSetLoader.SelectIrises();
                var trueCounter = 0;
                var counter = 0;
                foreach (var item in irisesTest)
                {
                    var outputValue = animalSVMClassifier.Classify(item.Item1);
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
        public void IrisSupportVectorMachineTrainAndClassify8020Test()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var irises = dataSetLoader.SelectTrainingIrises(90);
            for (double i = 0; i < 1; i = i + 1)
            {
                Kernel kernel = new LinearKernel();
                SVMClassifier animalSVMClassifier = new SVMClassifier(irises, kernel, 0.001, 10.0);
                animalSVMClassifier.Train();
                var irisesTest = dataSetLoader.SelectSelectingIrises(10);
                var trueCounter = 0;
                var counter = 0;
                foreach (var item in irisesTest)
                {
                    var outputValue = animalSVMClassifier.Classify(item.Item1);
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
        public void ComputerVendorsSupportVectorMachineClassifierTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var computerVendors = dataSetLoader.SelectComputerVendors();
            for (double i = 0; i < 1; i = i + 1)
            {
                Kernel kernel = new LinearKernel();
                SVMClassifier computerVendorsSVMClassifier = new SVMClassifier(computerVendors, kernel, 0.001, 10.0);
                computerVendorsSVMClassifier.Train();
                var computerVendorsTest = dataSetLoader.SelectComputerVendors();
                var trueCounter = 0;
                var counter = 0;
                foreach (var item in computerVendorsTest)
                {
                    var outputValue = computerVendorsSVMClassifier.Classify(item.Item1);
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
        public void MushroomSupportVectorMachineClassifierTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var mushroom = dataSetLoader.SelectMushroom();
            for (double i = 0; i < 1; i = i + 1)
            {
                Kernel kernel = new LinearKernel();
                SVMClassifier mushroomSVMClassifier = new SVMClassifier(mushroom, kernel, 0.001, 10.0);
                mushroomSVMClassifier.Train();
                var mushroomTest = dataSetLoader.SelectMushroom();
                var trueCounter = 0;
                var counter = 0;
                foreach (var item in mushroomTest)
                {
                    var outputValue = mushroomSVMClassifier.Classify(item.Item1);
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
        public void MushroomSupportVectorMachineTrainAndClassify8020Test()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var mushroom = dataSetLoader.SelectTrainingMushroom(80);
            for (double i = 0; i < 1; i = i + 1)
            {
                Kernel kernel = new LinearKernel();
                SVMClassifier mushroomSVMClassifier = new SVMClassifier(mushroom, kernel, 0.001, 10.0);
                mushroomSVMClassifier.Train();
                var mushroomTest = dataSetLoader.SelectSelectingMushroom(20);
                var trueCounter = 0;
                var counter = 0;
                foreach (var item in mushroomTest)
                {
                    var outputValue = mushroomSVMClassifier.Classify(item.Item1);
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
        public void TennisSupportVectorMachineClassifierTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var tennis = dataSetLoader.SelectTennis();
            for (double i = 0; i < 1; i = i + 1)
            {
                Kernel kernel = new LinearKernel();
                SVMClassifier tennisSVMClassifier = new SVMClassifier(tennis, kernel, 0.001, 10.0);
                tennisSVMClassifier.Train();
                var tennisTest = dataSetLoader.SelectTennis();
                var trueCounter = 0;
                var counter = 0;
                foreach (var item in tennisTest)
                {
                    var outputValue = tennisSVMClassifier.Classify(item.Item1);
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
        public void AnimalDualPerceptronTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var animals = dataSetLoader.SelectAnimals();
            for (double i = 0; i < 1; i = i + 1)
            {
                Kernel kernel = new LinearKernel();
                DualPerceptronClassifier dualPerceptronClassifier = new DualPerceptronClassifier(animals, kernel);
                dualPerceptronClassifier.Train();
                var animalsTest = dataSetLoader.SelectAnimals();
                var trueCounter = 0;
                var counter = 0;
                foreach (var item in animalsTest)
                {
                    var outputValue = dualPerceptronClassifier.Classify(item.Item1);
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
