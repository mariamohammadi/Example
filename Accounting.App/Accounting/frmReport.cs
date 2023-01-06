using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accounting.DataLayer.context;
using Accounting.Utility.convertor;
using Accounting.ViewModels.customers;

namespace Accounting.App.Accounting
{
    public partial class frmReport : Form
    {
        public int TypeId = 0;
        public frmReport()
        {
            InitializeComponent();
        }

        private void frmReportPay_Load(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                List<ListCustomerViewModel> list = new List<ListCustomerViewModel>();
                list.Add(new ListCustomerViewModel()
                {
                    CustomerId = 0,
                    FullName = "انتخاب کنید"


                });
                list.AddRange(db.CustomerRepository.GetNameCustomers());
                cbCustomer.DataSource = list;
                cbCustomer.DisplayMember = "fullName";
                cbCustomer.ValueMember = "CustomerId";
            }
            if (TypeId == 1)
            {
                this.Text = "گزارش دریافتی ها";
            }
            else
            {
                this.Text = "گزارش پرداختی ها";
            }

        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgReport.CurrentRow != null)
            {
                int id = int.Parse(dgReport.CurrentRow.Cells[0].Value.ToString());
                frmNewtransaction frmnew=new frmNewtransaction();
                frmnew.AccountID = id;
                if (frmnew.ShowDialog() == DialogResult.OK)
                {
                    Filter();
                }

            }
        }
        void Filter()
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                List<DataLayer.Accounting> result = new List<DataLayer.Accounting>();
                DateTime? startDate;
                DateTime? endDate;

                if ((int)cbCustomer.SelectedValue != 0)
                {
                    int Customerid = int.Parse(cbCustomer.SelectedValue.ToString());
                    result.AddRange(db.AccountingRepository.Get(a => a.TypeID == TypeId && a.CustomerId == Customerid));
                }
                else
                {
                    result.AddRange(db.AccountingRepository.Get(a => a.TypeID == TypeId));
                }


                if (txtFromDate.Text != "    /  /")
                {
                    startDate = Convert.ToDateTime(txtFromDate.Text);
                    startDate = DateConvertor.ToMiladi(startDate.Value);
                    result = result.Where(r => r.DateTitle >= startDate.Value).ToList();

                }
                if (txtToDate.Text != "    /  /")
                {
                    endDate = Convert.ToDateTime(txtToDate.Text);
                    endDate = DateConvertor.ToMiladi(endDate.Value);
                    result = result.Where(r => r.DateTitle <= endDate.Value).ToList();
                }


                dgReport.Rows.Clear();
                foreach(var accounting in result)
                {
                    string CustomerName = db.CustomerRepository.GetCustomerNameByid(accounting.CustomerId);
                    dgReport.Rows.Add(accounting.ID, CustomerName, accounting.Amount, accounting.DateTitle.ToShamsi(),accounting.Discription);
                }

               }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Filter();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Filter();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgReport.CurrentRow != null)
            {
                int id = int.Parse(dgReport.CurrentRow.Cells[0].Value.ToString());
                if (RtlMessageBox.Show("آیا ازحذف مطمعن هستید", "هشدار", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (UnitOfWork db = new UnitOfWork())
                    {
                        db.AccountingRepository.Delete(id);
                        db.save();
                        Filter();

                    }
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dtPrint = new DataTable();
            dtPrint.Columns.Add("Customer");
            dtPrint.Columns.Add("Amount");
            dtPrint.Columns.Add("Date");
            dtPrint.Columns.Add("discription");
             foreach(DataGridViewRow item in dgReport.Rows)
            {
                dtPrint.Rows.Add(
                    item.Cells[1].Value.ToString(),
                     item.Cells[2].Value.ToString(),
                     item.Cells[3].Value.ToString(),
                     item.Cells[4].Value.ToString()
                    );
                stiPrint.Load(Application.StartupPath + "/" + "Report.mrt");
                stiPrint.RegData("DT", dtPrint);
                stiPrint.Show();

            }

        }

        private void cbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
