
using NetML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace netMLLanguage.Parser
{
    public class NetMLCreator
    {
        private NetMLObject netMLObject;        
        private NetMLClassificatonCreator netMLClassificatonCreator;
        private NetMLClusteringCreator netMLClusteringCreator;
        private NetMLRegressionCreator netMLRegressionCreator;

        public NetMLCreator(string netMLCreationString)
        {
            NetMLParser netMLParser = new NetMLParser();
            var result = netMLParser.Parse(netMLCreationString);
            this.netMLObject = result;
            if (netMLObject.AlgorithmusClassification == "classification")
            {
                netMLClassificatonCreator = new NetMLClassificatonCreator(netMLObject);
            }
            else if (netMLObject.AlgorithmusClassification == "clustering")
            {
                netMLClusteringCreator = new NetMLClusteringCreator(netMLObject);
            }
            else if (netMLObject.AlgorithmusClassification == "regression")
            {
                netMLRegressionCreator = new NetMLRegressionCreator(netMLObject);
            }
        }

        public NetMLCreator(NetMLObject netMLObject)
        {
            this.netMLObject = netMLObject;
            if (netMLObject.AlgorithmusClassification == "classification")
            {
                netMLClassificatonCreator = new NetMLClassificatonCreator(netMLObject);
            }
            else if (netMLObject.AlgorithmusClassification == "clustering")
            {
                netMLClusteringCreator = new NetMLClusteringCreator(netMLObject);
            }
            else if (netMLObject.AlgorithmusClassification == "regression")
            {
                netMLRegressionCreator = new NetMLRegressionCreator(netMLObject);
            }
        }

        public void Create(List<Tuple<double[], double>> data)
        {
            if(netMLClassificatonCreator != null)
            {
                netMLClassificatonCreator.Create(data);
            }
            else if (netMLRegressionCreator != null)
            {
                netMLRegressionCreator.Create(data);
            }            
        }

        public void Create(List<double[]> data)
        {
            if (netMLClusteringCreator != null)
            {
                netMLClusteringCreator.Create(data);
            }            
        }

        public void Train()
        {
            netMLClassificatonCreator.Train();
        }

        public double Classify(double[] inputValues)
        {
            return netMLClassificatonCreator.Classify(inputValues);
        }

        public double Regression(double[] inputValues)
        {
            return netMLRegressionCreator.Classify(inputValues);
        }

        public int CalculateClusterAffinity(double[] clusteringData)
        {
            return netMLClusteringCreator.CalculateClusterAffinity(clusteringData);
        }

        public List<Tuple<int, double[]>>[] Cluster()
        {
            return netMLClusteringCreator.Cluster();
        }

        public List<Tuple<int, double[]>>[] Cluster(int clusters)
        {
            return netMLClusteringCreator.Cluster(clusters);
        }
    }
}
