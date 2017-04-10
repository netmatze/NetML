using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using NetML.kMeans;
using System.Diagnostics;
using NetML.kNearestNeighbors;

namespace NetMLTests
{
    [TestClass]
    public class kMeansTests
    {
        [TestMethod]
        public void kMeansTest()
        {
            List<double[]> centroidData = new List<double[]>();
            foreach (var item in Enumerable.Range(0, 2))
            {
                foreach (var innerItem in Enumerable.Range(0, 2))
                {
                    centroidData.Add(new double[] { item, innerItem });
                }
            }
            kMeansClustering kmeansClustering = new kMeansClustering(centroidData);
            var clusters = kmeansClustering.Cluster(2);
            //var centroids = kmeansClustering.centroids;
            //var clusters = kmeansClustering.ClusterWithCentroid(4);            
        }

        [TestMethod]
        public void kMeansIrisTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var irises = dataSetLoader.SelectClusteringIrises();            
            kMeansClustering kmeansClustering = new kMeansClustering(irises, new MaximumMetric());
            var clusters = kmeansClustering.Cluster(3);
            var clusterCounter = 0;
            Dictionary<int, int> clusterDictonary = new Dictionary<int, int>();
            foreach(var cluster in clusters)
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
            //var centroids = kmeansClustering.centroids;
            //var clusters = kmeansClustering.ClusterWithCentroid(4);            
        }
    }
}
