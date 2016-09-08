using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sqlite2Cs
{
    public partial class AddInterface : Form
    {
        public AddInterface()
        {
            InitializeComponent();
        }


        public Action<string> AfterChangeTextDel { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            if (AfterChangeTextDel != null && !string.IsNullOrEmpty(textBox1.Text))
            {
                AfterChangeTextDel(textBox1.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
