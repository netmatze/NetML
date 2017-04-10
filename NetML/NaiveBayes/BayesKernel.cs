using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.NaiveBayes
{
    public abstract class BayesKernel
    {
        protected List<Tuple<double[], double>> data;
        protected Dictionary<double, int> classes = new Dictionary<double, int>();

        public abstract double Classify(double[] inputValues);

        protected int CountClass()
        {
            foreach (var item in data)
            {
                if (!classes.ContainsKey(item.Item2))
                {
                    classes.Add(item.Item2, 1);
                }
                else
                {
                    classes[item.Item2]++;
                }
            }
            return classes.Count;
        }
    }
}
