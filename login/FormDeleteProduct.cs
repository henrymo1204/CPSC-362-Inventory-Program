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
    public partial class FormDeleteProduct : Form
    {
        SqlConnection sqlcon = null;
        int count = 0;

        public FormDeleteProduct(FormMain form)
        {
            InitializeComponent();
            Connection open = new Connection();
            this.sqlcon = open.connect();
            update_combobox();
        }

        public delegate void UpdateDelegate(object sender, UpdateEventArgs args);
        public event UpdateDelegate UpdateEventHandler;

        public class UpdateEventArgs : EventArgs
        {
            public string Data { get; set; }
        }

        protected void update()
        {
            UpdateEventArgs args = new UpdateEventArgs();
            UpdateEventHandler.Invoke(this, args);
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

        private void Button2_Click(object sender, EventArgs e)
        {   //exit
            Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {//delete
            sqlcon.Open();
            SqlCommand query = new SqlCommand("DELETE FROM Product WHERE productID = @productID", sqlcon);
            SqlCommand query1 = new SqlCommand("SELECT ProductID FROM Product WHERE ProductName = '" + comboBox1.SelectedItem + "';", sqlcon);
            SqlDataReader read = query1.ExecuteReader();
            read.Read();
            string output = read.GetString(0);
            read.Close();
            query.Parameters.AddWithValue("@productID", output);
            query.ExecuteNonQuery();
            update();
            sqlcon.Close();
            MessageBox.Show("Product Deleted.");
            update_combobox();
            comboBox1.Text = string.Empty;
        }
    }
}
