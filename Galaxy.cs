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
            SolSystem test = new SolSystem();
            test.bodies.Add(new Body("sol.png", new Vector2(10f, 10f), new Vector2(0, 0)));
            test.bodies.Add(new SurfacePlanet("mercury.png", new Vector2(.2f, .2f), new Vector2(5790, 00)));
            test.bodies.Add(new SurfacePlanet("venus.png", new Vector2(1.8f, 1.8f), new Vector2(10820, 00)));
            test.bodies.Add(new SurfacePlanet("Earth.png", new Vector2(3f, 3f), new Vector2(14960, 0)));
            test.bodies.Add(new SurfacePlanet("Mars.png", new Vector2(1.6f, 1.6f), new Vector2(22790, 000)));
            test.bodies.Add(new GasPlanet("Jupiter.png", new Vector2(5f, 5f), new Vector2(77860, 00)));
            test.bodies.Add(new GasPlanet("saturn.png", new Vector2(5f, 5f), new Vector2(143350, 00)));
            test.bodies.Add(new GasPlanet("uranus.png", new Vector2(2f, 2f), new Vector2(287250, 00)));
            test.bodies.Add(new GasPlanet("neptune.png", new Vector2(2f, 2f), new Vector2(449510, 00)));
            test.bodies.Add(new SurfacePlanet("pluto.png", new Vector2(.8f, .8f), new Vector2(590640, 00)));
            test.bodies.Add(new Moon("Luna.png", new Vector2(.5f, .5f), new Vector2(38.44f, 00), test.bodies[3]));
            

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
            for(int i=0; i< 1000; i++)
            {
                Random rand = new Random();
                int num = rand.Next(379, 478);
                int n = rand.Next(0, 6283);

                test.bodies.Add(new Body("asteroid.png", new Vector2(.8f, .8f), new Vector2(num * 100, 00)));
                test.bodies[10 + i].theta = (float)n / 1000;
                test.bodies[10 + i].radians = (float)2/(num*100);
            }
            test.loc = new Vector2(100, 100);
            test.crafts.Add(0);
            Galaxy.solSystems.Add(test);

            //second system
            SolSystem test2 = new SolSystem();
            test2.bodies.Add(new Body("blueStar.png", new Vector2(12f, 12f), new Vector2(0, 0)));
            test2.bodies.Add(new Planet("tan.png", new Vector2(3f, 3f), new Vector2(14960, 0)));
            test2.bodies.Add(new Planet("swamp.png", new Vector2(1.6f, 1.6f), new Vector2(22790, 000)));
            test2.bodies[1].radians = .000049583f * 60;
            test2.bodies[2].radians = .000019393f * 60;
            test2.loc = new Vector2(100000, 100000);
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
