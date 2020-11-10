using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eridanus
{
    public class BaseObj
    {
        private static uint nextId=0;
        public Vector2 loc; //coordinates of body within system
        public uint id;
        
        //default constructor
        public BaseObj() { }

        public void initialize()
        {
            id = nextId;
            nextId++;
        }

        //return position
        public Vector2 getPos()
        {
            return loc;
        }

        public void updatePos(Vector2 p)
        {
            loc = p;
        }
    }
}
