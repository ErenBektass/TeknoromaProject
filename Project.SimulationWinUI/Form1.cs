using Project.DAL.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project.SimulationWinUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            mc = new MyContext();
        }

        MyContext mc;

        private void Form1_Load(object sender, EventArgs e)
        {
            mc.Categories.ToList();

        }
    }
}
