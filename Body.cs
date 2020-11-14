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
    public class Body : BaseObj
    { 
        public string name;     //name of body, Ex. Earth
        public string imgfile;  //image to be used
        public double mass;   //mass in metric tons
        public double radius;  //radius in km
        public uint temp;   //temp in kelvin
        public float orbitDist; //distance in 10km 
        public float radians;     //movement in radians relative to the sun per minute
        public double theta;
        public Texture2D sprite;
        public Rectangle box;
        //public uint curSystem;

        //default constructor
        public Body() {
            imgfile = "Map-617.png";
            FileStream fileStream = new FileStream("Content/sprites/" + imgfile, FileMode.Open);
            sprite = Texture2D.FromStream(DrawTest.graphicsDevice, fileStream);
            fileStream.Dispose();
        }
        public Body(String img, double rad, Vector2 l)
        {
            imgfile = img;
            FileStream fileStream = new FileStream("Content/sprites/" + imgfile, FileMode.Open);
            sprite = Texture2D.FromStream(DrawTest.graphicsDevice, fileStream);
            fileStream.Dispose();
            radius = rad;
            loc = l;
            theta = 0;
            orbitDist = ((loc.X*loc.X) + (loc.Y*loc.Y));
            orbitDist = (float)Math.Sqrt(orbitDist);
            this.initialize();
        }

        public void initialize() {
            this.getBox();
            base.initialize();
        }

        public void renameBody(string n)
        {
            name = n;
        }

        public virtual void simulateOrbit(){ this.getBox();  }

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

        public virtual Vector2 getOrbitCenter()
        {
            return Vector2.Zero;
        }

        public void getBox()
        {
            box = new Rectangle((int)(loc.X-(radius/2000)),(int)(loc.Y-(radius/2000)), (int)(radius/1000), (int)(radius / 1000));
        }
    }
}
