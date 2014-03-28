using Industree.Time.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Industree.Time.Internal
{
    internal class UnityTimerFactory : ITimerFactory
    {
        public ITimer GetTimer(float interval)
        {
            UnityTimer timer = GameObject.FindGameObjectWithTag(Tags.timer).AddComponent<UnityTimer>();
            timer.interval = interval;
            return timer;
        }
    }
}
