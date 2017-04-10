using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.kNearestNeighbors
{
    public abstract class Metric
    {
        public abstract double CalculateMetric(List<Tuple<double, double>> calculationPairs);
        public abstract Tuple<int, double> CalculateClusterMetric(double[] value, Tuple<int, double[]> centruid);
        public abstract Tuple<int, double> CalculateClusterMetric(double[] value, Tuple<int, double[], double[]> metroid);
    }
}
