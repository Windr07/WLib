using System;
using System.Windows.Forms;

namespace WLib.Samples.WinForm.View
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
