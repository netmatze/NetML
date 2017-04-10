using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.kNearestNeighbors
{
    public class EuclideMetric : Metric
    {
        public override double CalculateMetric(List<Tuple<double, double>> calculationPairs)
        {
            var addResult = 0.0;
            foreach (var pair in calculationPairs)
            {
                var aX = pair.Item1;
                var bX = pair.Item2;
                addResult += Math.Pow((aX - bX), 2);
            }
            var result = Math.Sqrt(addResult);
            return result;
        }

        public override Tuple<int, double> CalculateClusterMetric(double[] value, Tuple<int, double[]> centruid)
        {
            var powSum = 0.0;
            for (int i = 0; i<centruid.Item2.Length; i++)
            {
                powSum += Math.Pow((centruid.Item2[i] - value[i]), 2);
            }
            var result = Math.Sqrt(powSum);
            return new Tuple<int, double>(centruid.Item1, result);
        }

        public override Tuple<int, double> CalculateClusterMetric(double[] value, Tuple<int, double[], double[]> centruid)
        {
            var powSum = 0.0;
            for (int i = 0; i < centruid.Item2.Length; i++)
            {
                powSum += Math.Pow((centruid.Item2[i] - value[i]), 2);
            }
            var result = Math.Sqrt(powSum);
            return new Tuple<int, double>(centruid.Item1, result);
        }
    }
}
