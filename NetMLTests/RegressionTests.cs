using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetML.DecisionTree;
using System.Collections.Generic;
using System.Diagnostics;
using NetML.SupportVectorMachine;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using NetML.NaiveBayes;
using System.Globalization;
using NetML.LogisticRegression;


namespace NetMLTests
{
    [TestClass]
    public class RegressionTests
    {
        [TestMethod]
        public void CreditDataRegressionTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var creditData = dataSetLoader.SelectCreditData();
            Regression loggistigRegression =
                new Regression(creditData, new NetML.LogisticRegression.LogisticCostFunction());
            loggistigRegression.Train();
            var creditDataTest = dataSetLoader.SelectCreditData();
            var trueCounter = 0;
            var counter = 0;
            foreach (var item in creditDataTest)
            {
                var outputValue = loggistigRegression.Classify(item.Item1);
                if (outputValue == item.Item2)
                    trueCounter++;
                Debug.WriteLine(string.Format("Value {0} - Predicted {1} = {2}",
                    item.Item2, outputValue, (outputValue == item.Item2) ? "true" : "false"));
                counter++;
            }
            Debug.WriteLine(string.Format("Data {0} - True {1} Verhältnis: {2}",
                counter.ToString(), trueCounter.ToString(), (Convert.ToDouble(trueCounter) / Convert.ToDouble(counter)).ToString()));
        }
    }
}
