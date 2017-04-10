using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetML.DecisionTree;
using NetML.NaiveBayes;
using System.Collections.Generic;
using NetML.SupportVectorMachine;
using NetML.NeuronalNetworks;

namespace NetMLTests
{
    [TestClass]
    public class DifferentClassifications
    {
        [TestMethod]
        public void AnimalClassifyMethod()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            var animals = dataSetLoader.SelectAnimals();
            var data = dataSetLoader.CalculatePercent(50, animals);
            DecisionTreeClassifier decisionTreeClassifier =
                new DecisionTreeClassifier(data.Item1, new ShannonEntropySplitter());
            NaiveBayesClassifier naiveBayes =
                  new NaiveBayesClassifier(data.Item1);
            var list = new List<NetML.Classification>();
            Kernel kernel = new LinearKernel();
            SVMClassifier animalSVMClassifier = 
                new SVMClassifier(animals, kernel, 0.001, 10.0);
            var neuronalAnimals = dataSetLoader.SelectNeuronalNetworkAnimals();                         
            NeuronalNetworkClassifier neuronalNetworkClassifier =
                new NeuronalNetworkClassifier(neuronalAnimals, 16, 7, 16, 500, 0.1);
            list.Add(decisionTreeClassifier);
            list.Add(naiveBayes);
            list.Add(animalSVMClassifier);
            list.Add(neuronalNetworkClassifier);
            Classifier classifier = new Classifier();
            classifier.Classify(list, data.Item2);
        }

        [TestMethod]
        public void CreditDataClassifyMethod()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            var creditData = dataSetLoader.SelectCreditData();                        
            var data = dataSetLoader.CalculatePercent(100, creditData);
            DecisionTreeClassifier decisionTreeClassifier =
                new DecisionTreeClassifier(data.Item1, new ShannonEntropySplitter());
            NaiveBayesClassifier naiveBayes =
                  new NaiveBayesClassifier(data.Item1);
            var list = new List<NetML.Classification>();
            Kernel kernel = new LinearKernel();
            SVMClassifier SVMClassifier =
                new SVMClassifier(creditData, kernel, 0.001, 10.0);
            var neuronalCreditData = dataSetLoader.SelectNeuronalNetworksCreditData();
            NeuronalNetworkClassifier neuronalNetworkClassifier =
                new NeuronalNetworkClassifier(neuronalCreditData, 20, 2, 20, 5000, 0.1);
            list.Add(decisionTreeClassifier);
            list.Add(naiveBayes);
            list.Add(SVMClassifier);
            //list.Add(neuronalNetworkClassifier);
            Classifier classifier = new Classifier();
            classifier.Classify(list, creditData);
        }
    }
}
