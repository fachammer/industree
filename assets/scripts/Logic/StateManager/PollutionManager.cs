using System;
namespace Industree.Logic.StateManager
{
    public class PollutionManager
    {
        private int currentPollution = 0;
        private int maximumPollution;

        public event Action MaximumPollutionReached = () => { };
        public event Action ZeroPollutionReached = () => { };

        public PollutionManager(int maximumPollution)
        {
            if (maximumPollution <= 0)
                throw new ArgumentException("maximum pollution must not be zero or negative");

            this.maximumPollution = maximumPollution;
        }

        public PollutionManager(int maximumPollution, int currentPollution)
            : this(maximumPollution)
        {
            if (currentPollution < 0)
                throw new ArgumentException("current pollution must not be negative");
            else if (currentPollution > maximumPollution)
                throw new ArgumentException("current pollution must not be greater than maximum pollution");

            this.currentPollution = currentPollution;
        }

        public int CurrentPollution { get { return currentPollution; } }

        public int MaximumPollution { get { return maximumPollution; } }

        public void IncreasePollutionByAmount(int pollutionAmount)
        {
            if (pollutionAmount < 0)
                throw new ArgumentException("pollution amount must not be negative");

            currentPollution += pollutionAmount;

            if (currentPollution > maximumPollution)
                currentPollution = maximumPollution;

            if (currentPollution == maximumPollution)
                MaximumPollutionReached();
        }

        public void DecreasePollutionByAmount(int pollutionAmount)
        {
            if (pollutionAmount < 0)
                throw new ArgumentException("pollution amount must not be negative");

            currentPollution -= pollutionAmount;

            if (currentPollution < 0)
                currentPollution = 0;

            if (currentPollution == 0)
                ZeroPollutionReached();
        }
    }
}
