using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.kNearestNeighbors
{
    public class MaximumMetric : Metric
    {
        public override double CalculateMetric(List<Tuple<double, double>> calculationPairs)
        {
            var maxResult = 0.0;
            foreach (var pair in calculationPairs)
            {
                var aX = pair.Item1;
                var bX = pair.Item2;
                maxResult += Math.Max(aX,bX);
            }
            return maxResult;
        }

        public override Tuple<int, double> CalculateClusterMetric(double[] value, Tuple<int, double[]> centruid)
        {
            var maxResult = 0.0;
            for (int i = 0; i < centruid.Item2.Length; i++)
            {
                maxResult += Math.Max(centruid.Item2[i],value[i]);
            }
            var result = Math.Sqrt(maxResult);
            return new Tuple<int, double>(centruid.Item1, result);
        }

        public override Tuple<int, double> CalculateClusterMetric(double[] value, Tuple<int, double[], double[]> centruid)
        {
            var maxResult = 0.0;
            for (int i = 0; i < centruid.Item2.Length; i++)
            {
                maxResult += Math.Max(centruid.Item2[i], value[i]);
            }
            var result = Math.Sqrt(maxResult);
            return new Tuple<int, double>(centruid.Item1, result);
        }
    }
}
