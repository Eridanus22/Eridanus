using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eridanus.SpaceSystems
{
    public abstract class Order
    {
        public float theta=0;
        public abstract void update(Craft craft);

        public abstract void start(Craft craft);

        public virtual float getOrientation(Vector2 pos) { return 0;  }

        public virtual String toString() { return "null";  }
    }
}
