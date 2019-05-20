using CashCard.Library;
using NUnit.Framework;

namespace CashCard.Tests
{

    [TestFixture]
    public class CashTests
    {
        private Card _cashCard;

        [SetUp]
        public void Setup()
        {
            _cashCard = new Card(1234);
        }


        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public void Card_Deposit_BalanceShouldIncreaseByTheInputAmount(int input, int expected)
        {
            //act
            var cashCard = new Card(1234);
            _cashCard.Deposit(input);

            //assert
            Assert.That(_cashCard.Balance, Is.EqualTo(expected));
        }

        [Test]
        public void Card_Deposit_BalanceShouldIncreaseByMultipleInputs()
        {
            var cashCard = new Card(1234);
            cashCard.Deposit(10);
            cashCard.Deposit(10);

            //assert
            Assert.That(cashCard.Balance, Is.EqualTo(20));
        }


        [Test]
        [TestCase(1, 9, 10)]
        [TestCase(2, 8, 10)]
        public void Card_Withdrawal_BalanceShouldDecreaseByTheInputAmount(int input, int expected, int initialBalance)
        {
            var cashCard = new Card(1234);
            cashCard.Deposit(initialBalance);
            cashCard.Withdrawal(input);
            Assert.That(cashCard.Balance, Is.EqualTo(expected));
        }

        [Test]
        public void Card_Withdrawal_BalanceShouldDecreaseByMultipleInputs()
        {
            var cashCard = new Card(1234);
            //act
            cashCard.Deposit(20);
            cashCard.Withdrawal(10);
            cashCard.Withdrawal(10);

            //assert
            Assert.That(cashCard.Balance, Is.EqualTo(0));
        }



        [Test]
        public void Card_Withdrawal_ExceptionThrownIfBalanceIsLessThanZero()
        {
            var cashCard = new Card(1234);
            Assert.That(() => cashCard.Withdrawal(10), Throws.Exception);
        }

        [Test]
        public void Card_CheckPin_ExceptionThrownIfPinIsNotOk()
        {
            var cashCard = new Card(1);
            Assert.That(() => cashCard.IsPinOk(), Throws.Exception);
        }

        [Test]
        public void Card_CheckPin_NoExceptionThrownIfPinIsOk()
        {
            var cashCard = new Card(1234);
            Assert.That(() => cashCard.IsPinOk(), Throws.Nothing);
        }
    }
}
