using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eridanus
{
    public class Pos
    {
        public double x;
        public double y;

        //default constructor
        public Pos() { }

        //constructor sets initial position
        public Pos(double X, double Y)
        {
            this.x = X;
            this.y = Y;
        }

        //moves specified amount in 2 dimensions
        public void moveDir(float X, float Y)
        {
            x += X;
            y += Y;
        }

        public double getX()
        {
            return x;
        }

        public double getY()
        {
            return y;
        }

        //sets position to any coordinates
        public void setPos(double X, double Y)
        {
            this.x = X;
            this.y = Y;
        }
    }
}
