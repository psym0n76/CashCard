namespace CashCard.Library
{
    public interface ICash
    {
        void Withdrawal(double amount);
        void Deposit(double amount);
        bool IsPinOk();
    }
}
