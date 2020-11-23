using Eridanus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eridanus
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
   
            this.Controls.Add(new DrawTest() { Dock = DockStyle.Fill });
            DrawTest.gameForm = this;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (GameRun.paused == true)
            {
                GameRun.paused = false;
                this.pauseButton.BackgroundImage = global::Eridanus.Properties.Resources.pause;
            }
            else
            {
                GameRun.paused = true;
                this.pauseButton.BackgroundImage = global::Eridanus.Properties.Resources.forward;
            }
        }

        public void updateTimeLabel(WorldTime time)
        {
            timeLabel.Text = time.toString();
        }

        private void timeSlider_Scroll(object sender, EventArgs e)
        {
            int val = 1000-timeSlider.Value;

            GameWait.updateSpeed(val);
            
        }

        private void milButton_Click(object sender, EventArgs e)
        {
            MilitaryForm form = new MilitaryForm();
            form.Show();
        }
    }
}
