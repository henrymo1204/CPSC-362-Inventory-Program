using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using login.classes;

namespace login
{
    public partial class FormSearchProduct : Form
    {

        SqlConnection sqlcon = null;

        public FormSearchProduct()
        {
            InitializeComponent();
            Connection open = new Connection();
            this.sqlcon = open.connect();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {//search product quantity
            sqlcon.Open();
            SqlCommand query1 = new SqlCommand("SELECT ProductName FROM Product WHERE ProductID = @ProductID", sqlcon);
            query1.Parameters.AddWithValue("@ProductID", textBox1.Text);
            SqlCommand query2 = new SqlCommand("SELECT Quantity FROM Product WHERE ProductID = @ProductID", sqlcon);
            query2.Parameters.AddWithValue("@ProductID", textBox1.Text);
            SqlCommand query3 = new SqlCommand("SELECT Location FROM Product WHERE ProductID = @ProductID", sqlcon);
            query3.Parameters.AddWithValue("@ProductID", textBox1.Text);
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("You did not enter a product ID!");
            }
            else
            {
                string name = query1.ExecuteScalar().ToString();
                string quantity = query2.ExecuteScalar().ToString();
                string location = query3.ExecuteScalar().ToString();
                MessageBox.Show("Product " + textBox1.Text + " " + name + " has " + quantity + " in stock at " + location + ".");
            }
            sqlcon.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {//exit
            Close();
        }
    }
}
