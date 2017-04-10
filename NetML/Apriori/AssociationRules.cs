using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetML.Apriori
{
    public class AssociationRules
    {
        private double minConfidence = 0.3;

        public double MinConfidence
        {
            get { return minConfidence; }
            set { minConfidence = value; }
        }

        private bool ContainsKey(Item list, Item searchItem)
        {
            foreach(var key in list.ItemKeys)
            {
                foreach(var innerKey in searchItem.ItemKeys)
                {
                    if(key == innerKey)
                    {
                        return true;
                    }
                }
            }              
            return false;
        }

        public List<AssociationItem> CalcuateAssoctionRules(List<Item> fromList, List<Item> toList, List<List<string>> transactions)
        {
            var transactionsCount = transactions.Count;
            List<AssociationItem> assocationItems = new List<AssociationItem>();
            foreach(var fromItem in fromList)
            {                
                foreach(var toItem in toList)
                {
                    if (!ContainsKey(fromItem,toItem))
                    {
                        Item newItem = new Item();
                        foreach (var fromItemPart in fromItem.ItemKeys)
                        {
                            newItem.ItemKeys.Add(fromItemPart);
                        }
                        foreach (var toItemPart in toItem.ItemKeys)
                        {
                            if (!newItem.ItemKeys.Contains(toItemPart))
                                newItem.ItemKeys.Add(toItemPart);
                        }
                        CountItem(newItem, transactions);
                        double support = Convert.ToDouble(newItem.Count) / Convert.ToDouble(transactionsCount);
                        double supportFrom = Convert.ToDouble(fromItem.Count) / Convert.ToDouble(transactionsCount);
                        double confidence = support / supportFrom;
                        if (confidence >= minConfidence)
                        {
                            assocationItems.Add(new AssociationItem() { ToItem = toItem, FromItem = fromItem, Confidence = confidence, Support = support });
                        }
                    }
                }
            }
            return assocationItems;
        }

        public void CountItem(Item item, List<List<string>> transactions)
        {
            List<string> readyItems = new List<string>();
            foreach (var transaction in transactions)
            {
                var found = true;
                var itemFound = false;
                foreach (var key in item.ItemKeys)
                {
                    foreach (var transacitonItem in transaction)
                    {
                        if (key == transacitonItem)
                        {
                            itemFound = true;
                            break;
                        }
                    }
                    if (!itemFound)
                    {
                        found = false;
                        break;
                    }
                    itemFound = false;
                }
                if (found)
                    item.Count++;
            }            
        }

        public void CountItems(List<Item> c, List<List<string>> transactions)
        {
            List<string> readyItems = new List<string>();
            foreach (var item in c)
            {
                foreach (var transaction in transactions)
                {
                    var found = true;
                    var itemFound = false;
                    foreach (var key in item.ItemKeys)
                    {
                        foreach (var transacitonItem in transaction)
                        {
                            if (key == transacitonItem)
                            {
                                itemFound = true;
                                break;
                            }
                        }
                        if (!itemFound)
                        {
                            found = false;
                            break;
                        }
                        itemFound = false;
                    }
                    if (found)
                        item.Count++;
                }
            }
        }
    }
}
