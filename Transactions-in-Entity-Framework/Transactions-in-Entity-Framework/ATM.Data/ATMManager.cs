namespace ATM.Data
{
    using System;
    using System.Linq;
    using Models;

    public static class ATMManager
    {
        public static void ValidateAccountAmount(CardAccount account, decimal amount)
        {
            if (amount < 0)
            {
                throw new InvalidOperationException("Invalid operation. The amount must be a positive number.");
            }

            if (account.CardCash < amount)
            {
                throw new InvalidOperationException("Insufficient amount. Please, try again.");
            }
        }

        public static CardAccount GetAccount(ATMContext context, string cardNumber, string cardPin)
        {

            var account = context.CardAccounts.FirstOrDefault(a => a.CardNumber == cardNumber);

            if (account == null)
            {
                throw new InvalidOperationException("CardNumber does not exist!");
            }

            account = context.CardAccounts.FirstOrDefault(a => a.CardPIN == cardPin && a.CardNumber == cardNumber);

            if (account == null)
            {
                throw new InvalidOperationException("Invalid PIN!");
            }

            return account;
        } 
    }
}
