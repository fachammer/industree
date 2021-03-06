﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Industree.Time
{
    public interface ITimer
    {
        event Action<ITimer> Tick;

        float TimeSinceLastTick { get; }

        void Pause();
        void Resume();
        void Stop();
    }
}
