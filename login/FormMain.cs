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
    public partial class FormMain : Form
    {

        SqlConnection sqlcon = null;

        public FormMain()
        {
            InitializeComponent();
            Connection open = new Connection();
            this.sqlcon = open.connect();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            FormDeleteSupplier form = new FormDeleteSupplier();
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {//search product quantity
            FormSearchProductQuantity form = new FormSearchProductQuantity();
            form.ShowDialog();
        }


        private void Button2_Click(object sender, EventArgs e)
        {//search product location
            FormSearchProductLocation form = new FormSearchProductLocation();
            form.ShowDialog();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {//search shipment expected arrival time
            FormSearchShipmentTime form = new FormSearchShipmentTime();
            form.ShowDialog();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'loginDataSet.Product' table. You can move, or remove it, as needed.
            this.productTableAdapter.Fill(this.loginDataSet.Product);
            dataGridView1.DataSource = Source();
        }

        private DataTable dt = new DataTable();
        private DataSet ds = new DataSet();
        
        public DataTable Source()
        {
            sqlcon.Open();
            SqlCommand cmd = sqlcon.CreateCommand();
            cmd.CommandText = "SELECT * FROM Product";
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            ds.Clear();
            adap.Fill(ds);
            dt = ds.Tables[0];
            sqlcon.Close();
            return dt;
        }

        private void Form3_UpdateEventHandler(object sender, FormInsertProduct.UpdateEventArgs args)
        {
            dataGridView1.DataSource = Source();
        }

        private void Form4_UpdateEventHandler(object sender, FormDeleteProduct.UpdateEventArgs args)
        {
            dataGridView1.DataSource = Source();
        }

        private void insertProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormInsertProduct form = new FormInsertProduct(this);
            form.UpdateEventHandler += Form3_UpdateEventHandler;
            form.ShowDialog();
        }

        private void deleteProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDeleteProduct form = new FormDeleteProduct(this);
            form.UpdateEventHandler += Form4_UpdateEventHandler;
            form.ShowDialog();
        }

        private void insertSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormInsertSupplier form = new FormInsertSupplier();
            //form.UpdateEventHandler += Form5_UpdateEventHandler;
            form.ShowDialog();
        }

        private void deleteSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDeleteSupplier form = new FormDeleteSupplier();
            form.ShowDialog();
        }

        private void updateProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUpdateProduct form = new FormUpdateProduct();
            form.ShowDialog();
        }

        private void updateSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUpdateSupplier form = new FormUpdateSupplier();
            form.ShowDialog();
        }
    }
}
