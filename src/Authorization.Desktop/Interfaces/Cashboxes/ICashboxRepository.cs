using Authorization.Desktop.Entities.Cashboxes;
using Authorization.Desktop.ViewModels.Cashboxes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Desktop.Interfaces.Cashboxes
{
    public interface ICashboxRepository:IRepository<Cashbox,CashboxViewModel>
    {
    public Task<bool> GetByIdCashboxName(string CashboxName);

    }
}
