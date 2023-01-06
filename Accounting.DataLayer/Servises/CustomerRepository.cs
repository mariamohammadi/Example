using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DataLayer.Repositories;
using Accounting.ViewModels.customers;

namespace Accounting.DataLayer.Servises
{
   public class CustomerRepository : ICustomerRepository
    {
      private  Accounting_DBEntities db;

        public  CustomerRepository(Accounting_DBEntities context)
        {
            db = context;
        }

        public bool DeleteCustomer(int customerId)
        {
            var customer = GetCustomerById(customerId);
            DeleteCustomer(customer);
            return true;
        }

        public bool DeleteCustomer(Customers customer)
        {
            try
            {
                db.Entry(customer).State = EntityState.Deleted;
                return true;

            }
            catch
            {
                return false;

            }
        }

        public List<Customers> GetAllCustomer()
        {
            return db.Customers.ToList();
        }

        public IEnumerable<Customers> GetCustomerByFilter(string parameter)
        {
            return db.Customers.Where(c => c.FullName.Contains(parameter) || c.Email.Contains(parameter) || c.Mobile.Contains(parameter)).ToList();
        }

        public Customers GetCustomerById(int CustomerId)
        {
            return db.Customers.Find(CustomerId);
        }

        public int GetCustomerIdByName(string name)
        {
            return db.Customers.First(c => c.FullName == name).CustomerId;
        }

        public string GetCustomerNameByid(int customerId)
        {
            return db.Customers.Find(customerId).FullName;
        }

        public List<ListCustomerViewModel> GetNameCustomers(string filter = "")
        {
            if (filter == "")
            {
                return db.Customers.Select(c => new ListCustomerViewModel()
                {
                    CustomerId = c.CustomerId,
                    FullName=c.FullName
                
                }
                ).ToList();

            }
            return db.Customers.Where(c => c.FullName.Contains(filter)).Select(c => new ListCustomerViewModel()
            { 
                CustomerId=c.CustomerId,
                FullName=c.FullName
                }
            ).ToList();
        }

        public bool InsertCustomer(Customers customer)
        {
            try
            {
                db.Customers.Add(customer);
                return true;

            }
            catch
            {
                return false;

            }
        }

       

        public bool UpdateCustomer(Customers customer)
        {
            try
            {
                db.Entry(customer).State = EntityState.Modified;
                return true;

            }
            catch
            {
                return false;

            }
        }
    }
}
