using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Helpers
{
    public class TransferHelperService : ITransferHelperService
    {
        private INotificationService notificationService;

        public TransferHelperService(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        public void TakeActionOnFromBalance(decimal fromBalance, string email)
        {
            if (fromBalance < 0m)
            {
                throw new InvalidOperationException("Insufficient funds to make transfer");
            }
            else if (fromBalance < 500m)
            {
                this.notificationService.NotifyFundsLow(email);
            }
        }
        public void TakeActionOnToBalance(decimal paidIn, decimal payinLimit,string email)
        {            
            if (paidIn > payinLimit)
            {
                throw new InvalidOperationException("Account pay in limit reached");
            }
            else if (payinLimit - paidIn < 500m)
            {
                this.notificationService.NotifyApproachingPayInLimit(email);
            }
        }
    }
}
