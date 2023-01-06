using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accounting.DataLayer;
using Accounting.DataLayer.context;
using ValidationComponents;

namespace Accounting.App
{
    public partial class frmNewtransaction : Form
    {
        public int AccountID = 0;
        public frmNewtransaction()
        {
            InitializeComponent();
        }

        private void frmNewtransaction_Load(object sender, EventArgs e)
        {
            dgvCustomers.AutoGenerateColumns = false;
            using (UnitOfWork db = new UnitOfWork())
            {
                dgvCustomers.DataSource = db.CustomerRepository.GetNameCustomers();
                if (AccountID != 0)
                {
                    var account = db.AccountingRepository.GetById(AccountID);
                    txtDescription.Text = account.Discription;
                    txtName.Text = db.CustomerRepository.GetCustomerNameByid(account.CustomerId);
                    txtAmount.Text = account.Amount.ToString();
                    if (account.TypeID == 1)
                    {
                        rbRecive.Checked = true;
                    }
                    else
                    {
                        rbPay.Checked = true;
                    }
                    this.Text = "ویرایش";
                    btnSave.Text= "ویرایش";
                }
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                dgvCustomers.AutoGenerateColumns = false;
                dgvCustomers.DataSource = db.CustomerRepository.GetNameCustomers(txtFilter.Text);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dgvCustomers.CurrentRow.Cells[0].Value.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                if (rbPay.Checked || rbRecive.Checked)
                {
                    using (UnitOfWork db = new UnitOfWork())
                    {

                        DataLayer.Accounting accounting = new DataLayer.Accounting()
                        {
                            Amount = int.Parse(txtAmount.Value.ToString()),
                            CustomerId = db.CustomerRepository.GetCustomerIdByName(txtName.Text),
                            TypeID = (rbRecive.Checked) ? 1 : 2,
                            DateTitle = DateTime.Now,
                            Discription = txtDescription.Text,

                        };
                        if (AccountID == 0)
                        {
                            db.AccountingRepository.Insert(accounting);
                            db.save();
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {

                            accounting.ID = AccountID;
                            db.AccountingRepository.Update(accounting);
                        }
                        db.save();
                        DialogResult = DialogResult.OK;
                    }
                    
                }
                else
                {
                    RtlMessageBox.Show("لطفا نوع تراکنش را انتخاب کنید");
                }

            }
        }
    }
}
