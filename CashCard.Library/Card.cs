using System;

namespace CashCard.Library
{
    public class Card : ICash

    {
        private const int Pin = 1234;
        private double _balance;
        private readonly int _pin;

        public Card(int pin)
        {
            _pin = pin;
        }

        public double Balance
        {
            get
            {
                IsPinOk();
                return _balance;
            }
        }


        public void Deposit(double amount)
        {
            IsPinOk();
            _balance = Balance + amount;
        }

        public void Withdrawal(double amount)
        {
            IsPinOk();
            if (_balance < amount)
                throw new Exception("Not enough Cash in account");

            _balance = Balance - amount;
        }

        public void IsPinOk()
        {
            if (_pin != Pin)
                throw new Exception("Pin Invalid");
        }

    }
}
