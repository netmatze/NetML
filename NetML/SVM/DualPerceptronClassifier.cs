using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.SupportVectorMachine
{
    public class DualPerceptronClassifier : Classification
    {
        private Dictionary<double, List<double[]>> classes;
        private List<Tuple<double[], double>> data;
        private List<Tuple<double, DualPerceptron>> perceptrons;
        private Kernel kernel;

        public DualPerceptronClassifier(List<Tuple<double[], double>> data)
        {
            this.data = data;
            this.kernel = new LinearKernel();            
            classes = new Dictionary<double, List<double[]>>();
            perceptrons = new List<Tuple<double, DualPerceptron>>();
            data.ForEach((Tuple<double[], double> tuple) =>
            {
                if (!classes.ContainsKey(tuple.Item2))
                {
                    classes.Add(tuple.Item2, new List<double[]>());
                    classes[tuple.Item2].Add(tuple.Item1);
                }
                else
                {
                    classes[tuple.Item2].Add(tuple.Item1);
                }
            });
        }

        public DualPerceptronClassifier(List<Tuple<double[], double>> data, Kernel kernel)
        {
            this.data = data;
            this.kernel = kernel;
            classes = new Dictionary<double, List<double[]>>();
            perceptrons = new List<Tuple<double, DualPerceptron>>();
            data.ForEach((Tuple<double[], double> tuple) =>
            {
                if (!classes.ContainsKey(tuple.Item2))
                {
                    classes.Add(tuple.Item2, new List<double[]>());
                    classes[tuple.Item2].Add(tuple.Item1);
                }
                else
                {
                    classes[tuple.Item2].Add(tuple.Item1);
                }
            });
        }

        public override void Train()
        {
            foreach(var classItem in classes.OrderBy(kv => kv.Key))
            {
                List<Tuple<double[], double>> preparedItems = new List<Tuple<double[], double>>();
                foreach (var item in data)
                {
                    if(item.Item2 == classItem.Key)
                    {
                        preparedItems.Add(new Tuple<double[], double>(item.Item1, 1.0));
                    }
                    else
                    {
                        preparedItems.Add(new Tuple<double[], double>(item.Item1, -1.0));
                    }
                }
                DualPerceptron dualPerceptron = new DualPerceptron();
                dualPerceptron.TrainParallel(preparedItems, kernel);
                perceptrons.Add(new Tuple<double, DualPerceptron>(classItem.Key, dualPerceptron));
            }
        }

        public override double Classify(double[] inputValues)
        {
            var maxValue = double.MinValue;
            var returnClass = 0.0;
            foreach (var perceptron in perceptrons)
            {
                var value = perceptron.Item2.ClassifyKernelValue(inputValues, kernel);
                if(maxValue < value)
                {
                    maxValue = value;
                    returnClass = perceptron.Item1;
                }
            }
            return returnClass;
        }
    }
}
