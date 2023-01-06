using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DataLayer.Repositories;
using Accounting.DataLayer.Servises;
using Accounting.DataLayer.context;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            UnitOfWork db = new UnitOfWork();
            var list = db.CustomerRepository.GetAllCustomer();
        }
    }
}
