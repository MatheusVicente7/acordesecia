using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Estoque1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Clear();
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Fazer Login,conferindo Usuario e Senha
            SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT *
                FROM[Stock].[dbo].[login] where username = '" + textBox1.Text + "' and password = '" + textBox2.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                this.Hide();
                Stock main = new Stock();
                main.Show();
            }
            else
            {
                MessageBox.Show("Senha e Usuário Estão inválidos.", "Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
                button1_Click(sender, e);
            }



        }
    }
}
