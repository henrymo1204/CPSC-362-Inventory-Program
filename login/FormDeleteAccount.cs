using login.classes;
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

namespace login
{
    public partial class FormDeleteAccount : Form
    {
        SqlConnection sqlcon = null;//sql connection variable
        public FormDeleteAccount()
        {
            InitializeComponent();

            Connection open = new Connection();// create a connection object
            this.sqlcon = open.connect();//set sqlcon to the sql connection object returned from the connect function

            update_combobox();


        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            SqlCommand query = new SqlCommand("DELETE FROM Login WHERE Username = @Username", sqlcon);//delete product from database
   
            if (userCombo.SelectedIndex > -1)//check if something is selected in combobox1
            {//if true
                sqlcon.Open();//open database
                query.Parameters.AddWithValue("@Username", userCombo.SelectedItem);//set username to text in combobox
                query.ExecuteNonQuery();//execute query
                MessageBox.Show("User account " + userCombo.SelectedItem + " deleted.");//show message box
                sqlcon.Close();//close database
                update_combobox();//update combobox
                userCombo.Text = string.Empty;//reset combobox
            }
            else
            {
                MessageBox.Show("Product not selected");//show message box
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void update_combobox()//update combobox
        {
            userCombo.Items.Clear();//clear combobox
            sqlcon.Open();//open database
            SqlCommand query = new SqlCommand("SELECT Username FROM Login;", sqlcon);//get login id and username from login entity
            SqlDataReader read = query.ExecuteReader();//execute query and store values to data reader
            while (read.Read())//while reading data from data reader
            {
                string s = read.GetString(0);
                userCombo.Items.Add(s);//add items to usercombo
            }
            read.Close();//close data reader
            sqlcon.Close();//close database
        }
    }
}
