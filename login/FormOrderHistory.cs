using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using login.classes;

namespace login
{
    public partial class FormOrderHistory : Form
    {
        SqlConnection sqlcon = null;
        Client client;
        User user;

        public FormOrderHistory(User u, Client c)
        {
            InitializeComponent();
            Connection open = new Connection();// create a connection object
            this.sqlcon = open.connect();//set sqlcon to the sql connection object returned from the connect function
            user = u;
            client = c;
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {           
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["login.Properties.Settings.LoginConnectionString"].ConnectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                string query = "select * from OrderRecord where OrderDate between '{dtFromDate.Value}' and '{dtToDate.Value}'";

                orderRecordBindingSource.DataSource = db.Query<Orders>(query, commandType: CommandType.Text);
            }
            
        }

        private void buttonView_Click(object sender, EventArgs e)
        {
            Orders obj = orderRecordBindingSource.Current as Orders;
            if (obj != null)
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["login.Properties.Settings.LoginConnectionString"].ConnectionString))
                {
                    if (db.State == ConnectionState.Closed)
                    {
                        db.Open();
                    }
                    // need adjustments
                    string query = "select p.ProductID, p.ProductName, d.Quantity, p.Price from OrderList d inner join Product p on d.ProductID = p.ProductID" +
                            $" where d.OrderID = '{obj.OrderID}'";

                    List<OrderDetail> list = db.Query<OrderDetail>(query, commandType: CommandType.Text).ToList();
                    using (FormInvoice frm = new FormInvoice(obj, user, client))
                    {
                        frm.ShowDialog();
                    }
                }
            }
        }
        
        private void FormOrderHistory_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'loginDataSet.OrderRecord' table. You can move, or remove it, as needed.
            this.orderRecordTableAdapter.Fill(this.loginDataSet.OrderRecord);
     
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            sqlcon.Open();
            SqlCommand query1 = new SqlCommand("SELECT MIN(OrderDate), Max(OrderDate) FROM OrderRecord;", sqlcon);
            SqlDataReader read = query1.ExecuteReader();
            while (read.Read())
            {
                dt1 = Convert.ToDateTime(read.GetString(0)).AddDays(-1);
                dt2 = Convert.ToDateTime(read.GetString(1));
                dtFromDate.Value = dt1;
                dtToDate.Value = dt2;
            }
            sqlcon.Close();
        }
        
    }
}
