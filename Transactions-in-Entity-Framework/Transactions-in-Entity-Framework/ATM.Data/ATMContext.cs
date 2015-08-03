namespace ATM.Data
{
    using System.Data.Entity;
    using Models;

    public class ATMContext : DbContext
    {
        public ATMContext()
            : base("name=ATMContext")
        {
        }

        public IDbSet<CardAccount> CardAccounts { get; set; }

        public IDbSet<TransactionsHistory> TransactionHistories { get; set; } 
    }
}