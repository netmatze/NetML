using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.SupportVectorMachine
{
    public class SVMClassifier : Classification
    {
        private Dictionary<double, List<double[]>> classes;
        private Dictionary<string, List<double[]>> classesMultibleOutputvalues;
        private List<Tuple<double[], double>> data;
        private List<Tuple<double[], double[]>> dataMultibleOutputvalues;
        private List<Tuple<double, SVM>> supportVectorMachines;
        private Kernel kernel;
        private double n;
        private double C;

        public SVMClassifier(List<Tuple<double[], double>> data)
        {
            this.data = data;
            this.kernel = new LinearKernel();
            this.n = 0.05;
            this.C = 1.0;
            classes = new Dictionary<double, List<double[]>>();
            supportVectorMachines = new List<Tuple<double, SVM>>();
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

        public SVMClassifier(List<Tuple<double[], double>> data, Kernel kernel)
        {
            this.data = data;
            this.kernel = kernel;
            this.n = 0.05;
            this.C = 1.0;
            classes = new Dictionary<double, List<double[]>>();
            supportVectorMachines = new List<Tuple<double, SVM>>();
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

        public SVMClassifier(List<Tuple<double[], double[]>> data, Kernel kernel)
        {
            this.dataMultibleOutputvalues = data;
            this.kernel = kernel;
            this.n = 0.05;
            this.C = 1.0;
            classes = new Dictionary<double, List<double[]>>();
            supportVectorMachines = new List<Tuple<double, SVM>>();
            data.ForEach((Tuple<double[], double[]> tuple) =>
            {
                var resultString = String.Empty;
                foreach(var item in tuple.Item2)
                {
                    resultString += item;
                }                
                if(!classesMultibleOutputvalues.ContainsKey(resultString))
                {
                    classesMultibleOutputvalues.Add(resultString, new List<double[]>());
                    classesMultibleOutputvalues[resultString].Add(tuple.Item1);
                }
                else
                {
                    classesMultibleOutputvalues[resultString].Add(tuple.Item1);
                }
            });
        }

        public SVMClassifier(List<Tuple<double[], double>> data, Kernel kernel, double n, double C)
        {
            this.data = data;
            this.kernel = kernel;
            this.n = n;
            this.C = C;
            classes = new Dictionary<double, List<double[]>>();
            supportVectorMachines = new List<Tuple<double, SVM>>();
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
                SVM svm = new SVM();
                svm.N = n;
                svm.C = C;
                svm.Train(preparedItems, kernel);
                supportVectorMachines.Add(new Tuple<double, SVM>(classItem.Key, svm));
            }
        }

        public override double Classify(double[] inputValues)
        {
            var maxValue = double.MinValue;
            var returnClass = 0.0;
            foreach (var svm in supportVectorMachines)
            {
                var value = svm.Item2.ClassifyValue(inputValues);
                if(maxValue < value)
                {
                    maxValue = value;
                    returnClass = svm.Item1;
                }
            }
            return returnClass;
        }
    }
}
