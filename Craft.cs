﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eridanus.SpaceSystems
{
    public class Craft : BaseObj
    {
        public string name;     //name of ship
        public uint hullNum;    //number of class built
        public float speed; // current velocity
        public float orientation;   //radians
        public ShipClass type;
        public uint empireID;
        public int curSystem;
        public int index;
        //cargo onboard
        //fuel
        //crew, command
        //orders, task group, element, fleet
        public ArrayOrders orders;  //queue, queuelist?
        //health/armor/shields
        public int[] armor; //each armor tile stores an int of armor ex armor[5] = 17

        //default constructor
        public Craft() {
            type = new ShipClass();
        }

        public Craft(Vector2 l, int sys, float s)
        {
            this.initialize();
            type = new ShipClass();
            //armor = type.armor;
            type.constructShip();
            loc = l;
            curSystem = sys;
            speed = s;
        }

        public void initialize()
        {
            base.initialize();
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
            //remove from curSystem
            //Galaxy.crafts[this id] = empty;
            //Galaxy.emptyCrafts.Enqueue(this id);
        }

        public Rectangle getBox()
        {

            return new Rectangle((int)(loc.X - 100), (int)(loc.Y- 100), (int)200, (int)200);
        }
        
        public void setVelocity(float v)
        {
            //calculates engine power needed to maintain velocity based on mass
        }

    }
}
