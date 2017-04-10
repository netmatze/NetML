using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.DecisionTree
{
    public class Classes<T>
    {
        public Classes(string lable)
        {
            this.Lable = lable;
        }

        public string Lable
        {
            get;
            private set;
        }

        public int Value
        {
            get;
            set;
        }

        public List<Attribut<T>> Attributes
        {
            get;
            set;
        }
    }
}
