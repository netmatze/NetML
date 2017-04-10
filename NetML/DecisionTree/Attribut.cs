using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.DecisionTree
{
    public class Attribut<T>
    {
        public Attribut(string name, T value)
        {
            Value = value;
            Name = name;
        }

        public string Name
        {
            get;
            set;
        }

        public T Value
        {
            get;
            set;
        }
    }
}
