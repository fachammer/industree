using Industree.Time.Internal;

namespace Industree.Time
{
    public static class Timing
    {
        public static ITimerFactory GetTimerFactory()
        {
            return new UnityTimerFactory();
        }
    }
}
