using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ex92
{
    public partial class Form1 : Form
    {

        public class MyClass
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        List<MyClass> list;

        public void init()
        {
            list = new List<MyClass>();
            for (int i = 0; i < 10; i++)
            {
                int id = i;
                string name = "user " + id;
                list.Add(new MyClass { ID = id, Name = name });
            }
        }

        public Form1()
        {
            init();
            InitializeComponent();
            MyComboBox.DataSource = list;
        }
    }


}
