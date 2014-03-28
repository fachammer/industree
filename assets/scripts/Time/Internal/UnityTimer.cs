using UnityEngine;
using System;
using Industree.Facade;

namespace Industree.Time.Internal
{
    public class UnityTimer : MonoBehaviour, ITimer
    {
        public float interval;

        private float passedTime;
        private IGame game;

        public float TimeSinceLastTick { get { return passedTime; } }

        public event Action<ITimer> Tick = timer => { };

        private void Awake()
        {
            game = GameFactory.GetGameInstance();
            game.GamePause += Pause;
            game.GameResume += Resume;
        }

        private void Update()
        {
            passedTime += UnityEngine.Time.deltaTime;

            if (passedTime >= interval)
            {
                try
                {
                    Tick(this);
                }
                catch (Exception e)
                {
                    Debug.LogWarning("Exception in tick procedure. Stopping timer. Exception: " + e);
                    Stop();
                }

                passedTime = 0f;
            }
        }

        private void OnDestroy()
        {
            game.GamePause -= Pause;
            game.GameResume -= Resume;
        }

        public void Pause()
        {
            enabled = false;
        }

        public void Stop()
        {
            Pause();
            Destroy(this);
        }

        public void Resume()
        {
            enabled = true;
        }

        private static GameObject GetGameObject()
        {
            return GameObject.FindGameObjectWithTag(Tags.timer);
        }
    }
}

