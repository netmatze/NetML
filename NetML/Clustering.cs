using NetML.kNearestNeighbors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML
{
    public abstract class Clustering
    {
        protected List<double[]> inputData;
        public abstract List<Tuple<int, double[]>>[] Cluster();
        public abstract List<Tuple<int, double[]>>[] Cluster(int clusters);

        protected Metric metric;

        public abstract int CalculateClusterAffinity(double[] clusteringData); 

        protected int CalculateRuleOfThumb()
        {
            var n = inputData.Count;
            var clusterCount = Math.Sqrt(n / 2);            
            return (int)clusterCount;
        }
    }
}
