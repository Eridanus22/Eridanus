using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eridanus.SpaceSystems
{
    public class Craft
    {
        private static uint nextId = 0; //queue for destroyed/removed ships ids to be reused
        public Vector2 loc; //coordinates of body within system
        public uint id;
        public string name;     //name of ship
        public uint hullNum;    //number of class built
        public float speed; // current velocity
        public float orientation;   //radians
        public ShipClass type;
        public uint empireID;
        public int curSystem;
        //cargo onboard
        //fuel
        //crew, command
        //orders, task group, element, fleet
        public ArrayOrders orders;  //queue, queuelist?
        //health/armor/shields

        //default constructor
        public Craft() {
            type = new ShipClass();
        }

        public Craft(Vector2 l, int sys, float s)
        {
            this.initialize();
            type = new ShipClass();
            type.constructShip();
            loc = l;
            curSystem = sys;
            speed = s;
        }

        public void initialize()
        {
            this.id = nextId;
            nextId++;
        }

        public void update()
        {
            if (orders != null)
            {
                orders.update(this);
            }
            
        }

        public void hyperTravel() { }

        //combat destruction or scrapping?
        public void destroy()
        {
            //explosion animation on position relative to scale
            Galaxy.crafts.Remove(this);
        }

    }
}
