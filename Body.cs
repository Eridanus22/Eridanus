using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Windows.Forms;

namespace Eridanus.SpaceSystems
{
    public class Body
    {
        private static uint nextId = 0;
        public Vector2 loc; //coordinates of body within system
        public uint id;
        public string name;     //name of body, Ex. Earth
        public string imgfile;  //image to be used
        public ulong mass;   //mass in kg
        public uint radius;  //radius in km
        public uint temp;   //temp in kelvin
        public float orbitDist; //distance in km
        public float radians;     //movement in radians relative to the sun per minute
        public double theta;
        public Texture2D sprite;
        public Vector2 scale;
        public uint curSystem;

        //default constructor
        public Body() {
            imgfile = "Map-617.png";
            FileStream fileStream = new FileStream("Content/sprites/" + imgfile, FileMode.Open);
            sprite = Texture2D.FromStream(DrawTest.graphicsDevice, fileStream);
            fileStream.Dispose();
            scale = new Vector2(0.5f, 0.5f);
        }
        public Body(String img, Vector2 size, Vector2 l)
        {
            imgfile = img;
            FileStream fileStream = new FileStream("Content/sprites/" + imgfile, FileMode.Open);
            sprite = Texture2D.FromStream(DrawTest.graphicsDevice, fileStream);
            fileStream.Dispose();
            scale = size;
            loc = l;
            theta = 0;
            orbitDist = ((loc.X*loc.X) + (loc.Y*loc.Y));
            orbitDist = (float)Math.Sqrt(orbitDist);
            this.initialize();
        }

        public void initialize() {
            this.id = nextId;
            nextId++;
        }

        public void renameBody(string n)
        {
            name = n;
        }

        public virtual void simulateOrbit()
        {
            //hypotenuse/radius = orbitDist
            theta = (theta + radians) % (2*Math.PI);
            loc.X = (orbitDist * (float)Math.Cos(theta));
            loc.Y = (orbitDist * (float)Math.Sin(theta));
        }

        public virtual uint getHabitability()
        {
            return 0;
        }

        public void calcRadians(double yrLength)
        {
            //yrLength is in Earth days
            radians = (float)((2 * Math.PI) / (yrLength * 24));
        }

        public void calcOrbit()
        {
            orbitDist = ((loc.X * loc.X) + (loc.Y * loc.Y));
            orbitDist = (float)Math.Sqrt(orbitDist);
        }
    }
}
