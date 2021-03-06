﻿using System;

namespace CashCard.Library
{
    public class Card : ICash

    {
        // Implementation that works in one location
        private const int SecurityPin = 1234;
        private double _balance;
        private readonly int _pin;
        private static readonly object Locker = new object();

        public Card(int pin)
        {
            _pin = pin;
        }

        public bool IsPinOk()
        {
            if (_pin != SecurityPin)
            {
                throw new Exception("Security Pin Invalid");
            }
            else
            {
                return true;
            }
        }

        public void Deposit(double amount)
        {

            lock (Locker)
            {
                IsPinOk();
                _balance = Balance + amount;
            }
        }

        public void Withdrawal(double amount)
        {
            lock (Locker)
            {
                IsPinOk();
                if (_balance < amount)
                    throw new Exception("Not enough Cash in account");

                _balance = Balance - amount;
            }
        }

        public double Balance
        {
            get
            {
                IsPinOk();
                return _balance;
            }
        }
    }
}
