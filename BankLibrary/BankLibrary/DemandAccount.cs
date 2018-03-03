using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    class DemandAccount : Account
    {
        public DemandAccount(decimal sum, int percentage) : base(sum, percentage)
        {
        }

        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs("New account was opened before demand, Id of the account: " + this._id, this._sum));
        }
    }
}
