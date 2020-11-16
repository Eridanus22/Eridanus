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
    public class Asteroid : SurfacePlanet
    {
        //public double escapeVel; //meters per second
        //public float habitability;
        //other enviromental BS, water etc. 
        //atmosphere
        //planet surface, resources

        public Asteroid(string n, string img, double m, double r, float od, double t, float yrLen, float dayLen)
        {
            name = n;
            imgfile = img;
            mass = m;
            radius = r;
            orbitDist = od;
            theta = t;
            yearLength = yrLen;
            dayLength = dayLen;
            this.readSprite();
            loc = new Vector2(orbitDist, 0);
            base.initialize();
        }

        public override void simulateOrbit()
        {
            if (Settings.simAsteroidOrbits) { base.simulateOrbit(); }
            else { this.getBox(); }
        }

        public override Rectangle getOrbitBox() { return Rectangle.Empty; }

        public override void readSprite()
        {
            try
            {
                FileStream fileStream = new FileStream(imgfile, FileMode.Open);
                sprite = Texture2D.FromStream(DrawTest.graphicsDevice, fileStream);
                fileStream.Dispose();
            }
            catch (Exception)
            { //load random asteroid img
                imgfile = "asteroid.png";
                FileStream fileStream = new FileStream("Content/sprites/" + imgfile, FileMode.Open);
                sprite = Texture2D.FromStream(DrawTest.graphicsDevice, fileStream);
                fileStream.Dispose();
            }
        }
    }
}

