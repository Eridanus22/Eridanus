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
        //public rotation relative to parent

        public Moon(String img, Vector2 size, Vector2 l, Body p)
        {
            imgfile = img;
            parent = p;
            FileStream fileStream = new FileStream("Content/sprites/" + imgfile, FileMode.Open);
            sprite = Texture2D.FromStream(DrawTest.graphicsDevice, fileStream);
            fileStream.Dispose();
            scale = size;
            theta = 0;
            orbitDist = ((l.X * l.X) + (l.Y * l.Y));
            orbitDist = (float)Math.Sqrt(orbitDist);

            loc = new Vector2(parent.loc.X + l.X, parent.loc.Y + l.Y);
            base.initialize();
        }

        public override void simulateOrbit()
        {
            //update after parent
            base.simulateOrbit();
            /*
             theta = (theta + radians) % (2 * Math.PI);
            loc.X = (orbitDist * (float)Math.Cos(theta));
            loc.Y = (orbitDist * (float)Math.Sin(theta));
              */
            loc.X += parent.loc.X;
            loc.Y += parent.loc.Y;
        }
    }
}
