using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetML.Apriori
{
    //http://www.youtube.com/watch?v=0lCvvF0Wdio
    //https://dev.twitter.com/docs/twitter-libraries
    public class Apriori
    {
        private int minmumSupport = 2;

        public int MinmumSupport
        {
            get { return minmumSupport; }
            set { minmumSupport = value; }
        }

        private List<List<Item>> cresultLists = new List<List<Item>>();

        public List<List<Item>> CResultLists
        {
            get { return cresultLists; }
            set { cresultLists = value; }
        }

        private List<List<Item>> pruneresultLists = new List<List<Item>>();

        public List<List<Item>> PruneResultLists
        {
            get { return pruneresultLists; }
            set { pruneresultLists = value; }
        }

        private List<List<Item>> lresultLists = new List<List<Item>>();

        public List<List<Item>> LResultLists
        {
            get { return lresultLists; }
            set { lresultLists = value; }
        }
       
        private bool ContainsKey(List<Item> list, Item searchItem)
        {
            foreach (Item item in list)
            {
                if (item.Equals(searchItem))
                {
                    return true;
                }
            }
            return false;
        }

        private Item FindKey(List<Item> list, Item searchItem)
        {
            foreach (Item item in list)
            {
                if (item.Equals(searchItem))
                {
                    return item;
                }
            }
            return null;
        }

        public List<Item> CalcuateApriori(List<List<string>> transactions)
        {
            List<Item> c1 = BuildStartItems(transactions);
            cresultLists.Add(c1);
            List<Item> l1 = BuildLSet(c1);
            lresultLists.Add(c1);
            return CalculateApriorieIntern(transactions, c1);
        }

        private List<Item> CalculateApriorieIntern(List<List<string>> transactions, List<Item> c)
        {
            List<Item> cResult = CombineItems(c);
            cresultLists.Add(cResult);
            List<Item> prunedResult = PruneItems(cResult, c);
            CountItems(prunedResult, transactions);            
            pruneresultLists.Add(prunedResult);
            List<Item> lResult = BuildLSet(prunedResult);
            lresultLists.Add(lResult);
            if (lResult.Count > 0)
            {
                return CalculateApriorieIntern(transactions, lResult);
            }
            return lResult;
        }
      
        public List<Item> CombineItems(List<Item> items)
        {
            List<Item> list = new List<Item>();
            List<Item> visitedItems = new List<Item>();
            List<Item> c2 = new List<Item>();
            HashSet<string> hashSet = new HashSet<string>();
            var length = 0;
            var newLength = 0;
            foreach (Item item in items)
            {
                foreach (string innerKey in item.ItemKeys)
                {
                    if (!hashSet.Contains(innerKey))
                    {
                        hashSet.Add(innerKey);
                    }
                }
                length = item.ItemKeys.Count;
                newLength = length + 1;
            }
            if (newLength == 2)
            {
                return CombineTwoItems(items);
            }
            else
            {
                foreach (var key in hashSet)
                {
                    var itemCount = 0;
                    Item valueItem = new Item();
                    foreach (Item item in items)
                    {
                        itemCount = item.ItemKeys.Count;
                        if (!visitedItems.Contains(item))
                        {
                            if (item.ItemKeys.Contains(key))
                            {
                                visitedItems.Add(item);
                                foreach (string innerKey in item.ItemKeys)
                                {
                                    if (!valueItem.ItemKeys.Contains(innerKey))
                                        valueItem.ItemKeys.Add(innerKey);
                                }
                            }
                        }
                    }
                    if (valueItem.ItemKeys.Count >= newLength)
                        c2.Add(valueItem);
                }
                List<Item> listItems = new List<Item>();
                foreach (var item in c2)
                {
                    if (item.ItemKeys.Count == newLength)
                    {
                        if (!ContainsKey(listItems, item))
                            listItems.Add(item);
                    }
                    else
                    {
                        AddItems(listItems, item, newLength);
                    }
                }
                return listItems;
            }
        }

        public void AddItems(List<Item> listItems, Item item, int newLenght)
        {                                   
            var mStartValue = 0;
            var mEndValue = mStartValue + 1;      
            while (true)
            {
                List<string> fixValues = new List<string>();
                for (int m = mStartValue; m < mEndValue; m++)
                {
                    var key = item.ItemKeys.ToArray()[m];
                    fixValues.Add(key);                        
                }
                List<string> notFixedValues = new List<string>();
                foreach (var notFixedValue in item.ItemKeys)
                {
                    if (!fixValues.Contains(notFixedValue))
                        notFixedValues.Add(notFixedValue);
                }
                var counter = 0;
                List<Item> notFixedValuesCombinations = CombineFixedItems(notFixedValues, newLenght - 1);
                foreach (var notFixedValue in notFixedValuesCombinations)
                {
                    Item newItem = new Item();
                    foreach (var fixItem in fixValues)
                    {
                        newItem.ItemKeys.Add(fixItem);
                        counter++;
                    }
                    foreach (var notFixItem in notFixedValue.ItemKeys)
                        newItem.ItemKeys.Add(notFixItem);
                    if (!ContainsKey(listItems, newItem))
                        listItems.Add(newItem);
                }
                mStartValue++;
                mEndValue++;
                if (mEndValue == item.ItemKeys.Count + 1)
                    break;
            }
        }

        private List<Item> CombineFixedItems(List<string> notFixedValues, int newLength)
        {
            List<Item> combineFixedItems = new List<Item>();
            for (int m = 0; m < notFixedValues.Count; m++)
            {
                var startValue = notFixedValues[m];
                for (int i = 0; i < notFixedValues.Count; i++)
                {
                    Item newItem = new Item();
                    newItem.ItemKeys.Add(startValue);
                    for (int j = i + 1; j < newLength + i; j++)
                    {
                        var value = j;
                        if (value >= notFixedValues.Count)
                        {
                            value = j % notFixedValues.Count;
                        }
                        if (notFixedValues.ToArray()[value] != startValue)
                            newItem.ItemKeys.Add(notFixedValues.ToArray()[value]);
                    }
                    if (!ContainsKey(combineFixedItems, newItem))
                        if(newItem.ItemKeys.Count == newLength)
                            combineFixedItems.Add(newItem);
                }
            }
            return combineFixedItems;
        }

        public List<Item> CombineTwoItems(List<Item> items)
        {
            List<Item> list = new List<Item>();
            foreach (Item item in items)
            {
                var createNewItem = true;
                Item newItem = null;
                foreach (Item c1Item in items)
                {
                    if (createNewItem)
                    {
                        newItem = new Item();
                        foreach (string key in item.ItemKeys)
                        {
                            if (!newItem.ItemKeys.Contains(key))
                            {
                                newItem.ItemKeys.Add(key);
                            }
                        }
                    }
                    foreach (string key in c1Item.ItemKeys)
                    {
                        if (!newItem.ItemKeys.Contains(key))
                        {
                            newItem.ItemKeys.Add(key);
                            if (!ContainsKey(list, newItem))
                                list.Add(newItem);
                            createNewItem = true;
                        }
                        else
                        {
                            createNewItem = false;
                        }
                    }
                }
            }
            return list;
        }

        public void CountItems(List<Item> c, List<List<string>> transactions)
        {
            List<string> readyItems = new List<string>();
            foreach(var item in c)
            {                
                foreach (var transaction in transactions)
                {                    
                    var found = true;
                    var itemFound = false;
                    foreach(var key in item.ItemKeys)
                    {
                        foreach (var transacitonItem in transaction)
                        {
                            if(key == transacitonItem)
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

        public List<Item> BuildStartItems(List<List<string>> transactions)
        {
            List<Item> c1 = new List<Item>();
            foreach (var transaction in transactions)
            {
                foreach (var transacitonItem in transaction)
                {
                    Item item = new Item();
                    item.ItemKeys.Add(transacitonItem);
                    item.Count = 1;
                    if (!ContainsKey(c1, item))
                    {
                        c1.Add(item);
                    }
                    else
                    {
                        Item keyItem = FindKey(c1, item);
                        if(keyItem != null)
                        {
                            keyItem.Count++;
                        }
                    }
                }
            }
            return c1;
        }

        public List<Item> BuildLSet(List<Item> c)
        {
            List<Item> l = new List<Item>();
            foreach (var item in c)
            {
                if (item.Count >= minmumSupport)
                {
                    l.Add(item);
                }
            }
            return l;
        }

        private List<Item> PruneItem(Item item, List<Item> l, int newLength)
        {             
            List<string> values = new List<string>();
            foreach (var key in item.ItemKeys)
            {
                values.Add(key);
            }
            List<Item> combineFixedItems = new List<Item>();
            for (int m = 0; m < values.Count; m++)
            {
                var startValue = values[m];
                for (int i = 0; i < values.Count; i++)
                {
                    Item newItem = new Item();
                    newItem.ItemKeys.Add(startValue);
                    for (int j = i + 1; j < newLength + i; j++)
                    {
                        var value = j;
                        if (value >= values.Count)
                        {
                            value = j % values.Count;
                        }
                        if (values.ToArray()[value] != startValue)
                            newItem.ItemKeys.Add(values.ToArray()[value]);
                    }
                    if (!ContainsKey(combineFixedItems, newItem))
                        if (newItem.ItemKeys.Count == newLength)
                            combineFixedItems.Add(newItem);
                }
            }
            return combineFixedItems;
        }

        public List<Item> PruneItems(List<Item> c, List<Item> l)
        {
            List<Item> prunedItems = new List<Item>();
            foreach(var item in c)
            {                
                var lenght = item.ItemKeys.Count;
                var newLength = lenght - 1;
                if (lenght >= 2)
                {
                    List<Item> pruneItems = PruneItem(item, l, newLength);
                    foreach(var innerItem in pruneItems)
                    {
                        if (ContainsKey(l, innerItem))
                        {
                            if (!prunedItems.Contains(item))
                                prunedItems.Add(item);
                        }
                    }
                }
                else
                {
                    return c;
                }
            }
            return prunedItems;
        }
    }
}
