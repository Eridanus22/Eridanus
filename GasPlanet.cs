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
    public class GasPlanet : Planet
    {
        //resources (gas)

        public GasPlanet(String img, Vector2 size, Vector2 l)
        {
            imgfile = img;
            FileStream fileStream = new FileStream("Content/sprites/" + imgfile, FileMode.Open);
            sprite = Texture2D.FromStream(DrawTest.graphicsDevice, fileStream);
            fileStream.Dispose();
            scale = size;
            loc = l;
            theta = 0;
            orbitDist = ((loc.X * loc.X) + (loc.Y * loc.Y));
            orbitDist = (float)Math.Sqrt(orbitDist);
            base.initialize();
        }
    }
}

