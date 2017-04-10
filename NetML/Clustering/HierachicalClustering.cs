using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.kMeans
{
    public class HierachicalClustering : Clustering
    {
        private int maxCount;
        public List<Tuple<int, double[]>> centroids = new List<Tuple<int, double[]>>();

        public HierachicalClustering(List<double[]> inputData, int maxCount = 30)
        {
            this.inputData = inputData;
            this.maxCount = maxCount;
        }
        public override List<Tuple<int, double[]>>[] Cluster()
        {
            var n = inputData.Count;
            var clusterCount = Math.Sqrt(n / 2);
            return Cluster((int)clusterCount);
        }

        public override List<Tuple<int, double[]>>[] Cluster(int clusterCount)
        {
            List<Tuple<int, double[]>>[] clusters =
                new List<Tuple<int, double[]>>[clusterCount];
            //var counter = 1;
            //foreach (var item in inputData)
            //{
            //    var mod = counter % clusterCount;
            //    if (clusters[mod] == null)
            //        clusters[mod] = new List<Tuple<int, double[]>>();
            //    clusters[mod].Add(new Tuple<int, double[]>(mod, item));
            //    counter++;
            //}
            //var resultClusters = OptimizeCluster(clusterCount, clusters, inputData);
            //return resultClusters;
            return null;
        }

        public override int CalculateClusterAffinity(double[] clusteringData)
        {
            throw new NotImplementedException();
        }
    }
}
