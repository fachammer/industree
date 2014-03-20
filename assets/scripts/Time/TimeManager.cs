using Industree.Facade;

namespace Industree.Time
{
    public class TimeManager
    {
        public TimeManager(ITimer timer, IGame game)
        {
            game.GamePause += timer.Pause;
            game.GameResume += timer.Resume;
            game.GameEnd += timer.Pause;
        }
    }
}
