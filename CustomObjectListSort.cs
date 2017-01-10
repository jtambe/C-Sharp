using System;
using System.Collections.Generic;



namespace project
{
    public class Order : IComparable, IComparable<Order>
    {
        public DateTime OrderDate { get; set; }
        public int OrderId { get; set; }

        // one way to do comparison
        public int CompareTo(object obj)
        {
            Order orderToCompare = obj as Order;
            if (orderToCompare.OrderDate < OrderDate )
            {
                return 1;
            }
            if (orderToCompare.OrderDate > OrderDate )
            {
                return -1;
            }

            // The orders are equivalent.
            return 0;
        }

        // second way to do comparison
        public int CompareTo(Order obj)
        {
            Order orderToCompare = obj as Order;
            if (orderToCompare.OrderDate < OrderDate )
            {
                return 1;
            }
            if (orderToCompare.OrderDate > OrderDate )
            {
                return -1;
            }

            // The orders are equivalent.
            return 0;
        }
    }

    class CompareByDate : IComparer<object>
    {

        public int Compare(object x, object y)
        {
            Order xObj = x as Order;
            Order yObj = y as Order;
            if (xObj.OrderDate < yObj.OrderDate )
            {
                return 1;
            }
            if (xObj.OrderDate > yObj.OrderDate )
            {
                return -1;
            }
            else
            {
                // The orders are equivalent.
                return 0;                
            }
        }

    }



    public class EntryClass
    {
        public static void Main()
        {
            List<Order> orderListObj = new List<Order>();
            for(int i = 1; i < 10; i++)
            {
                Order obj = new Order();
                obj.OrderDate = DateTime.Now.AddDays(-i);
                obj.OrderId = i;
                orderListObj.Add(obj);
            }


            // IComparer needs another class and then use it within sort method
            CompareByDate comparerClassObj = new CompareByDate();
            orderListObj.Sort(comparerClassObj);


            // default sort uses IComparable
            orderListObj.Sort();

            //simply use lamba expressions
            orderListObj.OrderByDescending(x => x.OrderDate);


            foreach(Order x in orderListObj)
            {
                Console.WriteLine(" Date is : {0} for OrderId: {1}", x.OrderDate, x.OrderId);
            }

            Console.ReadLine();
        }
    }
   


}

/*
As the name suggests, IComparable<T> reads out I'm comparable. 
IComparable<T> when defined for T lets you compare the current instance with another instance of same type. 

IComparer<T> reads out I'm a comparer, I compare. 
IComparer<T> is used to compare any two instances of T, typically outside the scope of the instances of T.

As to what they are for can be confusing at first. 
From the definition it should be clear that, 
IComparable<T> (defined in the class T itself) should be the de facto standard to provide the logic for sorting. 
The default Sort on List<T> etc relies on this. Implementing IComparer<T> on T doesn't help regular sorting. 

Subsequently, there is little value for implementing IComparable<T> on any other class other than T

IComparer<T> can be useful when you require sorting based on a custom order, but not as a general rule. 
For instance, in a class of Person at some point you might require to Sort people based on their age.
*/