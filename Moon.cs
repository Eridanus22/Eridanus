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
    class Moon : SurfacePlanet
    {
        public Body parent;

        public Moon(string n, string img, double m, double r, float od, double t, float yrLen, float dayLen, Body b)
        {
            name = n;
            imgfile = img;
            mass = m;
            radius = r;
            orbitDist = od;
            theta = t;
            yearLength = yrLen;
            dayLength = dayLen;
            parent = b;
            this.readSprite();
            loc = new Vector2(parent.loc.X + orbitDist, parent.loc.Y);
            base.initialize();
        }

        public override void simulateOrbit()
        {
            //update after parent
            theta = (theta + radians) % (2 * Math.PI);
            loc.X = (orbitDist * (float)Math.Cos(theta));
            loc.Y = (orbitDist * (float)Math.Sin(theta));
            loc.X += parent.loc.X;
            loc.Y += parent.loc.Y;
            this.getBox();
        }

        public override Vector2 getOrbitCenter()
        {
            return parent.getOrbitCenter();
        }

        public override Rectangle getOrbitBox() { return new Rectangle((int)(parent.loc.X-orbitDist), (int)(parent.loc.Y - orbitDist), (int)(2*orbitDist), (int)(2 * orbitDist)); }
    }
}
