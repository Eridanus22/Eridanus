using Microsoft.Xna.Framework;
using System.IO;
using System.Text;
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
            readSystem("Sol.txt");
    


            
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
            /*
            test.crafts.Add(0);
            
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
            */
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


        public static void readSystem(String fileName)
        {
           
            String line;
            String[] data;
            try
            {
                fileName = System.IO.Path.GetFullPath(Directory.GetCurrentDirectory() + @"\Systems\" + fileName);
            }
            catch (Exception)
            { return; }


            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            line = file.ReadLine(); //read in header
            line = file.ReadLine(); //read in system info
            data = line.Split('\t');
            SolSystem s = new SolSystem(data[1], readVect(data[6]));
            Body prevB =new Planet();   //empty
            while ((line = file.ReadLine()) != null)
            {
                
                data = line.Split('\t');   //info delimited by tabs
                String name, imgfile;
                Body b;
                double mass, radius, theta;
                float orbitDist, yearLength, dayLength;
                Vector2 pos;
                name = data[1];
                imgfile = data[2];
                try
                {
                    imgfile = System.IO.Path.GetFullPath(Directory.GetCurrentDirectory() + @"\Systems\" +s.name + @"\" + imgfile);
                    mass = Double.Parse(data[3]);
                    radius = Double.Parse(data[4]);
                    orbitDist = float.Parse(data[5]);
                    pos = readVect(data[6]);
                    theta = Double.Parse(data[7]);
                    yearLength = float.Parse(data[8]);
                    dayLength = float.Parse(data[9]);
                }catch (Exception) { Console.WriteLine("BAD PARSE"); continue;  }   //bad input

                if (String.Equals(data[0], "asteroid"))
                {
                    //b = new Asteroid(name, imgfile, mass, radius, orbitDist, theta, yearLength, dayLength);
                    continue;
                }
                else if (String.Equals(data[0], "surPlanet"))
                {
                    b = new SurfacePlanet(name, imgfile, mass, radius, orbitDist, theta, yearLength, dayLength);
                    b.calcRadians();
                    prevB = b;
                    s.nonAstNum++;
                }
                else if (String.Equals(data[0], "gasPlanet"))
                {
                    b = new GasPlanet(name, imgfile, mass, radius, orbitDist, theta, yearLength, dayLength);
                    b.calcRadians();
                    prevB = b;
                    s.nonAstNum++;
                }
                else if (String.Equals(data[0], "moon"))
                {
                    b = new Moon(name, imgfile, mass, radius, orbitDist, theta, yearLength, dayLength, prevB);
                    b.calcRadians();
                    s.nonAstNum++;
                }
                else if (String.Equals(data[0], "star"))
                {
                    b = new Star(name, imgfile, mass, radius, pos);
                }
                else
                {
                    continue;
                }

                s.bodies.Add(b);
            }

            file.Close();

            Galaxy.solSystems.Add(s);

        }

        public static Vector2 readVect(String v)
        {
            v=v.Replace("\"", string.Empty);
            String[] x = v.Split(',');
            return new Vector2(float.Parse(x[0]), float.Parse(x[1]));
        }
    }
}
