using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NetML.SupportVectorMachine
{
    public class DualPerceptron
    {
        public double[] alphai;
        public double[] w;
        public double b;
        private Kernel kernel;
        private List<Tuple<double[], double>> inputValues;

        public double N
        {
            get;
            set;
        }

        //private double N = 0.5;

        public double Classify(double[] classificationValues)
        {
            var summe = 0.0;
            for (int j = 0; j < classificationValues.Length; j++)
            {
                var x = classificationValues;
                for (int i = 0; i < this.inputValues.Count; i++)
                {
                    var alphaI = alphai[i];
                    var yI = inputValues[i].Item2;
                    var xI = inputValues[i].Item1;
                    double kernelProduct = kernel.Calculate(xI, x);
                    summe += kernelProduct * alphaI * yI; // kernel.Calculate(xI, xJ)
                }
            }
            var innerProduct = summe + b;
            if (innerProduct >= 0)
                return 1.0;
            return -1.0;
        }

        public double ClassifyKernel(double[] inputValues, Kernel kernel)
        {
            var innerProduct = kernel.Calculate(w, inputValues);
            if (innerProduct >= 0.5)
                return 1.0;
            return -1.0;
        }

        public double ClassifyKernelValue(double[] inputValues, Kernel kernel)
        {
            return kernel.Calculate(w, inputValues);            
        }

        public void Train(List<Tuple<double[], double>> inputValues, Kernel kernel)
        {
            this.inputValues = inputValues;
            this.kernel = kernel;
            alphai = new double[inputValues.Count];            
            var converged = false;
            while (!converged)
            {
                converged = true;
                for (int i = 0; i < inputValues.Count; i++)
                {
                    var yi = inputValues[i].Item2;
                    var xi = inputValues[i].Item1;
                    var sum = CalculateKernelSum(yi, xi, inputValues, kernel);
                    if (sum <= 0 && alphai[i] < 1000)
                    {
                        alphai[i]++;
                        converged = false;
                    }
                }
            }
            w = new double[inputValues[0].Item1.Length];
            for (int a = 0; a < inputValues[0].Item1.Length; a++)
            {
                var summe = 0.0;
                for (int i = 0; i < inputValues.Count; i++)
                {
                    var alphaI = alphai[i];
                    var yI = inputValues[i].Item2;
                    var xI = inputValues[i].Item1;
                    var xJ = inputValues[i].Item1[a];
                    double kernelProduct = kernel.Calculate(w, xI);
                    summe += xJ * alphaI * yI; // kernel.Calculate(xI, xJ)
                }
                w[a] = summe;
            }
            double N = inputValues[0].Item1.Length;
            var sumResult = 0.0;
            for (int s = 0; s < inputValues.Count; s++)
            {
                var ys = inputValues[s].Item2;
                var innerSumme = 0.0;
                for (int m = 0; m < inputValues.Count; m++)
                {
                    var alpahm = alphai[m];
                    var ym = inputValues[m].Item2;
                    var xm = inputValues[m].Item1;
                    var xs = inputValues[s].Item1;
                    innerSumme += alpahm * ym * kernel.Calculate(xm, xs); // InnerProduct(xm, xs);
                }
                sumResult += ys - innerSumme;
            }
            b = 1 / N * sumResult;
        }

        public void TrainParallel(List<Tuple<double[], double>> inputValues, Kernel kernel)
        {
            var processorCount = Environment.ProcessorCount;
            this.inputValues = inputValues;
            this.kernel = kernel;
            alphai = new double[inputValues.Count];
            var dataPart = inputValues.Count / processorCount;
            int count = processorCount;
            for (int c = 0; c < processorCount; c++)
            {
                Thread thread = new Thread((object pts) =>
                {
                    var fromToObject = (Tuple<int, int, int>)pts;
                    var converged = false;
                    while (!converged)
                    {
                        converged = true;
                        for (int i = fromToObject.Item2; i < fromToObject.Item3; i++)
                        {
                            var yi = inputValues[i].Item2;
                            var xi = inputValues[i].Item1;
                            var sum = CalculateKernelSum(yi, xi, inputValues, kernel);
                            if (sum <= 0 && alphai[i] < 1000)
                            {
                                alphai[i]++;
                                converged = false;
                            }
                        }
                    }
                    count--;
                });
                var startPartValue = c * dataPart;
                var endPartValue = c * dataPart + dataPart;
                thread.Start(new Tuple<int, int, int>(c, startPartValue, endPartValue));
            }
            while (count > 0) { }
            w = new double[inputValues[0].Item1.Length];
            for (int a = 0; a < inputValues[0].Item1.Length; a++)
            {
                var summe = 0.0;
                for (int i = 0; i < inputValues.Count; i++)
                {
                    var alphaI = alphai[i];
                    var yI = inputValues[i].Item2;
                    var xI = inputValues[i].Item1;
                    var xJ = inputValues[i].Item1[a];
                    double kernelProduct = kernel.Calculate(w, xI);
                    summe += xJ * alphaI * yI;
                }
                w[a] = summe;
            }
            double N = inputValues[0].Item1.Length;
            var sumResult = 0.0;
            for (int s = 0; s < inputValues.Count; s++)
            {
                var ys = inputValues[s].Item2;
                var innerSumme = 0.0;
                for (int m = 0; m < inputValues.Count; m++)
                {
                    var alpahm = alphai[m];
                    var ym = inputValues[m].Item2;
                    var xm = inputValues[m].Item1;
                    var xs = inputValues[s].Item1;
                    innerSumme += alpahm * ym * kernel.Calculate(xm, xs); // InnerProduct(xm, xs);
                }
                sumResult += ys - innerSumme;
            }
            b = 1 / N * sumResult;
        }

        public void Train(List<Tuple<double[], double>> inputValues)
        {
            alphai = new double[inputValues.Count];
            var converged = false;
            while (!converged)
            {
                converged = true;
                for (int i = 0; i < inputValues.Count; i++)
                {
                    var yi = inputValues[i].Item2;
                    var xi = inputValues[i].Item1;
                    var sum = CalculateSum(yi, xi, inputValues);
                    if (sum <= 0)
                    {
                        alphai[i]++;
                        converged = false;
                    }
                }
            }
            w = new double[inputValues[0].Item1.Length];
            for (int a = 0; a < inputValues[0].Item1.Length; a++)
            {
                for (int i = 0; i < inputValues.Count; i++)
                {
                    w[a] += inputValues[i].Item1[a] * alphai[i] * inputValues[i].Item2;
                }
            }
        }

        private double CalculateKernelSum(double yi, double[] xi, List<Tuple<double[], double>> inputValues, Kernel kernel)
        {
            var sum = 0.0;
            for (int j = 0; j < inputValues.Count; j++)
            {
                var alphaj = alphai[j];
                var yj = inputValues[j].Item2;
                var xj = inputValues[j].Item1;
                var scalarProduct = alphaj * yj;
                var innerProduct = kernel.Calculate(xi, xj);
                sum += scalarProduct * innerProduct;
            }
            return sum * yi;
        }

        private double CalculateSum(double yi, double[] xi, List<Tuple<double[], double>> inputValues)
        {
            var sum = 0.0;
            for (int j = 0; j < inputValues.Count; j++)
            {
                var alphaj = alphai[j];
                var yj = inputValues[j].Item2;
                var xj = inputValues[j].Item1;
                var scalarProduct = alphaj * yj;
                var innerProduct = InnerProduct(xi, xj);
                sum += scalarProduct * innerProduct;
            }
            return sum * yi;
        }

        private double InnerProduct(double[] firstVector, double[] secondVector)
        {
            var innerProduct = 0.0;
            for (int i = 0; i < firstVector.Length; i++)
            {
                innerProduct += firstVector[i] * secondVector[i];
            }
            return innerProduct;
        }
    }
}
