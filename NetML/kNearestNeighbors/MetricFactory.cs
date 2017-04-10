using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.kNearestNeighbors
{
    public class MetricFactory
    {
        public Metric Create(MetricEnum metric)
        {
            if(metric == MetricEnum.Manhattan)
            {
                return new ManhattanMetric();
            }
            else if(metric == MetricEnum.SquaredEuclid)
            {
                return new SquaredEuclideMetric();
            }
            else if(metric == MetricEnum.Maximum)
            {
                return new MaximumMetric();
            }
            return new EuclideMetric();
        }
    }

    public enum MetricEnum
    {
        Euclid,
        Manhattan,
        SquaredEuclid,
        Maximum
    }
}
