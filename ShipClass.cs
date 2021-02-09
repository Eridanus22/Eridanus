using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eridanus
{
    public class ShipClass
    {
        String name;
        int numBuilt;
        public string imgfile;  //image to be used
        public Texture2D sprite;
        public Vector2 scale;
        public Boolean military;
        public uint empireID;
        public List<uint> shipsOfClass;
        //public //list of components
        //fuel capacity
        //cargo capacity
        //public Bridge component
        //ship billets
        //engine metrics
        //turn rate (check if engine is damaged)

        public ShipClass() {
            FileStream fileStream = new FileStream("Content/sprites/frigateClass1a.png", FileMode.Open);
            sprite = Texture2D.FromStream(DrawTest.graphicsDevice, fileStream);
            fileStream.Dispose();
            this.calcScale();
        }

        public void constructShip(uint key)
        {
            shipsOfClass.Add(key);
            numBuilt++;
        }
  
        public void calcScale() {
            float s = (250 / sprite.Width);
            scale = new Vector2(s, s);
        }


    }
}
