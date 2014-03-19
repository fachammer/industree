using System;

namespace Industree.Logic.StateManager
{
    public class HealthManager
    {
        private int hitPoints;

        public int HitPoints
        {
            get { return hitPoints; }
            private set { hitPoints = value; }
        }

        public bool IsDead { get { return hitPoints == 0; } }

        public event Action Death = () => { };

        public HealthManager(int initialHitPoints)
        { 
            if (initialHitPoints <= 0)
                throw new ArgumentException("initial hit points must be positive");

            hitPoints = initialHitPoints;
        }

        public void TakeDamage(int damagePoints)
        {
            if (damagePoints < 0)
                throw new ArgumentException("damage points must not be negative");

            if (damagePoints >= hitPoints)
                Die();
            else
                hitPoints -= damagePoints;
        }

        public void Die()
        {
            if (IsDead)
                throw new InvalidOperationException("health manager is already dead and cannot die again");

            hitPoints = 0;
            Death();
        }
    }
}
