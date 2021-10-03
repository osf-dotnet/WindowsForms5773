using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ex3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        DialogResult result =  MessageBox.Show("כאן התוכן ...",
                                        "כאן הכותרת",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question,
                                        MessageBoxDefaultButton.Button1,
                                        MessageBoxOptions.RightAlign);


            switch (result)
            {
                case DialogResult.No:
                    MessageBox.Show("לחצת על לא");
                    break;

                case DialogResult.Yes:
                    MessageBox.Show("לחצת על כן");
                    break;

                default:
                    break;
            }


        }
    }
}
