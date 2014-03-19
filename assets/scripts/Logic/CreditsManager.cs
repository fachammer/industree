using System;

public class CreditsManager
{
    private int credits;

    public int Credits {
        get { return credits; }
        private set{
            int oldValue = Credits;
            credits = value;
            CreditsChange(oldValue, value);
        }
    }

    public event Action<int, int> CreditsChange = (oldCredits, newCredits) => { };

    public CreditsManager(int initialCreditsAmount)
    {
        if (initialCreditsAmount < 0)
            throw new ArgumentException("initial credits must not be negative");

        credits = initialCreditsAmount;
    }

    public void IncreaseCreditsByAmount(int amount)
    {
        if (amount < 0)
            throw new ArgumentException("increase credits amount must not be negative");

        Credits += amount;
    }

    public void DecreaseCreditsByAmount(int amount)
    {
        if (amount < 0)
            throw new ArgumentException("decrease credits amount must not be negative");
        if (amount > Credits)
            throw new ArgumentException("decrease credits amount must not be greater than the current credits amount");

        Credits -= amount;
    }
}
