using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GISsys
{
    public partial class frmQueryAttribute : Form
    {

        public frmQueryAttribute()
        {
            InitializeComponent();
            
        }

        public void ShowAttribute()
        {
          
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
           
        }

        private void frmQueryAttribute_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

    }
}
