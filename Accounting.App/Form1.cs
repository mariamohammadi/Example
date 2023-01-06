using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accounting.App.Accounting;
using Accounting.Utility.convertor;
using Accounting.ViewModels.accounting;
using Accounting.Business;



namespace Accounting.App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            frmCustomers frmCustomer = new frmCustomers();
            frmCustomer.ShowDialog();
        }

        private void btnNewAccounting_Click(object sender, EventArgs e)
        {
            
            frmNewtransaction frmNewtransaction = new frmNewtransaction();
            frmNewtransaction.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnReportPay_Click(object sender, EventArgs e)
        {
            frmReport frmReport = new frmReport();
            frmReport.TypeId = 2;
            frmReport.ShowDialog();
        }

        private void btnReportRecive_Click(object sender, EventArgs e)
        {

            frmReport frmReport = new frmReport();
            frmReport.TypeId = 1;
            frmReport.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            this.Hide();
            frmLogin frmLogin = new frmLogin();
            if (frmLogin.ShowDialog() == DialogResult.OK)
            {
                this.Show();
                lblDate.Text = DateTime.Now.ToShamsi();
                lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
                Report();
            }
            else
            {
                Application.Exit();
            }
        }
        void Report()
        {
            ReportViewModel report = Account.ReportForMain();
            lblPay.Text = report.Pay.ToString("#,0");
            lblRecive.Text = report.Recive.ToString("#,0");
            lblAccountBalance.Text = report.AccountBalance.ToString("#,0");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void تنظیماتورودبهبرنامهToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmLogin frmlogin = new frmLogin();
            frmlogin.IsEdit = true;
            frmlogin.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
