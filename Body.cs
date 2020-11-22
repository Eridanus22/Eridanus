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
        public Body() {}


        public void initialize() {
            this.getBox();
            base.initialize();
        }

        public virtual void simulateOrbit(){ this.getBox();  }

        public virtual void drawOrbit(SpriteBatch s, float z) { return; }

        public virtual void readSprite()
        {
            try
            {
                FileStream fileStream = new FileStream(imgfile, FileMode.Open);
                sprite = Texture2D.FromStream(DrawTest.graphicsDevice, fileStream);
                fileStream.Dispose();
            }
            catch (Exception) { //default image when stream errors
                imgfile = "Map-617.png";
                FileStream fileStream = new FileStream("Content/sprites/" + imgfile, FileMode.Open);
                sprite = Texture2D.FromStream(DrawTest.graphicsDevice, fileStream);
                fileStream.Dispose();
            }
        }

        public virtual void calcRadians() {}

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
            box = new Rectangle((int)((loc.X-(radius/2000))),(int)(loc.Y-(radius/2000)), (int)(radius/1000), (int)(radius / 1000));
        }

        //used for random initial location along orbit 
        public virtual void randLoc()
        {
            Random rand = new Random();
            theta = (rand.NextDouble() + radians) % (2 * Math.PI);
            loc.X = (orbitDist * (float)Math.Cos(theta));
            loc.Y = (orbitDist * (float)Math.Sin(theta));
            this.getBox();  //only need to get box once if orbits are not simulated
        }

        public virtual void drawBody(SpriteBatch s, float z)
        {
            s.Draw(sprite, destinationRectangle: box);
        }
    }
}
