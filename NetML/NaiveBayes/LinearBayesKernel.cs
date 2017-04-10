using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.NaiveBayes
{
    public class LinearBayesKernel : BayesKernel
    {
        public LinearBayesKernel(List<Tuple<double[], double>> data)
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
                var countcj = array[j].Value;
                var pcj = countcj / n;
                var pdcj = 1.0;
                for (int i = 0; i < dn; i++)
                {
                    var di = inputValues[i];
                    var countdi = CountAttributes(di, cj, i);
                    pdcj *= countdi / countcj;
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
            foreach (var item in data)
            {
                if (item.Item2 == cj)
                {
                    if (item.Item1[i] == di)
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }
    }
}
