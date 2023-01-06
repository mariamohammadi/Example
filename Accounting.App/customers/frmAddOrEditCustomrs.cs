using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidationComponents;
using Accounting.DataLayer;
using Accounting.DataLayer.context;
using System.Data.Entity;

namespace Accounting.App
{
    public partial class frmAddOrEditCustomrs : Form
    {
        public int customerId = 0;
        public frmAddOrEditCustomrs()
        {
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            if (openfile.ShowDialog()==DialogResult.OK)
            {
                pcCustomer.ImageLocation = openfile.FileName;

            }
        }

        private void frmAddOrEditCustomrs_Load(object sender, EventArgs e)
        {
            if (customerId != 0)
            {
                using (UnitOfWork db = new UnitOfWork()) {
                    this.Text = "ویرایش شخص";
                    btnSave.Text = "ویرایش";
                    var customer = db.CustomerRepository.GetCustomerById(customerId);
                    txtEmail.Text = customer.Email;
                    txtAddress.Text = customer.Address;
                    txtMobile.Text = customer.Mobile;
                    txtName.Text = customer.FullName;
                    pcCustomer.ImageLocation = Application.StartupPath + "/images/" + customer.CustomerImage;
            }
            } }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(pcCustomer.ImageLocation);
                string path = Application.StartupPath + "/Images/";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                pcCustomer.Image.Save(path + imageName);
             
                using (UnitOfWork db = new UnitOfWork())
                {
                    Customers customers = new Customers()
                    {
                    Address = txtAddress.Text,
                        Email = txtEmail.Text,
                        FullName = txtName.Text,
                        Mobile = txtMobile.Text,
                        CustomerImage = imageName

                    };
                    if (customerId == 0)
                    {
                        db.CustomerRepository.InsertCustomer(customers);
                    }
                    else
                    {
                        customers.CustomerId = customerId;
                        db.CustomerRepository.UpdateCustomer(customers);
                    }
                   
                    db.save();
                    DialogResult = DialogResult.OK;
                }
              
            }

        }
    }
}
