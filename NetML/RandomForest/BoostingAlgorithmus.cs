using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.RandomForest
{
    public class BoostingAlgorithmus : RandomForestAlgorithm
    {
        private List<Tuple<double, DecisionTree.DecisionTreeClassifier>> classifiers
            = new List<Tuple<double, DecisionTree.DecisionTreeClassifier>>();
        private int ensembleSizeT = 10;

        public BoostingAlgorithmus()
        {
        }

        public BoostingAlgorithmus(int ensembleSizeT)
        {
            this.ensembleSizeT = ensembleSizeT;
        }
        public override void Train(List<Tuple<double[], double>> data, 
            DecisionTree.Splitter splitter)
        {
            List<Tuple<double, double[], double>> list = Initialize(data);
            var sample = data;
            var alpha = 0.0;
            var epselonWrongCounter = 0.0;
            var epselon = 0.0;
            var n = data.Count;
            for (int i = 0; i < ensembleSizeT; i++)
            {
                if (i > 0)
                {
                    sample = BuildSample(list);
                }
                var classifier = new
                    DecisionTree.DecisionTreeClassifier(sample, splitter);
                classifier.Train();
                foreach(var value in sample)
                {
                    var result = classifier.Classify(value.Item1);
                    if(result != value.Item2)
                    {
                        epselonWrongCounter++;
                    }
                }
                epselon = epselonWrongCounter / (double)n;
                if (epselon == 0)
                {
                    classifiers.Add(new Tuple<double, DecisionTree.DecisionTreeClassifier>(alpha, classifier));
                    break;
                }
                else
                {
                    alpha = 0.5 * Math.Log(((1.0 - epselon) / epselon));
                    classifiers.Add(new Tuple<double, DecisionTree.DecisionTreeClassifier>(alpha, classifier));
                }
                list = Adjust(classifier, list, epselon);
                epselonWrongCounter = 0.0;               
            }
        }

        private List<Tuple<double[], double>> BuildSample(List<Tuple<double, double[], double>> data)
        {
            List<Tuple<double[], double>> sample = new List<Tuple<double[], double>>();
            Random random = new Random();
            var n = data.Count;
            var propabilityList = BuildPropabilityArray(data);
            for (int i = 0; i < n; i++)
            {
                var randomDouble = random.NextDouble();
                foreach(var propabilityItem in propabilityList)
                {
                    if(randomDouble > propabilityItem.Item1 && randomDouble < propabilityItem.Item2)
                    {
                        sample.Add(new Tuple<double[], double>
                            (propabilityItem.Item3, propabilityItem.Item4));
                        break;
                    }
                }
            }
            return sample;
        }

        private List<Tuple<double, double, double[], double>> BuildPropabilityArray(
            List<Tuple<double, double[], double>> data)
        {
            List<Tuple<double, double, double[], double>> probabilitySample = 
                new List<Tuple<double, double, double[], double>>();
            var from = 0.0;
            var to = 0.0;
            foreach(var item in data)
            {
                to = from + item.Item1;
                probabilitySample.Add(new Tuple<double, double, double[], double>(
                    from, to, item.Item2, item.Item3));
                from = to;
            }
            return probabilitySample;
        }

        private List <Tuple<double, double[], double>> Adjust(
            DecisionTree.DecisionTreeClassifier classifier,
            List<Tuple<double, double[], double>> data,
            double epselon)
        {
            List<Tuple<double, double[], double>> initList = 
                new List<Tuple<double, double[], double>>();
            foreach (var item in data)
            {
                var weight = item.Item1;
                var result = classifier.Classify(item.Item2);
                if(result == item.Item3)
                {
                    weight = weight / 2.0 * (1.0 - epselon);
                }
                else
                {
                    weight = weight / 2.0 * epselon;
                }
                Tuple<double, double[], double> weightItem
                    = new Tuple<double, double[], double>(weight, item.Item2, item.Item3);
                initList.Add(weightItem);
            }
            return initList;
        }

        private List<Tuple<double, double[], double>> Initialize(List<Tuple<double[], double>> data)
        {
            List<Tuple<double, double[], double>> initList = 
                new List<Tuple<double, double[], double>>();
            double weight = 1.0 / data.Count;
            foreach(var item in data)
            {
                Tuple<double, double[], double> weightItem 
                    = new Tuple<double, double[], double>(weight, item.Item1, item.Item2);
                initList.Add(weightItem);
            }
            return initList;
        }

        public override double Classify(double[] inputValues)
        {
            var results = new Dictionary<double, double>();
            foreach (var classifier in classifiers)
            {
                var result = classifier.Item2.Classify(inputValues);
                if (result != Int32.MaxValue)
                {
                    if (results.ContainsKey(result))
                    {
                        results[result] += classifier.Item1;
                    }
                    else
                    {
                        results.Add(result, classifier.Item1);
                    }
                }
            }
            var maxValue = 0.0;
            var maxAlphaValue = 0.0;
            foreach (var item in results)
            {
                if (item.Value >= maxAlphaValue)
                {
                    maxAlphaValue = item.Value;
                    maxValue = item.Key;
                }
            }
            return maxValue;
        }
    }
}
