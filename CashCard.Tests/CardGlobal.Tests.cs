using CashCard.Library;
using NUnit.Framework;

namespace CashCard.Tests
{

    [TestFixture]
    public class CardGlobalTests
    {
        private CardGlobal _card;
        private CardGlobal _secondCard;

        [SetUp]
        public void Setup()
        {
            _card = CardGlobal.GetInstance;
            _card.SetPin = 1234;

            _secondCard = CardGlobal.GetInstance;
            _secondCard.SetPin = 1234;
        }

        [Test]
        public void Card_CheckPin_ExceptionThrownIfPinIsNotOk()
        {
            _card.SetPin = 1;
            Assert.That(() => _card.IsPinOk(), Throws.Exception);
        }

        [Test]
        public void Card_CheckPin_NoExceptionThrownIfPinIsOk()
        {
            Assert.That(() => _card.IsPinOk(), Throws.Nothing);
        }

        [Test]
        public void Card_Withdrawal_ExceptionThrownIfBalanceIsLessThanZero()
        {
            Assert.That(() => _card.Withdrawal(100), Throws.Exception);
        }

        [Test]
        public void Card_Deposit_BalanceShouldIncreaseByMultipleDeposits()
        {
            //[existingBalance] i really don't like this but the other tests are interferring with the balance
            //one possible solution would be to isolate each test in its own class with a teardown ? but i'm not sure this is overkill
            var existingBalance = _card.Balance;

            _card.Deposit(1);
            _card.Deposit(1);

            //assert
            Assert.That(_card.Balance - existingBalance, Is.EqualTo((2)));
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public void Card_Deposit_BalanceShouldIncreaseByTheInputAmount(int input, int expected)
        {
            //act
            var existingBalance = _card.Balance;
            _card.Deposit(input);

            //assert
            Assert.That(_card.Balance - existingBalance, Is.EqualTo(expected));
        }


        [Test]
        [TestCase(1, 9, 10)]
        [TestCase(2, 8, 10)]
        public void Card_Withdrawal_BalanceShouldDecreaseByTheInputAmount(int input, int expected, int initialBalance)
        {
            var existingBalance = _card.Balance;
            _card.Deposit(initialBalance);
            _card.Withdrawal(input);
            Assert.That(_card.Balance - existingBalance, Is.EqualTo(expected));
        }

        [Test]
        public void Card_Withdrawal_BalanceShouldDecreaseByMultipleWithdrawals()
        {
            var existingBalance = _card.Balance;
            //act
            _card.Deposit(20);
            _card.Withdrawal(10);
            _card.Withdrawal(10);

            //assert
            Assert.That(_card.Balance - existingBalance, Is.EqualTo(0));
        }


        [Test]
        public void Card_Balance_ShouldMatchBetweenCardHolders()
        {
            //assert
            Assert.That(_card.Balance, Is.EqualTo(_secondCard.Balance));
        }

        [Test]
        public void Card_Balance_ShouldMatchBetweenCardHoldersAfterDeposit()
        {
            //assert
            _card.Deposit(1);
            Assert.That(_card.Balance, Is.EqualTo(_secondCard.Balance));
        }
    }
}