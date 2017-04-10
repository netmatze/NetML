using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.kNearestNeighbors
{
    public class KNearestNeighbors
    {
        public double Classify(double[] inputValue, List<Tuple<double[], double>> data,
            int k, Metric metric)
        {
            SortedList<double, Tuple<double[], double>> sortedList = 
                new SortedList<double, Tuple<double[], double>>();
            foreach (var classes in data)
            {
                List<Tuple<double, double>> calculationPairs =
                    new List<Tuple<double, double>>();
                var i = 0;
                foreach (var classValue in inputValue)
                {
                    var inputAttribute = classes.Item1[i];
                    var dataAttribute = classValue;                                        
                    calculationPairs.Add(new Tuple<double, double>(inputAttribute, dataAttribute));                    
                    i++;
                }
                var result = metric.CalculateMetric(calculationPairs);
                if (!sortedList.Keys.Contains((int)result))
                    sortedList.Add(result, classes);
            }            
            var counter = 0;
            Dictionary<double, Tuple<double, int>> selected = new Dictionary<double, Tuple<double, int>>();
            foreach (var item in sortedList)
            {
                if (counter < k)
                {
                    if (selected.ContainsKey(item.Key))
                    {
                        var count = selected[item.Key].Item2;
                        selected[item.Key] = new Tuple<double,int>(item.Value.Item2, count++);
                    }
                    else
                    {
                        selected.Add(item.Key, new Tuple<double, int>(item.Value.Item2, 1));
                    }
                }
                else
                {
                    break;
                }
                counter++;
            }
            var maxLable = 0.0;
            var maxValue = 0.0;
            foreach (var classes in selected)
            {
                if (classes.Value.Item1 > maxValue)
                {
                    maxValue = classes.Value.Item2;
                    maxLable = classes.Value.Item1;
                }
            }
            return maxLable;
        }
    }
}
