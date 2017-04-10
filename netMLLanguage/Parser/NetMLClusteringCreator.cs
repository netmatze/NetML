using NetML;
using NetML.kMeans;
using NetML.kNearestNeighbors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace netMLLanguage.Parser
{
    public class NetMLClusteringCreator
    {
        private NetMLObject netMLObject;
        private Clustering clustering;

        public NetMLClusteringCreator(NetMLObject netMLObject)
        {
            this.netMLObject = netMLObject;
        }

        public void Create(List<double[]> data)
        {
            switch (netMLObject.Algorithmus)
            {
                case "kmeans":
                    Kmeans(data);
                    break;
                case "kmetroids":
                    Kmetroids(data);
                    break;
            }
        }

        private void Kmeans(List<double[]> data)
        {           
            var metric = FindMetric();
            clustering = new kMeansClustering(data, metric);
        }

        private void Kmetroids(List<double[]> data)
        {
            var metric = FindMetric();
            clustering = new kMetroidClustering(data, metric);
        }

        private Metric FindMetric()
        {
            Metric metric = new EuclideMetric();
            switch (netMLObject.Options.First())
            {
                case "euclidmetric":
                    metric = new EuclideMetric();
                    break;
                case "manhattanmetric":
                    metric = new ManhattanMetric();
                    break;
                case "maximummetric":
                    metric = new MaximumMetric();
                    break;
                case "squaredeuclidmetric":
                    metric = new SquaredEuclideMetric();
                    break;
            }
            return metric;
        }

        public List<Tuple<int, double[]>>[] Cluster()
        {
            return clustering.Cluster();
        }

        public List<Tuple<int, double[]>>[] Cluster(int clusterCount)
        {
            return clustering.Cluster(clusterCount);
        }

        public int CalculateClusterAffinity(double[] clusteringData)
        {
            return clustering.CalculateClusterAffinity(clusteringData);
        }
    }
}
