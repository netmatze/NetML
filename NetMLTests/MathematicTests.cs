using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetML.Mathematics;

namespace NetMLTests
{
    [TestClass]
    public class MathematicTests
    {
        [TestMethod]        
        public void VectorCrossProductTest()
        {
            Vector vector1 = new Vector() { Elements = new double[] { 1, 2, 3 } };
            Vector vector2 = new Vector() { Elements = new double[] { -7, 8, 9 } };
            var resultVector = vector1.CrossProduct(vector2);
        }
    }
}
