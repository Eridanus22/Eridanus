using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eridanus.SpaceSystems
{
    /*
     Simple order to move to a destination in a linear fashion
     */
    class MoveTo : Order
    {
        public Vector2 destination;

        public MoveTo(Vector2 pos) { destination = pos;}

        public override void start(Craft craft)
        {
            theta = (float)Math.Atan2(destination.Y - craft.loc.Y, destination.X - craft.loc.X);
        }
        public override void update(Craft craft) {
            craft.loc.X += craft.speed * (float)Math.Cos(theta);
            craft.loc.Y += craft.speed * (float)Math.Sin(theta);

            //check for when its in range within a second
            if (craft.loc == destination)
            {
                //advance to next order
                craft.orders.next(craft);
            }
        }

        public override float getOrientation(Vector2 pos)
        {
            return theta;
        }

        public override String toString() { return "Move to "; }

    }
}
