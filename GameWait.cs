using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eridanus
{
    public static class GameWait
    {
        public static float gameSpeed;
        public static SpinWait sw;
        public static int callsSince;

        public static void init()
        {
            sw = new SpinWait();
            gameSpeed = 10000;
            callsSince = 0;
        }

        public static void wait()
        {
            
            if (gameSpeed > 0)
            {
                if (gameSpeed == 10000)
                {
                    for (int i = 0; i < 10000; i++)
                    {
                        sw.SpinOnce();
                    }
                }
                else if(gameSpeed>=1)
                {
                    for (int i = 0; i < gameSpeed; i++)
                    {
                        sw.SpinOnce();
                    }
                }
                else
                {
                    if ((callsSince * gameSpeed) >= 1)
                    {
                        sw.SpinOnce();
                        callsSince = 0;
                    }
                }
                callsSince++;
            }
        }

        public static void updateSpeed(int val)
        {
            if (val == 1000) { gameSpeed = 10000; } //sec/second
            else if (val == 0) { gameSpeed = 0; }   //no limit
            else
            {
                if (val > 950) { gameSpeed = 1000; }
                else if(val > 900) { gameSpeed = 100;  }    //min/second
                else if (val > 850) { gameSpeed = 10;  }    //10min/second
                else if(val > 800) { gameSpeed = 1; }    //hour/second
                else if (val > 700) { gameSpeed = .5f; }
                else if (val > 600) { gameSpeed = .1f; }
                else if (val > 500) { gameSpeed = .05f; }
                else if (val > 400) { gameSpeed = .01f; }
                else if (val > 300) { gameSpeed = .005f; }
                else if (val > 200) { gameSpeed = .0025f; }
                else if (val > 100) { gameSpeed = .001f; }
                else { gameSpeed = .0005f;  }
            }
            callsSince = 0;
        }
    }
}
