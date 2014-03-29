using Industree.Facade;
using Industree.Logic;
using Industree.Time;

namespace Industree.Logic
{
    public class TimedCreditsChanger
    {
        private IPlayer player;
        private ValuePerInterval<int> creditsPerInterval;

        public TimedCreditsChanger(IPlayer player, ValuePerInterval<int> creditsPerInterval, ITimerFactory timerFactory)
        {
            this.player = player;
            this.creditsPerInterval = creditsPerInterval;

            timerFactory.GetTimer(creditsPerInterval.Interval).Tick += OnCreditsChangeTimerTick;
        }

        private void OnCreditsChangeTimerTick(ITimer timer)
        {
            if (creditsPerInterval.Value > 0)
                player.IncreaseCredits(creditsPerInterval.Value);
            else if (creditsPerInterval.Value < 0)
                player.DecreaseCredits(-creditsPerInterval.Value);
        }
    }
}
