namespace ATM.ConsoleClient
{
    using System;
    using System.Data.Entity;
    using Data;
    using Data.Migrations;

    public class ATMMain
    {
        public static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ATMContext, Configuration>());
            var context = new ATMContext();
            
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    Console.Write("Please, enter card number: ");
                    var cardNumber = Console.ReadLine();

                    Console.Write("Please, enter PIN: ");
                    var pinCode = Console.ReadLine();

                    var account = ATMManager.GetAccount(context, cardNumber, pinCode);

                    Console.Write("Please, enter amount: ");
                    var amount = decimal.Parse(Console.ReadLine());

                    ATMManager.ValidateAccountAmount(account, amount);
                    account.CardCash -= amount;

                    var transaction = new TransactionsHistory()
                    {
                        CardNumber = cardNumber,
                        Amount = amount,
                        TransactionDate = DateTime.Now
                    };

                    context.TransactionHistories.Add(transaction);
                    context.SaveChanges();
                    dbContextTransaction.Commit();

                    Console.WriteLine("Withdraw complete.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    dbContextTransaction.Rollback();
                }
            }
        }
    }
}
