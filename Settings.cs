using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Eridanus
{
    public class Settings
    {
        static bool music;
        static bool graphics;
        static bool pause;
        public static bool drawOrbits;
        static uint maxframes;

        public Settings() {
            this.initSettings();
        }

        public void initSettings()
        {
            //load config/settings textfile and read options
            short i = 4;   //number of config items
            string[] lines = new string[i];
            try
            {
                string fileName = System.IO.Path.GetFullPath(Directory.GetCurrentDirectory() + @"\config.txt");
                lines = File.ReadAllLines(fileName, Encoding.UTF8);
            }
            catch (Exception) { MessageBox.Show("config.txt is missing"); }

            try
            {
                if (lines[0][6] == '1') //play music
                {
                    music = true;

                    //Play music
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer();
                    player.SoundLocation = Environment.CurrentDirectory + @"\Music\lightyears.wav";
                    player.PlayLooping();

                }
                else
                {
                    music = false;
                }

                if (lines[1][9] == '1') //display graphics
                {
                    graphics = true;

                }
                else
                {
                    graphics = false;
                }

                if (lines[2][6] == '1') //auto pause
                {
                    pause = false;

                }
                else
                {
                    pause = false;
                }

                string str = lines[3].Substring(9);
                maxframes = uint.Parse(str);

            }
            catch (Exception) { 
                MessageBox.Show("config.txt is improperly formatted, resetting to default values");
                music = true;
                graphics = true;
                pause = false;
                maxframes = 0;  //0 = no limit

            }
        }

        public static uint getFrames()
        {
            return maxframes;
        }

        public void writeSettings() //outputs current settings to config.txt
        {
            //check if config.txt exists
            try{
                string fileName = System.IO.Path.GetFullPath(Directory.GetCurrentDirectory() + @"\config.txt");
            }
            catch (Exception) {
                //otherwise create config.txt

            }



            //write to config.txt
        }
    }
}
