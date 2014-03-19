using System;

namespace Industree.Logic
{
    public class LevelManager
    {
        public int Level { get; private set; }

        public event System.Action<int, int> LevelUp = (oldLevel, newLevel) => { };

        public LevelManager(int initialLevel)
        {
            if (initialLevel < 0)
            {
                throw new ArgumentException("initial level must not be negative");
            }
            Level = initialLevel;
        }

        public void RaiseLevel()
        {
            int oldLevel = Level;
            int newLevel = ++Level;
            LevelUp(oldLevel, newLevel);
        }
    }
}
