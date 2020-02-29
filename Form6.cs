using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace login
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Button3_Click(object sender, EventArgs e)
        {  //Exit
            Close();

            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //form7
            Form7 form = new Form7();
            form.ShowDialog();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //form8
            Form8 form = new Form8();
            form.ShowDialog();
        }
    }
}
