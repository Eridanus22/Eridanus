using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eridanus
{
    public class Empire
    {
        public string name, shortname, shipPrefix;
        public uint empireID;
        public List<uint> ships;
        public List<ShipClass> shipClasses;
        //public List<colony> colonies
        //flag, logo
        //races, etc.
        public float constructionRate, researchRate, trainingRate, shipbuildRate;
        public Color empColor;
        //public Government gov;
        //public Military empMil;  ->public List<Officer> officers
        //territory
        //resources?
    }
}
