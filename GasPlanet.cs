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

        public GasPlanet(string n, string img, double m, double r, float od, double t, float yrLen, float dayLen)
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
    }
}

