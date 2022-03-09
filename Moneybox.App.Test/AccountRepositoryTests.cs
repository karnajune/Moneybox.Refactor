using AutoMapper;
using Moneybox.App.DataAccess;
using Moq;
using System;
using Xunit;

namespace Moneybox.App.Test
{
    public class AccountRepositoryTests
    {
        [Fact]
        public void GetAccount_InvalidAccountId_ReturnNull()
        {
            var mapperMock = new Mock<IMapper>();
            var notInTheListAccountId = Guid.Parse("e4e67706-5c9d-496c-8014-048c20287752");
            var accountRepository = new AccountRepository(mapperMock.Object);

            var result = accountRepository.GetAccountById(notInTheListAccountId);

            Assert.Null(result);

        }  
        
        [Fact]
        public void GetAccount_ValidAccountId_ReturnCorrectAccount()
        {
            var mapperMock = new Mock<IMapper>();
            var knownAccountId = Guid.Parse("2bbb9005-d008-4068-95d4-bf9de60af834");
            var accountRepository = new AccountRepository(mapperMock.Object);

            var result = accountRepository.GetAccountById(knownAccountId);

            Assert.Equal(result.Id, knownAccountId);

        }
    }
}
