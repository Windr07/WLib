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
        private List<FeatureAttributeClass> _featAttList = new List<FeatureAttributeClass>();

        public frmQueryAttribute()
        {
            InitializeComponent();
            
        }

        public void ShowAttribute()
        {
            this.treeView1.Nodes.Clear();
            this.textBox1.Text = "";
            for (int i = 0; i < this._featAttList.Count; i++)
            {
                this.treeView1.Nodes.Add(_featAttList[i].LayerName + ":\r\n 要素" + i.ToString());
            }
        }

        public void getFeature(List<FeatureAttributeClass> featAttList)
        {
            this._featAttList = featAttList;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int i = this.treeView1.SelectedNode.Index;
            List<string> attributes = new List<string>();
            attributes.AddRange(_featAttList[i].getAttribute());

            textBox1.Text = "";
            for (int j = 0; j < attributes.Count; j++)
            {
                textBox1.Text += attributes[j] + "\r\n";
            }
        }

        private void frmQueryAttribute_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

    }
}
