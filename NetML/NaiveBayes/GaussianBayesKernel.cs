using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.NaiveBayes
{
    public class GaussianBayesKernel : BayesKernel
    {
        public GaussianBayesKernel(List<Tuple<double[], double>> data)
        {
            base.data = data;
        }

        public override double Classify(double[] inputValues)
        {
            var n = (double)data.Count;
            var jn = CountClass();
            var pj = new double[jn];
            var dn = data[0].Item1.Length;
            for (int j = 0; j < jn; j++)
            {
                var array = classes.OrderBy(k => k.Key).ToArray();
                var cj = array[j].Key;
                double countcj = array[j].Value;
                var pcj = countcj / (double)n;
                var pdcj = 1.0;
                for (int i = 0; i < dn; i++)
                {
                    var di = inputValues[i];
                    var countdi = CountAttributes(di, cj, i);
                    pdcj *= countdi;
                }
                pj[j] = pdcj * pcj;
            }
            var maxValue = 0.0;
            var maxItem = 0.0;
            var resultarray = classes.OrderBy(k => k.Key).ToArray();
            for (int i = 0; i < pj.Length; i++)
            {
                if (pj[i] > maxValue)
                {
                    maxItem = resultarray[i].Key;
                    maxValue = pj[i];
                }
            }
            return maxItem;
        }

        private double CountAttributes(double di, double cj, int i)
        {
            var counter = 0.0;
            var meanValue = 0.0;
            foreach (var item in data)
            {
                if (item.Item2 == cj)
                {
                    var value = item.Item1[i];
                    meanValue += value;
                    counter++;
                }
            }
            if (counter == 0)
                counter = 1;
            var mean = meanValue / counter;
            var o2 = 0.0;
            var o2Value = 0.0;
            counter = 0;
            foreach (var item in data)
            {
                if (item.Item2 == cj)
                {
                    var value = (item.Item1[i] - mean) * (item.Item1[i] - mean);
                    o2Value += value;
                    counter++;
                }
            }
            if (counter == 1)
                counter = 2;
            o2 = o2Value / (counter - 1);
            if (o2 == 0.0)
                o2 = 1.0;
            var firstResult = -1 * ((di - mean) * (di - mean) / 2 * o2);
            var result = (1 / Math.Sqrt(2 * Math.PI * o2)) * Math.Pow(Math.E, firstResult);
            return result;
        }
    }
}
