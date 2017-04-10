using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.NaiveBayes
{
    //http://en.wikipedia.org/wiki/Naive_Bayes_classifier
    public class NaiveBayesClassifier : Classification
    {
        private List<Tuple<double[], double>> data;
        //private Dictionary<double, int> classes = new Dictionary<double, int>();
        private BayesKernel kernel;
        //private Dictionary<double, double> probability = 
        //    new Dictionary<double,double>(); 
        //    // P(male), P(female)
        //private Dictionary<double, Dictionary<double, double>> conditionalProbability = 
        //    new Dictionary<double,Dictionary<double,double>>();
        //    // P(height|male), P(weight|male)

        public NaiveBayesClassifier(List<Tuple<double[], double>> data)
        {
            this.data = data;
            kernel = new LinearBayesKernel(data);
        }

        public NaiveBayesClassifier(List<Tuple<double[], double>> data, BayesKernel kernel)
        {
            this.data = data;
            this.kernel = kernel;            
        }

        public override void Train()
        {
            
        }

        public override double Classify(double[] inputValues)
        {
            return this.kernel.Classify(inputValues);
            //var n = (double) data.Count;
            //var jn = CountClass();
            //var pj = new double[jn];
            //var dn = data[0].Item1.Length;
            //for(int j = 0; j < jn; j++)
            //{
            //    var array = classes.OrderBy(k => k.Key).ToArray();
            //    var cj = array[j].Key;
            //    var countcj = array[j].Value;
            //    var pcj = countcj / n;
            //    var pdcj = 1.0;
            //    for(int i = 0; i < dn; i++)
            //    {
            //        var di = inputValues[i];
            //        var countdi = CountAttributes(di, cj, i);
            //        pdcj *= countdi / countcj;
            //    }
            //    pj[j] = pdcj * pcj;
            //}
            //var maxValue = 0.0;
            //var maxItem = 0.0;
            //var resultarray = classes.OrderBy(k => k.Key).ToArray();
            //for (int i = 0; i < pj.Length; i++)
            //{
            //    if (pj[i] > maxValue)
            //    {
            //        maxItem = resultarray[i].Key;
            //        maxValue = pj[i];
            //    }
            //}
            //return maxItem;
        }

        //public double ClassifyGaussian(double[] inputValues)
        //{
        //    var n = (double)data.Count;
        //    var jn = CountClass();
        //    var pj = new double[jn];
        //    var dn = data[0].Item1.Length;
        //    for (int j = 0; j < jn; j++)
        //    {
        //        var array = classes.OrderBy(k => k.Key).ToArray();
        //        var cj = array[j].Key;
        //        double countcj = array[j].Value;
        //        var pcj = countcj / (double) n;
        //        var pdcj = 1.0;
        //        for (int i = 0; i < dn; i++)
        //        {
        //            var di = inputValues[i];
        //            var countdi = CountAttributesGaussian(di, cj, i);
        //            pdcj *= countdi;
        //        }
        //        pj[j] = pdcj * pcj;
        //    }
        //    var maxValue = 0.0;
        //    var maxItem = 0.0;
        //    var resultarray = classes.OrderBy(k => k.Key).ToArray();
        //    for (int i = 0; i < pj.Length; i++)
        //    {
        //        if (pj[i] > maxValue)
        //        {
        //            maxItem = resultarray[i].Key;
        //            maxValue = pj[i];
        //        }
        //    }
        //    return maxItem;
        //}

        //private double CountAttributes(double di, double cj, int i)
        //{
        //    var counter = 0.0;
        //    foreach (var item in data)
        //    {
        //        if (item.Item2 == cj)
        //        {
        //            if (item.Item1[i] == di)
        //            {
        //                counter++;
        //            }
        //        }
        //    }
        //    return counter;
        //}

        //private double CountAttributesGaussian(double di, double cj, int i)
        //{
        //    var counter = 0.0;
        //    var meanValue = 0.0;
        //    foreach(var item in data)
        //    {
        //        if (item.Item2 == cj)
        //        {
        //            var value = item.Item1[i];
        //            meanValue += value;
        //            counter++;
        //        }
        //    }
        //    if (counter == 0)
        //        counter = 1;
        //    var mean = meanValue / counter;
        //    var o2 = 0.0;
        //    var o2Value = 0.0;
        //    counter = 0;
        //    foreach (var item in data)
        //    {
        //        if (item.Item2 == cj)
        //        {
        //            var value = (item.Item1[i] - mean) * (item.Item1[i] - mean);
        //            o2Value += value;
        //            counter++;
        //        }
        //    }
        //    if (counter == 1)
        //        counter = 2;
        //    o2 = o2Value / (counter - 1);
        //    if (o2 == 0.0)
        //        o2 = 1.0;
        //    var firstResult = -1 * ((di - mean) * (di - mean) / 2 * o2);
        //    var result = (1 / Math.Sqrt(2 * Math.PI * o2)) * Math.Pow(Math.E, firstResult);
        //    return result;
        //}

        //private int CountClass()
        //{
        //    foreach(var item in data)
        //    {
        //        if (!classes.ContainsKey(item.Item2))
        //        {
        //            classes.Add(item.Item2, 1);
        //        }
        //        else
        //        {
        //            classes[item.Item2]++;
        //        }
        //    }
        //    return classes.Count;
        //}
    }
}
