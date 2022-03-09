using AutoMapper;
using Moneybox.App.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneybox.App.DataAccess
{
    public class AccountRepository : IAccountRepository
    {
        private IEnumerable<Account> _testAccounts { get; set; }
        private IMapper _mapper { get; set; }
        public AccountRepository(IMapper mapper)
        {
            _testAccounts = TestData.GetAccounts();
            _mapper = mapper;            
        }
        public Account GetAccountById(Guid accountId)
        {
            var result = new Account();
            result = _testAccounts.Where(x => x.Id == accountId).FirstOrDefault();
            return result;
        }

        public void Update(Account account)
        {
            var currentAccount = _testAccounts.Where(c => c.Id == account.Id);
            _mapper.Map(account, currentAccount);
        }
    }
}
