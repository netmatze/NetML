using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.Mathematics
{
    public class Vector
    {
        public Vector()
        {

        }

        public Vector(int elements)
        {
            this.elements = new double[elements];
        }

        private double[] elements;

        public double[] Elements
        {
            get { return elements; }
            set { elements = value; }
        }
    }
}
