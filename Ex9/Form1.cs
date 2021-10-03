using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ex9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            addButtonMouseEvent(this);

        }


        public void addButtonMouseEvent(Control c)
        {
            foreach (var item in c.Controls)
            {

                if (item is Control)
                    addButtonMouseEvent((Control)item);

                Button b = item as Button;
                if (b != null)
                {
                    b.MouseEnter += new EventHandler(button_MouseEnter);
                    b.MouseLeave += new EventHandler(button_MouseLeave);
                }
            }
        }

        void button_MouseLeave(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (b != null)
            {
                b.Size = new Size(b.Size.Width / 2, b.Size.Height / 2);

            }
        }

        void button_MouseEnter(object sender, EventArgs e)
        {

            Button b = sender as Button;
            if (b != null)
            {
                b.Size += b.Size;
            }
        }
    }
}
