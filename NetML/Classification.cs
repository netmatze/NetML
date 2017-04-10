using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML
{
    public abstract class Classification
    {
        public Classification()
        {

        }
         
        public abstract void Train();

        public abstract double Classify(double[] inputValues);        
    }
}
