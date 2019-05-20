using CashCard.Library;
using NUnit.Framework;

namespace CashCard.Tests
{

    [TestFixture]
    public class CashGlobalTests
    {
        private CardGlobal _cashCard;

        [SetUp]
        public void Setup()
        {
            _cashCard = CardGlobal.GetInstance;
            _cashCard.Pin = 1234;
        }


        [Test]
        public void Card_CheckPin_ExceptionThrownIfPinIsNotOk()
        {
            var cashCard = CardGlobal.GetInstance;
            cashCard.Pin = 1;
            Assert.That(() => cashCard.IsPinOk(), Throws.Exception);
        }

        [Test]
        public void Card_CheckPin_NoExceptionThrownIfPinIsOk()
        {
            var cashCard = CardGlobal.GetInstance;
            cashCard.Pin = 1234;
            Assert.That(() => cashCard.IsPinOk(), Throws.Nothing);
        }

        [Test]
        public void Card_Deposit_BalanceShouldIncreaseByMultipleInputs()
        {
            var cashCard = _cashCard;
            var existingBalance = cashCard.Balance;

            cashCard.Deposit(1);
            cashCard.Deposit(1);

            //assert
            Assert.That(cashCard.Balance, Is.EqualTo((2 - existingBalance)));
        }


        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public void Card_Deposit_BalanceShouldIncreaseByTheInputAmount(int input, int expected)
        {
            //act
            var cashCard = _cashCard;
            var existingBalance = cashCard.Balance;
            cashCard.Deposit(input);

            //assert
            Assert.That(cashCard.Balance-existingBalance, Is.EqualTo(expected));
        }




        [Test]
        [TestCase(1, 9, 10)]
        [TestCase(2, 8, 10)]
        public void Card_Withdrawal_BalanceShouldDecreaseByTheInputAmount(int input, int expected, int initialBalance)
        {
            var cashCard = _cashCard;
            var existingBalance = cashCard.Balance;
            cashCard.Deposit(initialBalance);
            cashCard.Withdrawal(input);
            Assert.That(cashCard.Balance - existingBalance, Is.EqualTo(expected));
        }

        [Test]
        public void Card_Withdrawal_BalanceShouldDecreaseByMultipleInputs()
        {
            var cashCard = _cashCard;
            var existingBalance = cashCard.Balance;
            //act
            cashCard.Deposit(20);
            cashCard.Withdrawal(10);
            cashCard.Withdrawal(10);

            //assert
            Assert.That(cashCard.Balance - existingBalance, Is.EqualTo(0));
        }



        [Test]
        public void Card_Withdrawal_ExceptionThrownIfBalanceIsLessThanZero()
        {
            var cashCard = _cashCard;
            Assert.That(() => cashCard.Withdrawal(100), Throws.Exception);
        }


    }
}