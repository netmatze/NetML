using System;
using System.Collections.Generic;
using System.Threading;

namespace NetML.SupportVectorMachine
{
    public class SVM
    {
        // Radial basis function kernel (Gaussian Kernel)
        // K(x, xx) = Math.Exp( -1 * Math.Pow(Norm(x - xx),2) / 2 * Math.Pow(sigma,2));

        private double[] w;
        private double b;
        private double[] alphai;
        private Kernel kernel;
        private List<Tuple<double[], double>> inputValues;

        public double N
        {
            get;
            set;
        }

        public double C
        {
            get;
            set;
        }

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
                    summe += kernelProduct * alphaI * yI;
                }                
            }
            var innerProduct = summe + b;
            //var innerProduct = kernel.Calculate(w, classificationValues) + b;
            if (innerProduct >= 0)
                return 1.0;
            return -1.0;
        }

        public double ClassifyValue(double[] inputValues)
        {
            return kernel.Calculate(w, inputValues);
        }

        public void TrainParallel(List<Tuple<double[], double>> inputValues, Kernel kernel)
        {
            var processorCount = Environment.ProcessorCount;
            this.inputValues = inputValues;
            this.kernel = kernel;
            alphai = new double[inputValues.Count];
            var n = this.N;
            var C = this.C;
            var dataPart = inputValues.Count / processorCount;
            int count = processorCount;
            for (int c = 0; c < processorCount; c++)
            {
                Thread thread = new Thread((object pts) =>
                {
                    var fromToObject = (Tuple<int, int, int>)pts;
                    var alphaOld = 0.0;
                    var alphaAlphaOld = 0.0;
                    var counter = 0;
                    do
                    {
                        for (int i = fromToObject.Item2; i < fromToObject.Item3; i++)
                        {
                            alphaOld = alphai[i];
                            double sum = 0.0;
                            for (int j = 0; j < inputValues.Count; j++)
                            {
                                var yj = inputValues[j].Item2;
                                var xi = inputValues[i].Item1;
                                var alphaj = alphai[j];
                                double[] xj = inputValues[j].Item1;
                                double kernelProduct = kernel.Calculate(xi, xj);
                                var result = yj * alphaj * kernelProduct;
                                sum += result;
                            }
                            var sumSum = alphai[i] + n - n * inputValues[i].Item2 * sum; // + n                    
                            if (sumSum < 0)
                            {
                                sumSum = 0.0;
                            }
                            else if (sumSum > C)
                            {
                                sumSum = C;
                            }
                            //alphai = alphai + n * ( 1 - yi * sum(yi * alphaj * innerProduct(xi, xj)))
                            alphai[i] = sumSum;
                            alphaAlphaOld = alphai[i] - alphaOld;
                        }
                        counter++;
                        if (counter > 5000)
                            break;
                    }
                    while (alphaAlphaOld != 0.0);
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

        public void Train(List<Tuple<double[], double>> inputValues, Kernel kernel)
        {
            this.inputValues = inputValues;
            this.kernel = kernel;            
            alphai = new double[inputValues.Count];            
            var n = this.N;
            var C = this.C;           
            var alphaOld = 0.0;
            var alphaAlphaOld = 0.0;
            var counter = 0;
            do
            {                    
                for (int i = 0; i < inputValues.Count; i++)
                {
                    alphaOld = alphai[i];
                    double sum = 0.0;
                    for (int j = 0; j < inputValues.Count; j++)
                    {
                        var yj = inputValues[j].Item2;
                        var xi = inputValues[i].Item1;
                        var alphaj = alphai[j];
                        double[] xj = inputValues[j].Item1;
                        double kernelProduct = kernel.Calculate(xi, xj);
                        var result = yj * alphaj * kernelProduct;
                        sum += result;
                    }
                    var sumSum = alphai[i] + n - n * inputValues[i].Item2 * sum; // + n                    
                    if (sumSum < 0)
                    {
                        sumSum = 0.0;
                    }
                    else if (sumSum > C)
                    {
                        sumSum = C;
                    }
                    alphai[i] = sumSum;
                    alphaAlphaOld = alphai[i] - alphaOld;
                }
                counter++;
                if (counter > 5000)
                    break;
            }
            while (alphaAlphaOld != 0.0); //|| counter < 1000);            
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
                    innerSumme += alpahm * ym * kernel.Calculate(xm, xs);
                }
                sumResult += ys - innerSumme;
            }
            b = 1 / N * sumResult;
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
 