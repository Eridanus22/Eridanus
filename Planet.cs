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
        public ushort yearLength; //in earth days 
        public ushort dayLength; //in hours (0=tidalLock)
        //rings?

        public Planet() { }

        public Planet(String img, double rad, Vector2 l)
        {
            imgfile = img;
            FileStream fileStream = new FileStream("Content/sprites/" + imgfile, FileMode.Open);
            sprite = Texture2D.FromStream(DrawTest.graphicsDevice, fileStream);
            fileStream.Dispose();
            radius = rad;
            loc = l;
            theta = 0;
            orbitDist = ((loc.X * loc.X) + (loc.Y * loc.Y));
            orbitDist = (float)Math.Sqrt(orbitDist);
            base.initialize();
        }

        public override void simulateOrbit()
        {
            //hypotenuse/radius = orbitDist
            theta = (theta + radians) % (2 * Math.PI);
            loc.X = (orbitDist * (float)Math.Cos(theta));
            loc.Y = (orbitDist * (float)Math.Sin(theta));
            base.simulateOrbit();
        }
    }
}
