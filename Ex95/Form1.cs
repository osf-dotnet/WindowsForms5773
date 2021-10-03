using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex95
{
    public partial class Form1 : Form
    {
        Stopwatch stopWatch;
        bool isTimerRun = false;

        public Form1()
        {
            InitializeComponent();
            stopWatch = new Stopwatch();
        } 

        private void runTimer()
        {
            if (isTimerRun)
            {
                string timerText = stopWatch.Elapsed.ToString();
                timerText = timerText.Substring(0, 8);
                this.timerLabel.Text = timerText;
            }
               
        }


        private void startTimerButton_Click(object sender, EventArgs e)
        {
            if (!isTimerRun)
            {
                stopWatch.Restart();
                isTimerRun = true;
            }

        }

        private void stopTimerButton_Click(object sender, EventArgs e)
        {
            if (isTimerRun)
            {
                stopWatch.Stop();
                isTimerRun = false;
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            runTimer();
        }
    }
}
 