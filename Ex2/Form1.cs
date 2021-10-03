using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ex2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Addbutton.MouseMove += new MouseEventHandler(Addbutton_MouseMove);
        }

        void Addbutton_MouseMove(object sender, MouseEventArgs e)
        {
            Random r = new Random();
            int Xsize = r.Next(this.Size.Height / 2);
            int Ysize = r.Next(this.Size.Width / 2);
            this.Addbutton.Location = new Point(Xsize, Ysize);
        }

    }
}
