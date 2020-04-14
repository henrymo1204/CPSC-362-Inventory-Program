﻿using System;
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
    public partial class FormOrder : Form
    {

        SqlConnection sqlcon = null;//sql connection variable
        User user;

        public FormOrder(User u)
        {
            InitializeComponent();
            Connection open = new Connection();// create a connection object
            this.sqlcon = open.connect();//set sqlcon to the sql connection object returned from the connect function
            user = u;
        }

        private void FormOrder_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'loginDataSet.Product' table. You can move, or remove it, as needed.
            this.productTableAdapter.Fill(this.loginDataSet.Product);

            DataGridViewComboBoxColumn box = new DataGridViewComboBoxColumn();
            box.Name = "Quantity";
            box.HeaderText = "Quantity";
            box.Items.Add(" ");
            for(int i = 0; i < 10; i++)
            {
                int num = i + 1;
                string temp = num.ToString();
                box.Items.Add(temp);
            }
            dataGridView1.Columns.Add(box);
        }

        private string update_orderid()//auto increment product id
        {
            sqlcon.Open();//open database
            SqlCommand query = new SqlCommand("SELECT MAX(OrderID) FROM OrderRecord;", sqlcon);//get the highest product id from Product enyity
            string output = query.ExecuteScalar().ToString();//set output to value output from executed query
            sqlcon.Close();//close database
            if(output == "")
            {
                return "00001";
            }
            int id = Int32.Parse(output);//convert output to integer and set it to id
            id++;//increment id
            output = id.ToString().PadLeft(5, '0');//set output to id
            return output.ToString();
        }

        private string update_orderlistid()//auto increment product id
        {
            SqlCommand query = new SqlCommand("SELECT MAX(OrderListID) FROM OrderList;", sqlcon);//get the highest product id from Product enyity
            string output = query.ExecuteScalar().ToString();//set output to value output from executed query
            if (output == "")
            {
                return "00001";
            }
            int id = Int32.Parse(output);//convert output to integer and set it to id
            id++;//increment id
            output = id.ToString().PadLeft(5, '0');//set output to id
            return output.ToString();
        }

        private int check()
        {
            int temp = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[4].Value != null)
                {
                    temp++;
                }
            }
            if (temp == 0)
            {
                MessageBox.Show("Please selected the quantity of at least 1 product!");
                return temp;
            }
            return temp;
        }

        private void refresh()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells[4].Value = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (check() != 0)
            {
                string id = update_orderid();
                SqlCommand query = new SqlCommand("INSERT INTO OrderRecord VALUES ('" + id + "', '" + user.UserID + "')", sqlcon);
                sqlcon.Open();//open database
                query.ExecuteNonQuery();//execute query
                sqlcon.Close();//close database
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[4].Value != null)
                    {
                        if (row.Cells[4].Value.ToString() != " ")
                        {
                            sqlcon.Open();
                            double num1 = double.Parse(row.Cells[3].Value.ToString());
                            double num2 = double.Parse(row.Cells[4].Value.ToString());
                            SqlCommand query1 = new SqlCommand("INSERT INTO OrderList VALUES ('" + update_orderlistid() + "', '" + row.Cells[0].Value.ToString() + "', '" + row.Cells[4].Value.ToString() + "', '" + (num1*num2).ToString() + "', '" + id + "')", sqlcon);
                            query1.ExecuteNonQuery();
                            sqlcon.Close();
                        }
                    }
                }
                MessageBox.Show("Order " + id + " is placed.");//show message box   
                refresh();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
