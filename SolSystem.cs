using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eridanus.SpaceSystems
{
    public class SolSystem
    {
        public String name;
        private static uint nextID = 0;
        public uint solID;
        public List<Body> bodies;
        public List<uint> crafts;
        public Vector2 loc; //location on galactic map
        public Rectangle box;
        //nebulas?

        public SolSystem(String n, Vector2 pos)
        {
            name = n;
            loc = pos;
            bodies = new List<Body>();
            crafts = new List<uint>();
            solID = nextID;
            nextID++;
            this.initialize();
        }

        public void initialize()
        {
            box = new Rectangle((int)(loc.X - 5000), (int)(loc.Y - 5000), (int)(10000), (int)(10000));
        }
        public void simulateOrbits()
        {
            //simulate orbits, update positions
            for (int i = 0; i < bodies.Count; i++)
            {
                bodies[i].simulateOrbit();
            }
        }

    }
}
