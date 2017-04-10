using NetML.DecisionTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.RandomForest
{
    public class RandomForestClassifier : Classification    
    {
        private List<Tuple<double[], double>> data;
        private Tree<double> tree;
        private Splitter splitter;
        private RandomForestAlgorithm randomForestAlgorithm;

        public RandomForestClassifier(List<Tuple<double[], double>> data, Splitter splitter,
            RandomForestAlgorithm randomForestAlgorithm)
        {
            this.data = data;
            this.splitter = splitter;
            this.randomForestAlgorithm = randomForestAlgorithm;
        }

        public override void Train()
        {
            randomForestAlgorithm.Train(data, splitter);
        }

        public override double Classify(double[] inputValues)
        {
            return randomForestAlgorithm.Classify(inputValues);
        }
    }
}
