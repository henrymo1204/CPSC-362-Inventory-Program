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
    public partial class FormSearchProductLocation : Form
    {

        SqlConnection sqlcon = null;
        int count = 0;

        public FormSearchProductLocation()
        {
            InitializeComponent();
            Connection open = new Connection();
            this.sqlcon = open.connect();
            update_combobox();
        }

        private void update_combobox()//refresh combobox
        {
            comboBox1.Items.Clear();
            sqlcon.Open();
            SqlCommand query = new SqlCommand("SELECT ProductBrand, ProductName FROM Product;", sqlcon);
            SqlDataAdapter da = new SqlDataAdapter(query);
            DataSet ds = new DataSet();
            da.Fill(ds);
            count = ds.Tables[0].Rows.Count;
            for (int i = 0; i < count; i++)
            {
                comboBox1.Items.Add(ds.Tables[0].Rows[i][0] + " " + ds.Tables[0].Rows[i][1]);
            }
            count--;
            sqlcon.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlcon.Open();
            SqlCommand query = new SqlCommand("SELECT ProductID FROM Product WHERE ProductName = '" + comboBox1.SelectedItem + "';", sqlcon);
            SqlDataReader read = query.ExecuteReader();
            read.Read();
            string output = read.GetString(0);
            read.Close();
            SqlCommand query1 = new SqlCommand("SELECT ProductName FROM Product WHERE ProductID = @ProductID", sqlcon);
            query1.Parameters.AddWithValue("@ProductID", output);
            SqlCommand query2 = new SqlCommand("SELECT ProductLocation FROM Product WHERE ProductID = @ProductID", sqlcon);
            query2.Parameters.AddWithValue("@ProductID", output);
            string name = query1.ExecuteScalar().ToString();
            string location = query2.ExecuteScalar().ToString();
            MessageBox.Show("Product " + output + " " + name + " is located at " + location + ".");
            sqlcon.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
