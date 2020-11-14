using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eridanus.SpaceSystems
{
    public class SurfacePlanet : Planet
    {
        //public double escapeVel; //meters per second
        //public int habitability;
        //other enviromental BS, water etc. 
        //atmosphere
        //planet surface, resources

        public SurfacePlanet() { }

        public SurfacePlanet(String img, double rad, Vector2 l)
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
            base.simulateOrbit();
        }
    }
}

