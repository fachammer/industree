using System;

namespace Industree.Time
{
    public interface IClock
    {
        void CallbackOnce(float timeIntervalInSeconds, Action tickCallback);
        void RemoveCallback(Action tickCallback);
        void ClearCallbacks();
    }
}
