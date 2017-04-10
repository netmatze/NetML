using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.kNearestNeighbors
{
    public class KNearestNeighborsClassifier : Classification
    {
        private List<Tuple<double[], double>> data;
        private Metric iMetric;
        private int k;

        public KNearestNeighborsClassifier(
            List<Tuple<double[], double>> data, int k, Metric iMetric)
        {
            this.data = data;
            this.iMetric = iMetric;
            this.k = k;
        }

        public override void Train()
        {
            
        }

        public override double Classify(double[] inputValues)
        {
            var kNearestNeighbors = new KNearestNeighbors();
            return kNearestNeighbors.Classify(inputValues, data, k, iMetric);
        }
    }
}
