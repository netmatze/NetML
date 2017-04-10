using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetML.DecisionTree;
using System.Collections.Generic;
using System.Diagnostics;
using NetML.kNearestNeighbors;
using NetML.SupportVectorMachine;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using NetML.NaiveBayes;
using NetML.RandomForest;
using System.Globalization;
using NetML.Mathematics;

namespace NetMLTests
{
    [TestClass]
    public class NetMLTests
    {
        [TestMethod]
        public void SoundTest()
        {
            DataSetLoader dataSetLoader = new DataSetLoader();
            Console.WriteLine(" Reading DataSet.. ");
            var sound = dataSetLoader.SelectSounds(@"\Sounds\Unbenannt.wma");
        }
    }
}
