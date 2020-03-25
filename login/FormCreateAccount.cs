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

    public partial class FormCreateAccount : Form
    {
        SqlConnection sqlcon = null;//sql connection variable
        public FormCreateAccount() //constructor
        {
            InitializeComponent();

            Connection open = new Connection();// create a connection object
            this.sqlcon = open.connect();//set sqlcon to the sql connection object returned from the connect function

            groupCombo.Items.Add("Admin"); //fill combo box
            groupCombo.Items.Add("Clerk");
            groupCombo.Items.Add("Client");
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            SqlCommand query = new SqlCommand("INSERT INTO Login VALUES (@loginID, @Username, @Password, @Usergroup);", sqlcon);//insert product into database
            if (string.IsNullOrEmpty(usernameBox.Text) || string.IsNullOrEmpty(passwordBox.Text))//check if any of the text box is empty
            {
                MessageBox.Show("Need more information!");//show message
            }
            else if (usernameExists(usernameBox.Text))
            {
                MessageBox.Show("Username already exists.");//show message
            }
            else
            {
                //get vals from text boxes
                query.Parameters.AddWithValue("@LoginID", get_id());
                query.Parameters.AddWithValue("@Username", usernameBox.Text);
                query.Parameters.AddWithValue("@Password", passwordBox.Text);
                query.Parameters.AddWithValue("@Usergroup", groupCombo.SelectedItem);

                if (groupCombo.SelectedIndex > -1)//check if something is selected in groupCombo
                {//if true
                    sqlcon.Open();//open database
                    query.ExecuteNonQuery();//execute query
                    MessageBox.Show("User " + usernameBox.Text + " inserted.");//show message box   
                    sqlcon.Close();//close database

                    usernameBox.Text = String.Empty;//empty textbox2
                    passwordBox.Text = String.Empty;//empty textbox3

                    groupCombo.SelectedIndex = -1;//reset combobox1
                }
                else
                {//if false
                    MessageBox.Show("Group not selected!");//show message box
                }
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private int get_id()//auto increment login id
        {
            sqlcon.Open();//open database
            SqlCommand query = new SqlCommand("SELECT MAX(LoginID) FROM Login;", sqlcon);//get the highest login id
            string output = query.ExecuteScalar().ToString();//set output to value output from executed query
            sqlcon.Close();//close database
            if(output == "")
            {
                return 1;
            }
            int id = Int32.Parse(output);//convert output to integer and set it to id
            id++;//increment id
            return id;
        }

        public bool usernameExists(string username) //return true if the username already exists in the database
        {
            sqlcon.Open();//open database
            SqlCommand query = new SqlCommand("SELECT Username FROM Login WHERE Username = @Username;", sqlcon);
            query.Parameters.AddWithValue("@Username", username); //check function parameter

            bool name = false; //false by default (assume the username does not exist)

            if (query.ExecuteScalar() != null) //if something is found in the table with parameter username
                name = true; //set bool to true, username exists

            sqlcon.Close();

            return name; //return bool
        }
    }
}
