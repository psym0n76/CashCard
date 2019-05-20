using System;

namespace CashCard.Library
{
    public class CardGlobal : ICash

    {
        private const int SecurityPin = 1234;
        private double _balance;

        public static CardGlobal GetInstance { get; } = new CardGlobal();

        public int SetPin { get; set; }

        public bool IsPinOk()
        {
            if (SetPin != SecurityPin)
            {
                throw new Exception("SecurityPin Invalid");
            }
            else
            {
                return true;
            }
        }

        public void Deposit(double amount)
        {
            if (IsPinOk())
                _balance = Balance + amount;
        }

        public void Withdrawal(double amount)
        {
            if (IsPinOk())
                if (_balance < amount)
                    throw new Exception("Not enough Cash in account");

            _balance = Balance - amount;
        }

        public double Balance
        {
            get
            {
                if (IsPinOk())
                    return _balance;

                return 0;
            }
        }
    }
}