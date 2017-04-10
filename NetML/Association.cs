using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetML.Apriori;

namespace NetML
{
    public abstract class Association<T>
    {
        public abstract List<AssociationItem> BestItems(List<T> list);
        public abstract AssociationItem BestItem(List<T> list); 
    }
}
