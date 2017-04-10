using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.kNearestNeighbors
{
    public class SquaredEuclideMetric : Metric
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
            return addResult;
        }

        public override Tuple<int, double> CalculateClusterMetric(double[] value, Tuple<int, double[]> centruid)
        {
            var addResult = 0.0;
            for (int i = 0; i < centruid.Item2.Length; i++)
            {
                addResult += Math.Pow((centruid.Item2[i] - value[i]), 2);
            }
            return new Tuple<int, double>(centruid.Item1, addResult);
        }

        public override Tuple<int, double> CalculateClusterMetric(double[] value, Tuple<int, double[], double[]> centruid)
        {
            var addResult = 0.0;
            for (int i = 0; i < centruid.Item2.Length; i++)
            {
                addResult += Math.Pow((centruid.Item2[i] - value[i]), 2);
            }
            return new Tuple<int, double>(centruid.Item1, addResult);
        }
    }
}
