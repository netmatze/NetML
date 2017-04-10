using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.DecisionTree
{
    public class AttributeTreeNode<T> : TreeNode<T>
    {
        public string Attribute
        {
            get;
            set;
        }

        public int Feature
        {
            get;
            set;
        }

        public double Value
        {
            get;
            set;
        }

        public override string ToString()
        {
            return string.Format(" {0} -> {1} ", Feature, Value);
        }
    }
}
