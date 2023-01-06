using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.ViewModels.customers;

namespace Accounting.DataLayer.Repositories
{
   public interface ICustomerRepository
    {
        List<Customers> GetAllCustomer();
        Customers GetCustomerById(int CustomerId);
        IEnumerable<Customers> GetCustomerByFilter(string parameter);
        List<ListCustomerViewModel> GetNameCustomers(string filter = "");
        bool InsertCustomer(Customers customer);
        bool UpdateCustomer(Customers customer);
        bool DeleteCustomer(Customers customer);
        bool DeleteCustomer(int customerId);
        int GetCustomerIdByName(string name);
        string GetCustomerNameByid(int customerId);

    }
}
