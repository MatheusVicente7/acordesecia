using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Estoque1
{
    public partial class Stock : Form
    {
        public Stock()
        {
            InitializeComponent();
        }

        private void Stock_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Products pro = new Products();
            pro.MdiParent = this;
            pro.Show();
        }
    }
}
