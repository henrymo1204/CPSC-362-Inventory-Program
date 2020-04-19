using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using login.classes;

namespace login
{
    public partial class FormInvoice : Form
    {
        Orders _orders;
        List<OrderDetail> _list; //unused
        public FormInvoice(Orders orders, List<OrderDetail> list)
        {
            InitializeComponent();
            _orders = orders;
            _list = list;
        }

        private void FormPrint_Load(object sender, EventArgs e)
        {
            //Init data source 
            //orderDetailBindingSource.DataSource = _list; // missing // for displaying OrderList for the order
            //Set parameter for your report
            Microsoft.Reporting.WinForms.ReportParameter[] p = new Microsoft.Reporting.WinForms.ReportParameter[]
            {
                new Microsoft.Reporting.WinForms.ReportParameter("pOrderID",_orders.OrderID.ToString()),
                new Microsoft.Reporting.WinForms.ReportParameter("pOrderDate",_orders.OrderDate.ToString("MM/dd/yyyy"))
            };
            this.reportViewer.LocalReport.SetParameters(p);
            this.reportViewer.RefreshReport();
        }

    }
}