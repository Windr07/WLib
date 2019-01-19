using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace GISsys
{
    public partial class frmHelp : Form
    {
        public frmHelp()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string[] lines = System.IO.File.ReadAllLines(Application.StartupPath + "\\help.txt", Encoding.Default);
            if (treeView1.SelectedNode == treeView1.Nodes[7])
            {
                this.richTextBox1.Text = String.Format(
                    "{0}\r\n{1}\r\n{2}\r\n{3}", lines[0], lines[1], lines[2],lines[3]);
            }
            else if (treeView1.SelectedNode == treeView1.Nodes[0])
            {
                this.richTextBox1.Text = lines[5];
            }
            else if (treeView1.SelectedNode == treeView1.Nodes[0])
            {
                this.richTextBox1.Text =lines[7];
            }
            else
            {
                this.richTextBox1.Text = "略";
            }
        }
    }
}
