using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using netMLLanguage.Parser;
using System.Diagnostics;
using System.Collections.Generic;

namespace NetMLTests
{
    [TestClass]
    public class NetMLLanguageTests
    {
        [TestMethod]
        public void kNNLanguageTest()
        {
            var netMLString = " create classification knn euclidmetric ";
            NetMLCreator netMLCreator = new NetMLCreator(netMLString);
            DataSetLoader dataSetLoader = new DataSetLoader();
            var data = dataSetLoader.SelectAnimals();
            netMLCreator.Create(data);
            netMLCreator.Train();
            var testData = dataSetLoader.SelectAnimals();
            var trueCounter = 0;
            var counter = 0;
            foreach (var item in testData)
            {
                var outputValue = netMLCreator.Classify(item.Item1);
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
        public void DecisionTreeLanguageTest()
        {
            var netMLString = "create classification decisiontree";
            NetMLParser netMLParser = new NetMLParser();
            var result = netMLParser.Parse(netMLString);
            NetMLCreator netMLCreator = new NetMLCreator(result);
            DataSetLoader dataSetLoader = new DataSetLoader();
            var data = dataSetLoader.SelectAnimals();
            netMLCreator.Create(data);
            netMLCreator.Train();
            var testData = dataSetLoader.SelectAnimals();
            var trueCounter = 0;
            var counter = 0;
            foreach (var item in testData)
            {
                var outputValue = netMLCreator.Classify(item.Item1);
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
        public void NaivebayersLanguageTest()
        {
            var netMLString = "create classification naivebayers linearbayeskernel";           
            NetMLCreator netMLCreator = new NetMLCreator(netMLString);
            DataSetLoader dataSetLoader = new DataSetLoader();
            var data = dataSetLoader.SelectAnimals();
            netMLCreator.Create(data);
            netMLCreator.Train();
            var testData = dataSetLoader.SelectAnimals();
            var trueCounter = 0;
            var counter = 0;
            foreach (var item in testData)
            {
                var outputValue = netMLCreator.Classify(item.Item1);
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
        public void RandomforestLanguageTest()
        {
            var netMLString = "create classification randomforest boosting"; //shannonentropysplitter
            NetMLCreator netMLCreator = new NetMLCreator(netMLString);
            DataSetLoader dataSetLoader = new DataSetLoader();
            var data = dataSetLoader.SelectAnimals();
            netMLCreator.Create(data);
            netMLCreator.Train();
            var testData = dataSetLoader.SelectAnimals();
            var trueCounter = 0;
            var counter = 0;
            foreach (var item in testData)
            {
                var outputValue = netMLCreator.Classify(item.Item1);
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
        public void SupportvectormachineLanguageTest()
        {
            var netMLString =
                "create classification supportvectormachine linearkernel n = 0.001 c = 10.0 ";
            NetMLCreator netMLCreator = new NetMLCreator(netMLString);
            DataSetLoader dataSetLoader = new DataSetLoader();
            var data = dataSetLoader.SelectAnimals();
            netMLCreator.Create(data);
            netMLCreator.Train();
            var testData = dataSetLoader.SelectAnimals();
            var trueCounter = 0;
            var counter = 0;
            foreach (var item in testData)
            {
                var outputValue = netMLCreator.Classify(item.Item1);
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
        public void BackprobpagationLanguageTest()
        {
            var netMLString =
                "create classification backpropagation inputneurons = 16 outputneurons = 1 firsthiddenlayerneurons = 16 evolutions = 100 learningrate = 0.1 ";
            NetMLParser netMLParser = new NetMLParser();
            var result = netMLParser.Parse(netMLString);
            NetMLCreator netMLCreator = new NetMLCreator(result);
            DataSetLoader dataSetLoader = new DataSetLoader();
            var data = dataSetLoader.SelectAnimals();
            netMLCreator.Create(data);
            netMLCreator.Train();
            var testData = dataSetLoader.SelectAnimals();
            var trueCounter = 0;
            var counter = 0;
            foreach (var item in testData)
            {
                var outputValue = netMLCreator.Classify(item.Item1);
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
        public void RadialbasisfunctionLanguageTest()
        {
            var netMLString =
                "create classification radialbasisfunction inputneurons = 5 outputneurons = 15 firsthiddenlayerneurons = 2 evolutions = 100 learningrate = 0.1 ";
            NetMLParser netMLParser = new NetMLParser();
            var result = netMLParser.Parse(netMLString);
        }

        [TestMethod]
        public void ClusteringLanguageTest()
        {
            var netMLString =
                "create clustering kmetroids euclidmetric ";
            NetMLParser netMLParser = new NetMLParser();
            var result = netMLParser.Parse(netMLString);
            NetMLCreator netMLCreator = new NetMLCreator(result);
            DataSetLoader dataSetLoader = new DataSetLoader();
            var irises = dataSetLoader.SelectClusteringIrises();
            netMLCreator.Create(irises);
            var clusters = netMLCreator.Cluster(3);
            var clusterCounter = 0;
            Dictionary<int, int> clusterDictonary = new Dictionary<int, int>();
            foreach (var cluster in clusters)
            {
                Debug.WriteLine(string.Format("Cluster {0} - Count {1}", clusterCounter, cluster.Count));
                clusterDictonary.Add(clusterCounter, 0);
                clusterCounter++;
            }
            var irisesTest = dataSetLoader.SelectClusteringIrises();
            var trueCounter = 0;
            var counter = 0;
            foreach (var item in irisesTest)
            {
                var outputValue = netMLCreator.CalculateClusterAffinity(item);
                Debug.WriteLine(string.Format("Value {0} - Predicted {1}", item, outputValue));
                clusterDictonary[outputValue]++;
                counter++;
                trueCounter++;
            }
            clusterCounter = 0;
            foreach (var cluster in clusters)
            {
                var calculatedCluster = clusterDictonary[clusterCounter];
                Debug.WriteLine(string.Format("Cluster {0} - Original Count {1} - Calculated Count {2}",
                    clusterCounter, cluster.Count, calculatedCluster));
                clusterCounter++;
            }
        }
    }
}
