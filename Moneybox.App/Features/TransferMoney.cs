using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using Moneybox.App.Helpers;
using System;

namespace Moneybox.App.Features
{
    public class TransferMoney
    {
        private IAccountRepository accountRepository;
        private ITransferHelperService transferHelperService;

        public TransferMoney(IAccountRepository accountRepository, ITransferHelperService transferHelperService)
        {
            this.accountRepository = accountRepository;
            this.transferHelperService = transferHelperService;
        }

        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var from = this.accountRepository.GetAccountById(fromAccountId);
            var to = this.accountRepository.GetAccountById(toAccountId);

            var fromBalance = from.Balance - amount;
            this.transferHelperService.TakeActionOnFromBalance(fromBalance, from.User.Email);

            var paidIn = to.PaidIn + amount;
          
            this.transferHelperService.TakeActionOnToBalance(paidIn, Account.PayInLimit, to.User.Email);

            from.Balance = fromBalance;
            from.Withdrawn = from.Withdrawn - amount;

            to.Balance = to.Balance + amount;
            to.PaidIn = to.PaidIn + amount;

            this.accountRepository.Update(from);
            this.accountRepository.Update(to);
        }
    }
}
