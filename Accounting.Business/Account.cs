using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DataLayer.context;
using Accounting.ViewModels.accounting;

namespace Accounting.Business
{
    public class Account
    {
        public static ReportViewModel ReportForMain()
        {
            ReportViewModel rp = new ReportViewModel();
            using(UnitOfWork db=new UnitOfWork())
            {
                DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01);
                DateTime endtDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 30);
                var recive = db.AccountingRepository.Get(a => a.TypeID == 1 && a.DateTitle >= startDate && a.DateTitle <= endtDate).Select(a => a.Amount).ToList();
                var pay = db.AccountingRepository.Get(a => a.TypeID == 2 && a.DateTitle >= startDate && a.DateTitle <= endtDate).Select(a => a.Amount).ToList();
                rp.Recive = recive.Sum();
                rp.Pay = pay.Sum();
                rp.AccountBalance = (recive.Sum() - pay.Sum());

            }
            return rp;
        }
    }
}
