using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetML.kMeans;
using System.Collections.Generic;
using System.Diagnostics;
using NetML.kNearestNeighbors;
using System.Linq;

namespace NetMLTests
{
    [TestClass]
    public class kMetroidTests
    {
        [TestMethod]
        public void kMetroidTest()
        {
            List<double[]> centroidData = new List<double[]>();
            centroidData.Add(new double[] { 10, 10 });
            centroidData.Add(new double[] { 1, 1 });
            centroidData.Add(new double[] { 9, 9 });
            centroidData.Add(new double[] { 3, 3 });
            centroidData.Add(new double[] { 2, 2 });
            centroidData.Add(new double[] { 8, 8 });
            kMetroidClustering kMetroidClustering = new kMetroidClustering(centroidData);
            var clusters = kMetroidClustering.Cluster(2);                    
        }

        [TestMethod]
        public void kMetroidIrisTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var irises = dataSetLoader.SelectClusteringIrises();
            kMetroidClustering kmeansClustering = new kMetroidClustering(irises, new EuclideMetric(), 100);
            var clusters = kmeansClustering.Cluster(3);
            var clusterCounter = 0;
            Dictionary<int, int> clusterDictonary = new Dictionary<int, int>();
            foreach (var cluster in clusters)
            {
                Debug.WriteLine(string.Format("Cluster {0} - Count {1}", clusterCounter, cluster.Count));
                clusterDictonary.Add(clusterCounter, 0);
                clusterCounter++;
            }
            var irisesTest = dataSetLoader.SelectIrises();
            var trueCounter = 0;
            var counter = 0;
            foreach (var item in irisesTest)
            {
                var outputValue = kmeansClustering.CalculateClusterAffinity(item.Item1);
                Debug.WriteLine(string.Format("Value {0} - Predicted {1} - ClassificationItem {2}", item, outputValue, item.Item2 - 1));
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

        [TestMethod]
        public void kMetroidIris8020Test()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var irises = dataSetLoader.SelectClusteringIrises();
            kMetroidClustering kmeansClustering = new kMetroidClustering(irises, new EuclideMetric());
            var clusters = kmeansClustering.Cluster(3);
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
                var outputValue = kmeansClustering.CalculateClusterAffinity(item);
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
