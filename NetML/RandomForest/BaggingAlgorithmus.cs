using System;
using System.Collections.Generic;
using System.Linq;

namespace NetML.RandomForest
{
    public class BaggingAlgorithmus : RandomForestAlgorithm
    {
        private List<DecisionTree.DecisionTreeClassifier> classifiers 
            = new List<DecisionTree.DecisionTreeClassifier>();
        private int ensembleSizeT;

        public BaggingAlgorithmus(int ensembleSizeT)
        {
            this.ensembleSizeT = ensembleSizeT;
        }

        public override void Train(List<Tuple<double[], double>> data, 
            DecisionTree.Splitter splitter)
        {
            var n = data.Count;
            var bootstrapSampleSize = n * 0.8;
            var dataArray = data.ToArray();
            for(int t = 0; t < ensembleSizeT; t++)
            {
                var sample = new List<Tuple<double[], double>>();
                Random random = new Random();
                for (int i = 0; i < bootstrapSampleSize; i++)
                {
                    var number = random.Next(0, n);
                    sample.Add(dataArray[number]);
                }
                var classifier = new 
                    DecisionTree.DecisionTreeClassifier(sample, splitter);
                classifier.Train();
                classifiers.Add(classifier);
            } 
        }

        public override double Classify(double[] inputValues)
        {
            var results = new Dictionary<double, int>();
            foreach (var item in classifiers)
            {
                var result = item.Classify(inputValues);
                if (result != Int32.MaxValue)
                {
                    if (results.ContainsKey(result))
                    {
                        results[result]++;
                    }
                    else
                    {
                        results.Add(result, 1);
                    }
                }
            }
            var maxValue = 0.0;
            var maxCounter = 0;
            foreach(var item in results)
            {
                if (item.Value > maxCounter)
                {
                    maxCounter = item.Value;
                    maxValue = item.Key;
                }
            }
            return maxValue;
        }
    }
}
