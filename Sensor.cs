using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eridanus
{
    class Sensor : Component
    {
        //EM, Thermal (passives)
        //Active sensors
        //resolution (target craft mass/size), strength/sensitivity, max range
        Boolean passive;    //active=false, passive=true;

        //check if sensor detects anything
        public virtual void sensorSweep() { }
    }
}
