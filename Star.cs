using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eridanus.SpaceSystems
{
    class Star : Body
    {
		public string steClass; //class of star Ex. G2V
		public uint age;     //thousands of years
		public double luminosity; //Brightness in Lumens (1 L = 1 Sol ssun)
		public uint baseTemp;	//temperature of internal in Kelvin
		public uint surTemp;    //temperature of surface in Kelvin
		//public color value

		//default constructor
		public Star(string n, string img, double m, double r) {
			name = n;
			imgfile = img;
			mass = m;
			radius = r;
			radians = 0;
			orbitDist = 0;
		}

		//creates a star from scratch
		public void GenerateStar()
		{
			//Class O (0.00003%) very large and blue
			//Class B (0.125%) extremely luminous and blue
			//Class A (0.625%) white-bluish white
			//Class F (3.03%) white
			//Class G (7.5%) yellow
			//Class K (12%) orange
			//Class M (76%) low luminosity, mostly red dwarfs

			Random random = new Random((int)DateTime.Now.Ticks);
			int num = random.Next(10000);   //num is [0, 9999]

			if (num > 2500) //Class M
			{   //Red, <3500K

			}
			else if (num > 1300)    //Class K
			{   //Orange-red, 3500-5000K

			}
			else if (num > 600) //Class G
			{   //White-yellow, 5000K-6000K

			}
			else if (num > 300) //Class F
			{
				//Blue-white, 6000K-7500K

			}
			else if (num > 40)  //Class A
			{
				//Blue, 7500K-11000K

			}
			else if (num > 3)   //Class B
			{
				//Blue, 11000-25000K

			}
			else  //Class 0
			{
				//Blue, >25000K

			}



		}

		public override void simulateOrbit() { }


		//TODO
		//Supernova, death of star
	}
}
