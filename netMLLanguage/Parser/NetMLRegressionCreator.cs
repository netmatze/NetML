using NetML;
using NetML.LogisticRegression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace netMLLanguage.Parser
{
    public class NetMLRegressionCreator
    {
        private Regression regression;
        private NetMLObject netMLObject;

        public NetMLRegressionCreator(NetMLObject netMLObject)
        {
            this.netMLObject = netMLObject;
        }

        public void Create(List<Tuple<double[], double>> data)
        {
            switch (netMLObject.Algorithmus)
            {
                case "linearregression":
                    Regression(data);
                    break;
            }
        }

        private void Regression(List<Tuple<double[], double>> data)
        {
            regression = new Regression(data, new LinearCostFunction());            
        }

        public void Train()
        {
            regression.Train();
        }

        public double Classify(double[] inputValues)
        {
            return regression.Classify(inputValues);
        }
    }
}
