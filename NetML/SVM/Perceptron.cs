using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.SupportVectorMachine
{
    public class Perceptron
    {
        public double[] w;

        private double N = 0.5;

        public double Classify(double[] inputValues)
        {
            var innerProduct = InnerProduct(w, inputValues);
            if (innerProduct >= 0)
                return 1.0;
            return -1.0;
        }

        public void Train(List<Tuple<double[], double>> inputValues)
        {
            w = new double[inputValues[0].Item1.Length];
            var converged = false;
            while(!converged)
            {
                converged = true;
                for (int i = 0; i < inputValues[0].Item1.Length; i++)
                {
                    var yi = inputValues[i].Item2;
                    var xi = inputValues[i].Item1;
                    var innerProduct = InnerProduct(w, xi);
                    if (innerProduct * yi <= 0)
                    {
                        var vectorResult = Scalarmultiblication(N * yi, xi);
                        w = VectorAdd(w, vectorResult);
                        converged = false;
                    }
                }
            }
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

        private double[] Scalarmultiblication(double scalar, double[] vector)
        {
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] = scalar * vector[i];
            }
            return vector;
        }

        private double[] VectorAdd(double[] vectorOne, double[] vectorTwo)
        {
            var resultVector = new double[vectorOne.Length];
            for (int i = 0; i < vectorOne.Length; i++)
            {
                resultVector[i] = vectorOne[i] + vectorTwo[i];
            }
            return resultVector;
        }
    }
}
