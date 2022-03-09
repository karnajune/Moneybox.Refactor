using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneybox.App.Helpers
{
   public static class TestData
    {
        public static IEnumerable<Account> GetAccounts()
        {
            var accounts = new List<Account>()
            {
                new Account
                {
                    Id=Guid.Parse("2bbb9005-d008-4068-95d4-bf9de60af834"),
                    Balance=1230,
                    PaidIn=100,
                    User=new User
                    {
                        Id=Guid.Parse("8a61ddec-9158-41af-b953-4ed09ff5c979"),
                        Name="TestUser1",
                        Email="TestUser1@testmail.com"
                    },
                    Withdrawn=100
                },
                new Account
                {
                    Id=Guid.Parse("0e554952-2e33-47cf-8c2f-ce62ab643add"),
                    Balance=3330,
                    PaidIn=1000,
                    User=new User
                    {
                        Id=Guid.Parse("2049cebc-124d-4057-b649-fe82b9001521"),
                        Name="TestUser2",
                        Email="TestUser2@testmail.com"
                    },
                    Withdrawn=700
                },
                new Account
                {
                    Id=Guid.Parse("3b274e26-2616-4bfd-ae98-2e585427059c"),
                    Balance=2530,
                    PaidIn=300,
                    User=new User
                    {
                        Id=Guid.Parse("c8c73a57-53c6-478f-b0d6-a1e7fa78f3de"),
                        Name="TestUser3",
                        Email="TestUser3@testmail.com"
                    },
                    Withdrawn=500
                },
            };

            return accounts;
        }
    }
}
