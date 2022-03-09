using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using Moneybox.App.Helpers;
using System;

namespace Moneybox.App.Features
{
    public class WithdrawMoney
    {
        private IAccountRepository accountRepository;
        private ITransferHelperService transferHelperService;


        public WithdrawMoney(IAccountRepository accountRepository, ITransferHelperService transferHelperService)
        {
            this.accountRepository = accountRepository;
            this.transferHelperService = transferHelperService;
        }

        public void Execute(Guid fromAccountId, decimal amount)
        {
            var from = this.accountRepository.GetAccountById(fromAccountId);
            if(from == null)
            {
                //return custom exception not found.
            }
            var fromBalance = from.Balance - amount;
            this.transferHelperService.TakeActionOnFromBalance(fromBalance, from.User.Email);
            from.Balance = fromBalance;
            from.Withdrawn += amount;

            this.accountRepository.Update(from);

        }
    }
}
