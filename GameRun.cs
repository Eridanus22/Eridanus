using Eridanus;
using Eridanus.SpaceSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Xna.Framework;
using EridanusA;

namespace Eridanus
{
    public static class GameRun
    {
        public static WorldTime worldTime;
        public static Boolean paused;

        public static void init()
        {
            worldTime = new WorldTime();
            paused = false;
            GameWait.init();
            Galaxy.init();
        }

        public static void run()
        {

            while (true)
            {

                if (!paused)
                {
                    GameWait.wait();    //waits in between cycles

                    if (worldTime.newMinute())
                    {
                        //update orbits
                        if (worldTime.newHour())
                        {
                            Galaxy.updateOrbits();

                            //update production (once an in-game day?)
                            if (worldTime.newDay())
                            {
                                //check colony production, shipyards, factories, mines
                                Galaxy.updateProduction();
                            }
                        }
                        //ground combat

                    }
                    //update all ship positions/status
                    Galaxy.updateCraft();

                    worldTime.increment();
                }
            }
        }
    }
}
