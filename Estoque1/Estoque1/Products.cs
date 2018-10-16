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
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Products_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            //Inserir Logica
            con.Open();
            Boolean status = false;
            if(comboBox1.SelectedIndex == 0)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            var sqlQuery = "";
            if(ifProductExists(con, textBox1.Text))
            {
                sqlQuery = @"UPDATE[product]  SET [ProductName] = '" + textBox2.Text + "' ,[ProductStatus] = '" + status + "' WHERE [ProductCode] = '" + textBox1.Text + "'";
            }
            else
            {
                sqlQuery = @"INSERT INTO[Stock].[dbo].[product] ([ProductCode],[ProductName],[ProductStatus]) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + status + "')";
            }

            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            cmd.ExecuteNonQuery();
            con.Close();

            //Leitura de Dados
            LoadData();
        }

        private bool ifProductExists(SqlConnection con, string productCode )
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select 1 From [Stock].[dbo].[product] WHERE [ProductCode] = '" + productCode+ "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows.Count > 0)
            return true;
            else
            {
                return false;
            }
        }
        public void LoadData()
        {
            SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter("Select * From [Stock].[dbo].[product]", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["ProductName"].ToString();
                if ((bool)item["ProductStatus"])
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Ativo";
                }
                else
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Inativo";
                }
                
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString() == "Ativo")
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            var sqlQuery = "";
            if (ifProductExists(con, textBox1.Text))
            {
                con.Open();
                sqlQuery = @"DELETE FROM [product]  WHERE [ProductCode] = '" + textBox1.Text + "'" ;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                MessageBox.Show("Código não Existe");
            }

           
            //Leitura de Dados
            LoadData();
        }
    }
}
