using Industree.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Industree.Logic.Test
{
    public class TestTimer : ITimer
    {
        private bool paused;
        private bool running;

        public event Action<ITimer> Tick;

        public TestTimer()
        {
            paused = false;
            running = true;
        }

        public void SimulateOneTick()
        {
            if(running && !paused)
                Tick(this);
        }

        public void Pause()
        {
            paused = true;
        }

        public void Resume()
        {
            paused = false;
        }

        public void Stop()
        {
            running = false;
        }
    }
}
