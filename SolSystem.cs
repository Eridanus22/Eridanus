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
        public List<int> crafts;
        public Vector2 loc; //location on galactic map

        public SolSystem() {
            bodies = new List<Body>();
            crafts = new List<int>();
            solID = nextID;
            nextID++;
        }

        public void simulateOrbits()
        {
            //simulate orbits, update positions
            for (int i = 1; i < bodies.Count; i++)
            {
                bodies[i].simulateOrbit();
            }
        }

        public void createCraft(Craft c, Vector2 l)
        {

        }
    }
}
