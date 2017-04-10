using NetML.DecisionTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.RandomForest
{
    public abstract class RandomForestAlgorithm
    {
        public abstract void Train(List<Tuple<double[], double>> data, Splitter splitter);

        public abstract double Classify(double[] inputValues);        
    }
}
