using System;

namespace CashCard.Library
{
    public class CardGlobal : ICash

    {
        private const int SecurityPin = 1234;
        private double _balance;

        private static readonly CardGlobal Instance = new CardGlobal();

        public static CardGlobal GetInstance
        {
            get { return Instance; }
        }

        public double Balance
        {
            get
            {
                if (IsPinOkFunction())
                    return _balance;

                return 0;
            }
        }

        public int Pin { get; set; }


        public void IsPinOk()
        {
            if (Pin != SecurityPin)
                throw new Exception("SecurityPin Invalid");
        }

        public bool IsPinOkFunction()
        {
            if (Pin != SecurityPin)
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
            if (IsPinOkFunction())
                _balance = Balance + amount;
        }

        public void Withdrawal(double amount)
        {
            if (IsPinOkFunction())
                if (_balance < amount)
                    throw new Exception("Not enough Cash in account");

            _balance = Balance - amount;
        }


    }
}