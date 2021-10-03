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

namespace Thread_windows_forms
{
    public partial class Form1 : Form
    {
        Stopwatch stopWatch;
        Thread timerThread;
        bool isTimerRun = false;

        public Form1()
        {
            InitializeComponent();
            stopWatch = new Stopwatch();
        }


void setTextInvok(string text)
{   
        this.timerLabel.Text = text;
}


private void runTimer()
{
    while (isTimerRun)
    {
        string timerText = stopWatch.Elapsed.ToString();
        timerText = timerText.Substring(0, 8);

        Action<string> d = setTextInvok;
        this.Invoke(d, new object[] { timerText });

        Thread.Sleep(1000);
    }
}


private void startTimerButton_Click(object sender, EventArgs e)
{
    if (!isTimerRun)
    {
        stopWatch.Restart();
        isTimerRun = true;

        if (timerThread != null)
            timerThread.Abort();


        timerThread = new Thread(runTimer);
        timerThread.Start();
    }

}

private void stopTimerButton_Click(object sender, EventArgs e)
{
    if (isTimerRun)
    {
        stopWatch.Stop();
        isTimerRun = false;

        if (timerThread != null)
            timerThread.Abort();
    }
}
    }
}
 