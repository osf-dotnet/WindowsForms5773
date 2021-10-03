using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ex91
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.MouseClick += new MouseEventHandler(Form1_MouseClick);
        }

        void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            Button b = new Button();
            b.Location = new Point(x, y);

            switch (e.Button)
            {
                case MouseButtons.Left:
                    b.BackColor = Color.Blue;
                    break;

                case MouseButtons.Middle:
                    b.BackColor = Color.Pink;
                    break;

                case MouseButtons.Right:
                    b.BackColor = Color.Red;
                    break;

                default:
                    break;
            }
            this.Controls.Add(b);
        }

    }
}
