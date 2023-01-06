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
using System.Data.Entity;

namespace Accounting.App
{
    public partial class frmCustomers : Form
    {
        public frmCustomers()
        {
            InitializeComponent();
        }

        private void frmCustomers_Load(object sender, EventArgs e)
        {
            Bindgrid();
        }
        void Bindgrid()
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                dgvCustomers.AutoGenerateColumns = false;
                dgvCustomers.DataSource = db.CustomerRepository.GetAllCustomer();
            }
        }

        private void btnRefreshNewCustomer_Click(object sender, EventArgs e)
        {
            Bindgrid();
        }

        private void btnAddNewCustomer_Click(object sender, EventArgs e)
        {
            frmAddOrEditCustomrs frmAdd = new frmAddOrEditCustomrs();
           if(frmAdd.ShowDialog() == DialogResult.OK)
            {
                Bindgrid();
            }
        }

        private void btnDeleteNewCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow != null)
            {
                using (UnitOfWork db = new UnitOfWork())
                {
                    string name = dgvCustomers.CurrentRow.Cells[1].Value.ToString();
                    if (RtlMessageBox.Show($" آیا از حذف {name} مطمعن هستید؟  ", "توجه", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {

                        int customerId = int.Parse(dgvCustomers.CurrentRow.Cells[0].Value.ToString());
                        db.CustomerRepository.DeleteCustomer(customerId);
                        db.save();
                        Bindgrid();
                    }
                }
            }
            else
            {
                RtlMessageBox.Show("لطفا شخصی را انتخاب کنید");
            }

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                dgvCustomers.DataSource = db.CustomerRepository.GetCustomerByFilter(txtFilter.Text);

            }
        }

        private void btnٍEditeNewCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow != null)
            {
                int customerId = int.Parse(dgvCustomers.CurrentRow.Cells[0].Value.ToString());
                frmAddOrEditCustomrs frmAddOrEdit = new frmAddOrEditCustomrs();
                frmAddOrEdit.customerId = customerId;
                if (frmAddOrEdit.ShowDialog() == DialogResult.OK)
                {
                    Bindgrid();

                }
            }
        }
    }
}
