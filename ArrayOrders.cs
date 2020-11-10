using Eridanus.SpaceSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eridanus.SpaceSystems
{
    public class ArrayOrders
    {
        public List<Order> orders;
        public float theta;

        public ArrayOrders()
        {
            orders = new List<Order>();
        }

        public void enqueue(Order obj)
        {
            orders.Add(obj);
        }
        
        public void remove(int index)
        {
            orders.RemoveAt(index);
        }

        public void add(int index, Order obj)
        {
            orders.Insert(index, obj);
        }

        public void clear()
        {
            orders = new List<Order>();
        }

        public void next(Craft craft)
        {
            orders.RemoveAt(0);
            orders[0].start(craft);
            theta = orders[0].theta;
        }

        public void update(Craft craft)
        {
            orders[0].update(craft);
        }

        public float orientation()
        {
            return theta;
        }
    }
}
