using NetML.NeuronalNetworks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace NetMLTests
{
    public class Classifier
    {
        public void Classify(NetML.Classification classification, List<Tuple<double[], double>> testdata)
        {
            classification.Train();
            var trueCounter = 0;
            var counter = 0;
            var classificationName = classification.GetType().Name;
            if(classification.GetType() == typeof(NeuronalNetworkClassifier))
            {
                foreach (var item in testdata)
                {
                    var neuronalNetoutputValue = ((NeuronalNetworkClassifier)classification).ClassifiyMultibleResultValue(item.Item1);
                    var resultString = String.Empty;
                    double maxValue = 0.0;
                    int innerCounter = 1;
                    int maxItem = 0;
                    foreach (var value in neuronalNetoutputValue)
                    {
                        if(value > maxValue)
                        {
                            maxValue = value;
                            maxItem = innerCounter;
                        }
                        innerCounter++;
                    }
                    if (maxItem == item.Item2)
                        trueCounter++;
                    Debug.WriteLine(string.Format("Value {0} - Predicted {1} = {2}",
                        item.Item2, maxItem, (maxItem == item.Item2) ? "true" : "false"));
                    counter++;
                }      
            }
            else
            {
                foreach (var item in testdata)
                {
                    var outputValue = classification.Classify(item.Item1);
                    if (outputValue == item.Item2)
                        trueCounter++;
                    Debug.WriteLine(string.Format("{0} Value {1} - Predicted {2} = {3}",
                        classificationName, item.Item2, outputValue, (outputValue == item.Item2) ? "true" : "false"));
                    counter++;
                }
            }
            Debug.WriteLine(string.Format("{0} Data {1} - True {2} Verhältnis: {3}",
               classificationName, counter.ToString(), trueCounter.ToString(), 
               (Convert.ToDouble(trueCounter) / Convert.ToDouble(counter)).ToString()));
        }

        public void Classify(List<NetML.Classification> classifications, List<Tuple<double[], double>> testdata)
        {
            foreach (var classification in classifications)
            {
                classification.Train();
                var trueCounter = 0;
                var counter = 0;
                var classificationName = classification.GetType().Name;
                if (classification.GetType() == typeof(NeuronalNetworkClassifier))
                {
                    foreach (var item in testdata)
                    {
                        var neuronalNetoutputValue = ((NeuronalNetworkClassifier)classification).
                            ClassifiyMultibleResultValue(item.Item1);
                        var resultString = String.Empty;
                        double maxValue = 0.0;
                        int innerCounter = 0;
                        int maxItem = 0;
                        foreach (var value in neuronalNetoutputValue)
                        {
                            if (value > maxValue)
                            {
                                maxValue = value;
                                maxItem = innerCounter;
                            }
                            innerCounter++;
                        }
                        if (maxItem == item.Item2)
                            trueCounter++;
                        counter++;
                    }
                }
                else
                {
                    foreach (var item in testdata)
                    {
                        var outputValue = classification.Classify(item.Item1);
                        if (outputValue == item.Item2)
                            trueCounter++;
                        counter++;
                    }
                }
                Debug.WriteLine(string.Format("{0} Data {1} - True {2} Verhältnis: {3}",
                   classificationName, counter.ToString(), trueCounter.ToString(),
               (Convert.ToDouble(trueCounter) / Convert.ToDouble(counter)).ToString()));
            }
        }
    }
}
