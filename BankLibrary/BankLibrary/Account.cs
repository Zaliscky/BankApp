using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public abstract class Account : IAccount
    {
        //event for displaying money
        protected internal event AccountStateHandler Withdrawed;
        protected internal event AccountStateHandler Added;
        protected internal event AccountStateHandler Opened;
        //event for clossing account
        protected internal event AccountStateHandler Closed;
        //event for adding percentages
        protected internal event AccountStateHandler Calculated;

        protected int _id;
        static int counter = 0;

        protected decimal _sum;
        protected int _percentage; // storing percenteage
        protected int _days = 0; // period from openning account

        public Account(decimal sum, int perc)
        {
            _sum = sum;
            _percentage = perc;
            _id = ++counter;
        }

       
        public decimal CurrentSum 
        {
            get { return _sum; }
        }

        public int Percentage { get { return _percentage; } }
        public int Id { get { return _id; } }

        //calling of the events
        private void CallEvent(AccountEventArgs e, AccountStateHandler handler)
        {
            if (handler != null && e != null)
                handler(this, e);
        }
        // calling of each event individually, transferring event as parameter
        protected virtual void OnOpened(AccountEventArgs e) { CallEvent(e, Opened); }
        protected virtual void OnWithdrawed(AccountEventArgs e) { CallEvent(e, Withdrawed); }
        protected virtual void OnAdded(AccountEventArgs e) { CallEvent(e, Added); }
        protected virtual void OnClosed(AccountEventArgs e) { CallEvent(e, Closed); }
        protected virtual void OnCalculated(AccountEventArgs e) { CallEvent(e, Calculated); }

        public virtual void Put(decimal sum)
        {
            _sum += sum;
            OnAdded(new AccountEventArgs("Sum of deposit" + sum, sum));
        }
        public virtual decimal Withdraw(decimal sum)
        {
            decimal result = 0;
            if (sum <= _sum)
            {
                _sum = sum;
                result = sum;
                OnWithdrawed(new AccountEventArgs(" Sum " + sum + " Money took from account " + _id, sum));
            }
            else
            {
                OnWithdrawed(new AccountEventArgs(" Not enough money on your account " + _id, 0));
            }
            return result;
        }

        protected internal virtual void Open()
        {
            OnOpened(new AccountEventArgs(" You have new deposit account !Id account: " + this._id, this._sum));
        }
        protected internal virtual void Close()
        {
            OnClosed(new AccountEventArgs("Account " + _id + " closed.  Result sum: " + CurrentSum, CurrentSum));

        }

        protected internal void IncrementDays() { _days++; }
        protected internal virtual void Calculate()
        {
            decimal increment = _sum * _percentage / 100;
            _sum = _sum + increment;
            OnCalculated(new AccountEventArgs(" Percentages Added : " + increment, increment));
        }

    }
}
