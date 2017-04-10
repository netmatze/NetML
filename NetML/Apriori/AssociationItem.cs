using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetML.Apriori
{
    public class AssociationItem
    {
        private Item fromItem;

        public Item FromItem
        {
            get { return fromItem; }
            set { fromItem = value; }
        }

        private Item toItem;

        public Item ToItem
        {
            get { return toItem; }
            set { toItem = value; }
        }

        private double support;

        public double Support
        {
            get { return support; }
            set { support = value; }
        }

        private double confidence;

        public double Confidence
        {
            get { return confidence; }
            set { confidence = value; }
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            foreach (var fromStr in fromItem.ItemKeys)
                stringBuilder.Append(fromStr);
            stringBuilder.Append("->");
            foreach (var toStr in toItem.ItemKeys)
                stringBuilder.Append(toStr);
            return stringBuilder.ToString();
        }
    }
}
