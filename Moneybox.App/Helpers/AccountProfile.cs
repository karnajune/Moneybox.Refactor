using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneybox.App.Helpers
{
   public class AccountProfile:Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, Account>();
        }
    }
}
