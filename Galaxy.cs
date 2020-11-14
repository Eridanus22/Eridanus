using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Eridanus.SpaceSystems
{
    public static class Galaxy
    {
        public static String name;
        public static List<SolSystem> solSystems; //all systems
        public static List<Craft> crafts;  //all crafts
        public static List<int> galacticCraft; //ships on galactic plane
        //boundaries

        public static void init()
        {
            name = "null";
            solSystems = new List<SolSystem>();
            crafts = new List<Craft>();
            galacticCraft = new List<int>();
        }

        public static void load()
        {
            SolSystem test = new SolSystem(Vector2.Zero);
            test.bodies.Add(new Body("sol.png", 695508, new Vector2(0, 0)));
            test.bodies.Add(new SurfacePlanet("mercury.png", 2439, new Vector2(5790, 00)));
            test.bodies.Add(new SurfacePlanet("venus.png", 6052, new Vector2(10820, 00)));
            test.bodies.Add(new SurfacePlanet("Earth.png", 6371, new Vector2(14960, 0)));
            test.bodies.Add(new SurfacePlanet("Mars.png", 6371, new Vector2(22790, 000)));
            test.bodies.Add(new GasPlanet("Jupiter.png", 69911, new Vector2(77860, 00)));
            test.bodies.Add(new GasPlanet("saturn.png", 58232, new Vector2(143350, 00)));
            test.bodies.Add(new GasPlanet("uranus.png", 25362, new Vector2(287250, 00)));
            test.bodies.Add(new GasPlanet("neptune.png", 24622, new Vector2(449510, 00)));
            test.bodies.Add(new SurfacePlanet("pluto.png", 1188, new Vector2(590640, 00)));
            test.bodies.Add(new Moon("Luna.png", 1738, new Vector2(38.44f, 00), test.bodies[3]));
            

            test.bodies[1].radians = .000049583f * 60;
            test.bodies[2].radians = .000019393f * 60;
            test.bodies[3].radians = .000011954f * 60;
            test.bodies[4].radians = .000006351f * 60;
            test.bodies[5].radians = .000001008f * 60;
            test.bodies[6].radians = .000024733f;
            test.bodies[7].radians = .000008539f;
            test.bodies[8].radians = .000004347f;
            test.bodies[9].radians = .000002892f;
            test.bodies[10].radians = .009696274f;

            //asteroid belt
            /*
            for(int i=0; i< 1000; i++)
            {
                Random rand = new Random();
                int num = rand.Next(379, 478);
                int n = rand.Next(0, 6283);

                test.bodies.Add(new Body("asteroid.png", new Vector2(.8f, .8f), new Vector2(num * 100, 00)));
                test.bodies[10 + i].theta = (float)n / 1000;
                test.bodies[10 + i].radians = (float)2/(num*100);
            }
            */
            
            test.crafts.Add(0);
            Galaxy.solSystems.Add(test);

            //second system
            SolSystem test2 = new SolSystem(new Vector2(100000, 100000));
            test2.bodies.Add(new Body("blueStar.png", 300000, new Vector2(0, 0)));
            test2.bodies.Add(new Planet("tan.png", 5000, new Vector2(14960, 0)));
            test2.bodies.Add(new Planet("swamp.png", 6500, new Vector2(22790, 000)));
            test2.bodies[1].radians = .000049583f * 60;
            test2.bodies[2].radians = .000019393f * 60;
            Galaxy.solSystems.Add(test2);
            Craft testCraft = new Craft(Vector2.Zero, 0, .01f);
            testCraft.orders = new ArrayOrders();
            testCraft.orders.enqueue(new MoveTo(new Vector2(100000, 1000)));
            Galaxy.crafts.Add(testCraft);
        }


        public static void genGalaxy(int sysNum, int bounds)
        {
            solSystems = new List<SolSystem>(sysNum);

        }

        public static void updateOrbits()
        {
            //simulate all systems
            for(int i=0; i< solSystems.Count; i++)
            {
                solSystems[i].simulateOrbits(); 
            }

        }

        public static void updateCraft() {

            //update all crafts (position and hypertravel)
            for (int i = 0; i < crafts.Count; i++)
            {
                crafts[i].update();
            }

            //combat

            //complete orders

        }

        public static void updateProduction() { }

        public static String getSystemName(int sys)
        {
            if (sys > -1)
            {
                return solSystems[sys].name;
            }
            else { return "GALACTIC PLANE";  }
        }
    }
}
