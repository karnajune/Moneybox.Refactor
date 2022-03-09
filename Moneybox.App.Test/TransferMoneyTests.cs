using Moneybox.App.DataAccess;
using Moneybox.App.Features;
using Moneybox.App.Helpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Moneybox.App.Test
{
  public  class TransferMoneyTests
    {
        private Account _fromAccount;
        private Account _toAccount;
        public TransferMoneyTests()
        {
            _fromAccount = new Account()
            {
                Id = Guid.NewGuid(),
                Balance = 100,
                PaidIn = 10,
                User = new User(),
                Withdrawn = 15
            };
            _toAccount = new Account()
            {
                Id = Guid.NewGuid(),
                Balance = 400,
                PaidIn = 30,
                User = new User(),
                Withdrawn = 25
            };
        }
        [Fact]
        public void TakeActionOnFromBalance_BalanceBelowZero_ReturnException()
        {           
            var accountRepositoryStub = new Mock<IAccountRepository>();
            var actionFromBalanceStub = new Mock<ITransferHelperService>();

            accountRepositoryStub.Setup(x => x.GetAccountById(It.IsAny<Guid>()))
                .Returns(_fromAccount);

            actionFromBalanceStub.Setup(x => x.TakeActionOnFromBalance(It.IsAny<decimal>(), It.IsAny<string>()))
            .Throws<InvalidOperationException>();

            var tanferMoneyObject = new TransferMoney(accountRepositoryStub.Object, actionFromBalanceStub.Object);
            try
            {
                tanferMoneyObject.Execute(_fromAccount.Id, _toAccount.Id, -1);
            }
            catch(Exception ex)
            {
                Assert.IsType<InvalidOperationException>(ex);
            }
        }
    }
}
