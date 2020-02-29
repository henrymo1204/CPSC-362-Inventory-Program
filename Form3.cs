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

namespace login
{
    public partial class Form3 : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\henry\source\repos\login\Login.mdf;Integrated Security=True;Connect Timeout=30");
       
        public Form3(Form2 form2)
        {
            InitializeComponent();
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

        private void Button2_Click(object sender, EventArgs e)
        {   //Exit
            Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        { //insert
            sqlcon.Open();
            SqlCommand query = new SqlCommand("INSERT INTO Product VALUES (@productID, @productBrand, @productName, @price, @quantity, @expirationDate, @productLocation, @supplierID);", sqlcon);
            query.Parameters.AddWithValue("@productID", textBox1.Text);
            query.Parameters.AddWithValue("@productBrand", textBox2.Text);
            query.Parameters.AddWithValue("@productName", textBox3.Text);
            query.Parameters.AddWithValue("@price", textBox4.Text);
            query.Parameters.AddWithValue("@quantity", textBox5.Text);
            query.Parameters.AddWithValue("@expirationDate", textBox6.Text);
            query.Parameters.AddWithValue("@productLocation", textBox7.Text);
            query.Parameters.AddWithValue("@supplierID", textBox8.Text);
            query.ExecuteNonQuery();
            update();
            sqlcon.Close();
            //update gridview in form2
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;
            textBox4.Text = String.Empty;
            textBox5.Text = String.Empty;
            textBox6.Text = String.Empty;
            textBox7.Text = String.Empty;
            textBox8.Text = String.Empty;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'loginDataSet.Product' table. You can move, or remove it, as needed.
            this.productTableAdapter.Fill(this.loginDataSet.Product);
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
