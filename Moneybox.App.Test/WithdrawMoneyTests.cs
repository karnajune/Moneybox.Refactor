using Moneybox.App.DataAccess;
using Moneybox.App.Features;
using Moneybox.App.Helpers;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace Moneybox.App.Test
{
   public class WithdrawMoneyTests
    {
        private readonly Account _fromAccount;
        private readonly Mock<IAccountRepository> _accountRepositoryStub;
        private readonly new Mock<ITransferHelperService> _actionFromBalanceStub;
        public WithdrawMoneyTests()
        {
            _fromAccount = new Account()
            {
                Id= Guid.NewGuid(),
                Balance = 100,
                PaidIn = 10,
                User = new User(),
                Withdrawn = 15
            };
            _accountRepositoryStub = new Mock<IAccountRepository>();
            _actionFromBalanceStub = new Mock<ITransferHelperService>();
        }
        [Fact]
        public void TakeActionOnFromBalance_BalanceBelowZero_ReturnException()
        {      
            _accountRepositoryStub.Setup(x => x.GetAccountById(It.IsAny<Guid>()))
                .Returns(_fromAccount);

            _actionFromBalanceStub.Setup(x => x.TakeActionOnFromBalance(It.IsAny<decimal>(), It.IsAny<string>()))
            .Throws<InvalidOperationException>();

            var tanferMoneyObject = new WithdrawMoney(_accountRepositoryStub.Object, _actionFromBalanceStub.Object);
            try
            {
                tanferMoneyObject.Execute(_fromAccount.Id, 101);
            }
            catch (Exception ex)
            {
                Assert.IsType<InvalidOperationException>(ex);
            }
        }
        [Fact]
        public void Execute_OnSuccessWithdrawal_MatchesBalanceandWithdrawal()
        {
            var amountToWithdraw = 100;            
            var accountDetailsAfterWithdraw = TestData.GetAccounts().Where(x => x.Id == Guid.Parse("2bbb9005-d008-4068-95d4-bf9de60af834")).FirstOrDefault();

            _accountRepositoryStub.Setup(x => x.GetAccountById(It.IsAny<Guid>()))
                .Returns(accountDetailsAfterWithdraw);

            var tanferMoneyObject = new WithdrawMoney(_accountRepositoryStub.Object, _actionFromBalanceStub.Object);

            tanferMoneyObject.Execute(accountDetailsAfterWithdraw.Id, amountToWithdraw);

            var accountDetailsActual = TestData.GetAccounts().Where(x => x.Id == Guid.Parse("2bbb9005-d008-4068-95d4-bf9de60af834")).FirstOrDefault();
            _accountRepositoryStub.Setup(x => x.GetAccountById(It.IsAny<Guid>()))
               .Returns(accountDetailsActual);

            //check if balance is deducted
            Assert.Equal(accountDetailsActual.Balance - amountToWithdraw, accountDetailsAfterWithdraw.Balance );

            // check if withdrawn amount is incremented
            Assert.Equal(accountDetailsActual.Withdrawn + amountToWithdraw, accountDetailsAfterWithdraw.Withdrawn );

        }
    }
}
