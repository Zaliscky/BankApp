using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLibrary;


namespace BankApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank<Account> bank = new Bank<Account>("SlavaUkraineBank");
            bool alive = true;
            while (alive)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green; 
                Console.WriteLine("1. Open an account \n 2. Display cash  \n 3. Add to the account\n");
                Console.WriteLine("4. Close the account \n 5. Skip the day \n 6. Exit");
                Console.WriteLine("Input number of unit:");
                Console.ForegroundColor = color;
                try
                {
                    int command = Convert.ToInt32(Console.ReadLine());

                    switch (command)
                    {
                        case 1:
                            OpenAccount(bank);
                            break;
                        case 2:
                            Withdraw(bank);
                            break;
                        case 3:
                            Put(bank);
                            break;
                        case 4:
                            CloseAccount(bank);
                            break;
                        case 5:
                            break;
                        case 6:
                            alive = false;
                            continue;
                    }
                    bank.CalculatePercentage();
                }
                catch (Exception ex)
                {
                    ///Exception message
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = color;
                }
            }
        }

        private static void OpenAccount(Bank<Account> bank)
        {
            Console.WriteLine("Point sum for creating account:");

            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Choose type of account: 1. demand 2. deposit");
            AccountType accountType;

            int type = Convert.ToInt32(Console.ReadLine());

            if (type == 2)
                accountType = AccountType.Deposit;
            else
                accountType = AccountType.Ordinary;

            bank.Open(accountType,
                sum,
                AddSumHandler,  //hadler for adding cash  on the account
                WithdrawSumHandler, 
                (o, e) => Console.WriteLine(e.Message), // handler for counting procents
                CloseAccountHandler, //handler for closing
                OpenAccountHandler); 
        }

        private static void Withdraw(Bank<Account> bank)
        {
            Console.WriteLine("Choose some to withdraw:");

            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Input id of the account:");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Withdraw(sum, id);
        }

        private static void Put(Bank<Account> bank)
        {
            Console.WriteLine("input sum to put onto account:");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Input id of the account:");
            int id = Convert.ToInt32(Console.ReadLine());
            bank.Put(sum, id);
        }

        private static void CloseAccount(Bank<Account> bank)
        {
            Console.WriteLine("Input id of the account, which must be closed:");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Close(id);
        }
        
        private static void OpenAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
     
        private static void AddSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
  
        private static void WithdrawSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
            if (e.Sum > 0)
                Console.WriteLine("Go to spen money");
        }

        private static void CloseAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

