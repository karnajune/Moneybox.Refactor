using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneybox.App.Helpers
{
   public interface ITransferHelperService
    {
        void TakeActionOnFromBalance(decimal fromBalance, string email);
        void TakeActionOnToBalance(decimal paidIn, decimal payinLimit, string email);

    }
}
