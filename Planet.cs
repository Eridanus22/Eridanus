using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Eridanus.SpaceSystems
{
    public class Planet : Body
    {
        public float yearLength; //in earth days 
        public float dayLength; //in hours (0=tidalLock || day==year)
        //rings?

        public Planet() { }

        public override void drawOrbit(SpriteBatch s, float z) { Primitives2D.DrawCircle(s, getOrbitCenter(), orbitDist, 360, Color.Cyan, 1f / z); }

        public override void simulateOrbit()
        {
            //hypotenuse/radius = orbitDist
            theta = (theta + radians) % (2 * Math.PI);
            loc.X = (orbitDist * (float)Math.Cos(theta));
            loc.Y = (orbitDist * (float)Math.Sin(theta));
            base.simulateOrbit();
        }

        public void initialize()
        {
            this.calcRadians();
            base.initialize();
        }

        public override void calcRadians()
        {
            //yrLength is in Earth days
            radians = (float)((2 * Math.PI) / (yearLength * 24));
        }

        public override void drawBody(SpriteBatch s, float z)
        {
            base.drawBody(s, z);
        }
    }
}
