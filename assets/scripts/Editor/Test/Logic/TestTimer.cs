using Industree.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Industree.Logic.Test
{
    public class TestTimer : ITimer
    {
        public void SimulateOneTick()
        {
            Tick(this);
        }

        public event Action<ITimer> Tick;
    }
}
