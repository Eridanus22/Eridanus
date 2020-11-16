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
        //public int curSystem;

        //default constructor
        public BaseObj() { }

        public void initialize()
        {
            id = nextId;
            nextId++;
        }

        public virtual Rectangle hitbox() { return Rectangle.Empty; }
    }
}
