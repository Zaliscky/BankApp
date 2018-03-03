using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    interface IAccount
    {
        void Put(decimal sum); // put on account
        decimal Withdraw(decimal sum); // take from account
    }
}
