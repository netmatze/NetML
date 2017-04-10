using NetML.kNearestNeighbors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.kMeans
{
    public class kMeansClustering : Clustering
    {
        private int maxCount;
        public List<Tuple<int, double[]>> centroids =
            new List<Tuple<int, double[]>>();        

        public kMeansClustering(List<double[]> inputData, int maxCount = 30)
        {
            this.inputData = inputData;
            this.maxCount = maxCount;
            base.metric = new EuclideMetric();
        }

        public kMeansClustering(List<double[]> inputData, Metric metric, int maxCount = 30) : this(inputData, maxCount)
        {
            base.metric = metric;   
        }

        public override List<Tuple<int, double[]>>[] Cluster()
        {            
            return Cluster(CalculateRuleOfThumb());
        }

        public override List<Tuple<int, double[]>>[] Cluster(int clusterCount)
        {            
            List<Tuple<int, double[]>>[] clusters =
                new List<Tuple<int, double[]>>[clusterCount];
            var counter = 1;
            foreach (var item in inputData)
            {
                var mod = counter % clusterCount;
                if (clusters[mod] == null)
                    clusters[mod] = new List<Tuple<int, double[]>>();
                clusters[mod].Add(new Tuple<int, double[]>(mod, item));
                counter++;
            }            
            var resultClusters = OptimizeCluster(clusterCount, clusters, inputData);
            return resultClusters;         
        }

        public List<Tuple<int, double[], double[]>>[] ClusterWithCentroid(int clusterCount)
        {
            List<Tuple<int, double[], double[]>>[] clusters =
                new List<Tuple<int, double[], double[]>>[clusterCount];
            var counter = 1;
            foreach (var item in inputData)
            {
                var mod = counter % clusterCount;
                if (clusters[mod] == null)
                    clusters[mod] = new List<Tuple<int, double[], double[]>>();
                clusters[mod].Add(new Tuple<int, double[], double[]>(mod, item, null));
                counter++;
            }
            var resultClusters = OptimizeClusterWithCentroid(clusterCount, clusters, inputData);
            return resultClusters;
        }

        public override int CalculateClusterAffinity(double[] clusteringData)
        {
            var selectedCluster = 0;
            var minClusterValue = System.Double.MaxValue;
            foreach(var cluster in centroids)
            {
                var result = base.metric.CalculateClusterMetric(clusteringData, cluster);
                if(result.Item2 < minClusterValue)
                {
                    minClusterValue = result.Item2;
                    selectedCluster = result.Item1;
                }
            }
            return selectedCluster;
        }

        public List<Tuple<int, double[]>>[] OptimizeCluster(
            int clusterCount,
            List<Tuple<int, double[]>>[] clusters,
            List<double[]> values)
        {            
            var counter = 0;
            while (counter < maxCount)
            {
                List<Tuple<int, double[]>> centroids = new List<Tuple<int, double[]>>();
                foreach (var cluster in clusters)
                {
                    double[] centroid = CalculateCentroid(cluster);
                    centroids.Add(new Tuple<int, double[]>
                        (cluster.First().Item1, centroid));
                }
                clusters = new List<Tuple<int, double[]>>[clusterCount];
                foreach (var value in values)
                {
                    Tuple<int, double> minDistance = new Tuple<int, double>(0, Double.MaxValue);
                    foreach (var item in centroids)
                    {
                        var result = CalculateDistance(value, item);
                        if (result.Item2 < minDistance.Item2)
                        {
                            minDistance = result;
                        }
                    }
                    if (clusters[minDistance.Item1] == null)
                    {
                        clusters[minDistance.Item1] = new List<Tuple<int, double[]>>();
                    }
                    clusters[minDistance.Item1].Add(
                        new Tuple<int, double[]>(minDistance.Item1, value));
                }
                counter++;
                this.centroids = centroids;
            }
            return clusters;
        }

        public List<Tuple<int, double[], double[]>>[] OptimizeClusterWithCentroid(int clusterCount,
           List<Tuple<int, double[], double[]>>[] clusters,
           List<double[]> values)
        {
            var counter = 0;
            while (counter < maxCount)
            {
                List<Tuple<int, double[]>> centroids = new List<Tuple<int, double[]>>();
                foreach (var cluster in clusters)
                {
                    if (cluster.Count > 0)
                    {
                        double[] centroid = CalculateCentroidWithCentroid(cluster);
                        centroids.Add(new Tuple<int, double[]>
                            (cluster.First().Item1, centroid));
                    }
                    else
                    {
                        double[] centroid = CalculateCentroidWithCentroid(cluster);
                        centroids.Add(new Tuple<int, double[]>
                            (-1, null));
                    }
                }
                clusters = new List<Tuple<int, double[], double[]>>[clusterCount];
                var clusterCounter = 0;
                foreach (var cluster in clusters)
                {
                    clusters[clusterCounter] = new List<Tuple<int, double[], double[]>>();
                    clusterCounter++;
                }
                foreach (var value in values)
                {
                    Tuple<int, double> minDistance = new Tuple<int, double>(0, Double.MaxValue);
                    foreach (var item in centroids)
                    {
                        if (item.Item1 > 0)
                        {
                            var result = CalculateDistance(value, item);
                            if (result.Item2 < minDistance.Item2)
                            {
                                minDistance = result;
                            }
                        }
                    }                    
                    var centroid = centroids[minDistance.Item1].Item2;
                    clusters[minDistance.Item1].Add(
                        new Tuple<int, double[], double[]>(minDistance.Item1, value, centroid));
                }
                counter++;
                this.centroids = centroids;
            }
            return clusters;
        }

        private Tuple<int, double> CalculateDistance(double[] value,
            Tuple<int, double[]> centruid)
        {            
            return base.metric.CalculateClusterMetric(value, centruid);
        }

        private double[] CalculateCentroid(List<Tuple<int, double[]>> cluster)
        {
            var count = cluster.Count();
            var sumItems = new double[cluster[0].Item2.Length];
            foreach (var item in cluster)
            {
                for (int i = 0; i < sumItems.Length; i++)
                {
                    sumItems[i] += item.Item2[i];
                }
            }
            var results = new double[sumItems.Length];
            for (int i = 0; i < results.Length; i++)
            {
                results[i] = sumItems[i] / count;
            }
            return results;
        }

        private double[] CalculateCentroidWithCentroid(List<Tuple<int, double[], double[]>> cluster)
        {
            var count = cluster.Count();
            if (count > 0)
            {
                var sumItems = new double[cluster[0].Item2.Length];
                foreach (var item in cluster)
                {
                    for (int i = 0; i < sumItems.Length; i++)
                    {
                        sumItems[i] += item.Item2[i];
                    }
                }
                var results = new double[sumItems.Length];
                for (int i = 0; i < results.Length; i++)
                {
                    results[i] = sumItems[i] / count;
                }
                return results;
            }
            return null;
        }
    }
}
