using System;

namespace Industree.Time
{
    public class Clock : IClock
    {
        public void CallbackOnce(float timeIntervalInSeconds, Action tickCallback)
        {
            throw new NotImplementedException();
        }

        public void RemoveCallback(Action tickCallback)
        {
            throw new NotImplementedException();
        }

        public void ClearCallbacks()
        {
            throw new NotImplementedException();
        }
    }
}
