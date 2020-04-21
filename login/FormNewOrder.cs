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
    public partial class FormNewOrder : Form
    {
        SqlConnection sqlcon = null;

        public FormNewOrder()
        {
            InitializeComponent();
            Connection open = new Connection();// create a connection object
            this.sqlcon = open.connect();//set sqlcon to the sql connection object returned from the connect function
        }

        private void FormNewOrder_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'loginDataSet.OrderRecord' table. You can move, or remove it, as needed.
            this.orderRecordTableAdapter.Fill(this.loginDataSet.OrderRecord);

            orderStatusDataGridViewTextBoxColumn.Items.Add("New");
            orderStatusDataGridViewTextBoxColumn.Items.Add("In Progress");
            orderStatusDataGridViewTextBoxColumn.Items.Add("Shipped");
            orderStatusDataGridViewTextBoxColumn.Items.Add("Ready For Pick Up");

            shippingMethodDataGridViewTextBoxColumn.Items.Add("USPS");
            shippingMethodDataGridViewTextBoxColumn.Items.Add("UPS");
            shippingMethodDataGridViewTextBoxColumn.Items.Add("FedEx");
            shippingMethodDataGridViewTextBoxColumn.Items.Add("Pick Up");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //View
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sqlcon.Open();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[3].Value != null)
                {
                    SqlCommand query = new SqlCommand("UPDATE OrderRecord SET OrderStatus = '" + row.Cells[3].Value.ToString() + "' WHERE OrderID = '" + row.Cells[0].Value.ToString() + "';", sqlcon);
                    query.ExecuteNonQuery();
                }
                if(row.Cells[4].Value != null)
                {
                    SqlCommand query1 = new SqlCommand("UPDATE OrderRecord SET ShippingMethod = '" + row.Cells[4].Value.ToString() + "' WHERE OrderID = '" + row.Cells[0].Value.ToString() + "';", sqlcon);
                    query1.ExecuteNonQuery();
                }
                if (row.Cells[5].Value != null)
                {
                    if(row.Cells[5].Value.ToString() != " ")
                    {
                        SqlCommand query2 = new SqlCommand("UPDATE OrderRecord SET TrackingNumber = '" + row.Cells[5].Value.ToString() + "' WHERE OrderID = '" + row.Cells[0].Value.ToString() + "';", sqlcon);
                        query2.ExecuteNonQuery();
                    }
                }
            }
            sqlcon.Close();
            this.orderRecordTableAdapter.Fill(this.loginDataSet.OrderRecord);
            MessageBox.Show("Database updated.");
        }
    }
}
