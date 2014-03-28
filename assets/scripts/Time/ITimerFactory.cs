using Industree.Time.Internal;

namespace Industree.Time
{
    public interface ITimerFactory
    {
        ITimer GetTimer(float interval);
    }
}
