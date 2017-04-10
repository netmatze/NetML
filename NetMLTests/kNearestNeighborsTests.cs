using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetML.DecisionTree;
using System.Collections.Generic;
using System.Diagnostics;
using NetML.kNearestNeighbors;

namespace NetMLTests
{
    [TestClass]
    public class kNearestNeighborsTests
    {
        [TestMethod]
        public void KNearestNeighborsTest()
        {
            Tuple<double[], double> tuple1 = new Tuple<double[], double>(new double[] { 3, 104, 2 }, 0);
            Tuple<double[], double> tuple2 = new Tuple<double[], double>(new double[] { 2, 100, 1 }, 0);
            Tuple<double[], double> tuple3 = new Tuple<double[], double>(new double[] { 1, 81, 0 }, 0);
            Tuple<double[], double> tuple4 = new Tuple<double[], double>(new double[] { 101, 10, 0 }, 1);
            Tuple<double[], double> tuple5 = new Tuple<double[], double>(new double[] { 99, 5, 0 }, 1);
            Tuple<double[], double> tuple6 = new Tuple<double[], double>(new double[] { 98, 2, 0 }, 1);
            Tuple<double[], double> tuple7 = new Tuple<double[], double>(new double[] { 3, 2, 20 }, 2);
            Tuple<double[], double> tuple8 = new Tuple<double[], double>(new double[] { 4, 2, 21 }, 2);
            Tuple<double[], double> tuple9 = new Tuple<double[], double>(new double[] { 2, 2, 23 }, 2);
            List<Tuple<double[], double>> movies =
                new List<Tuple<double[], double>>() { tuple1, tuple2, tuple3, tuple4, tuple5, tuple6, tuple7, tuple8, tuple9 };
            KNearestNeighborsClassifier kNearestNeighborsClassifier =
                new KNearestNeighborsClassifier(movies, 3, new EuclideMetric());
            var result = kNearestNeighborsClassifier.Classify(new double[] { 3, 104, 2 });
            result = kNearestNeighborsClassifier.Classify(new double[] { 101, 10, 0 });
            result = kNearestNeighborsClassifier.Classify(new double[] { 80, 10, 0 });
            result = kNearestNeighborsClassifier.Classify(new double[] { 5, 90, 1 });
            result = kNearestNeighborsClassifier.Classify(new double[] { 4, 2, 21 });
            result = kNearestNeighborsClassifier.Classify(new double[] { 3, 3, 25 });
        }
    }
}
