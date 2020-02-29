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
    public partial class FormSearchShipmentTime : Form
    {

        SqlConnection sqlcon = null;

        public FormSearchShipmentTime()
        {
            InitializeComponent();
            Connection open = new Connection();
            this.sqlcon = open.connect();

            sqlcon.Open();
            SqlCommand query = new SqlCommand("SELECT ShippingID FROM ShippingRecord;", sqlcon);
            SqlDataAdapter da = new SqlDataAdapter(query);
            DataSet ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                comboBox1.Items.Add(ds.Tables[0].Rows[i][0]);
            }
            sqlcon.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {//look up expected arrival time
            string message = "";
            sqlcon.Open();
            SqlCommand query1 = new SqlCommand("SELECT ArrivalTime FROM ShippingRecord WHERE ShippingID = @ShippingID;", sqlcon);
            query1.Parameters.AddWithValue("@ShippingID", comboBox1.Text);
            SqlCommand query2 = new SqlCommand("SELECT P.ProductName, IP.IncomingQuantity FROM Product AS P, IncomingProduct AS IP WHERE IP.ShippingID = @ShippingID AND IP.ProductID = P.ProductID;", sqlcon);
            query2.Parameters.AddWithValue("@ShippingID", comboBox1.Text);
            string time = query1.ExecuteScalar().ToString();
            SqlDataReader read = query2.ExecuteReader();
            while(read.Read())
            {
                message = message + read.GetString(1) + " cases of " + read.GetString(0) + "\n";
            }
            read.Close();
            MessageBox.Show("Shippment " + comboBox1.Text + " is expected to arrive on " + time + " with\n\n" + message + "\n.");
            sqlcon.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
