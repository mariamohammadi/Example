using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DataLayer.Repositories;
using Accounting.DataLayer.Servises;

namespace Accounting.DataLayer.context
{
  public  class UnitOfWork:IDisposable
    {
        Accounting_DBEntities db = new Accounting_DBEntities();
        private ICustomerRepository _CustomerRepository;
        public ICustomerRepository CustomerRepository { get
            {
                if (_CustomerRepository == null)
                {
                    _CustomerRepository = new CustomerRepository(db);
                  
                }
                return _CustomerRepository;
                
            } }
        private GenericRepository<Accounting> _accountingRepository;
        public GenericRepository<Accounting> AccountingRepository { get
            {
                if(_accountingRepository==null)
                {
                    _accountingRepository = new GenericRepository<Accounting>(db);
                }
                return _accountingRepository;
            } }
        private GenericRepository<Login> _loginRepository;
        public GenericRepository<Login> LoginRepository { get
            {
                if (_loginRepository == null)
                {
                    _loginRepository = new GenericRepository<Login>(db);
                }
            
                    return _loginRepository;
                
            } }
      public void save()
        {
            db.SaveChanges();

        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
