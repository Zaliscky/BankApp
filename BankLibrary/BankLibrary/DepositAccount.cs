using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    class DepositAccount : Account
    {
        public DepositAccount(decimal sum,int percentage) : base(sum,percentage)
        {        }
        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs("You have opened new deposit ! Account Id: " + this._id, this._sum));

        }

        public override void Put(decimal sum)
        {
            if (_days % 30 == 0)
                base.Put(sum);
            else
                base.OnAdded(new AccountEventArgs("You can put only after 30day period ", 0));
        }

        public override decimal Withdraw(decimal sum)
        {
            if (_days % 30 == 0)
                return base.Withdraw(sum);
            else
                base.OnWithdrawed(new AccountEventArgs("You can withdraw money after 30day period ", 0));
            return 0;
        }

        protected internal override void Calculate()
        {
            if (_days % 30 == 0)
                base.Calculate();
        }
    }
}
