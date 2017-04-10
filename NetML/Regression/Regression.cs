using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.LogisticRegression
{
    public class Regression : Classification
    {
        private List<Tuple<double[], double>> data = 
            new List<Tuple<double[], double>>();
        private IRegressionFunction costFunction;
        private double[] sigmas;
        private int countInput;
        private int max = 1000;
        private double alpha = 0.01;

        public Regression(List<Tuple<double[], double>> data, IRegressionFunction costFunction)
        {
            this.countInput = data[0].Item1.Length;
            this.data = data;
            this.costFunction = costFunction;
            sigmas = new double[countInput + 1];
        }

        public override void Train()
        {
            var counter = 0;
            while(counter < max)
            {
                foreach(var example in data)
                {
                    var tempSigmas = new double[countInput + 1];
                    for(int j = 0; j < countInput + 1; j++)
                    {
                        var xj = 0.0;
                        if(j == 0)
                            xj = 1.0;
                        else
                            xj = example.Item1[j - 1];
                        tempSigmas[j] = sigmas[j] + alpha *
                            (example.Item2 - costFunction.Calculate(sigmas, example.Item1)) * xj;
                    }   
                    for(int m = 0; m < tempSigmas.Length; m++)
                    {
                        sigmas[m] = tempSigmas[m];
                    }
                }
                counter++;
            }
        }

        public override double Classify(double[] inputValues)
        {
            return costFunction.Calculate(sigmas, inputValues);
        }
    }
}
