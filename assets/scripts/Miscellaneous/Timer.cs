using UnityEngine;
using System.Collections;
using System;

public class Timer : MonoBehaviour {

    public float interval;

    private float timer;
    private GameController gameController;

    public float TimeSinceLastTick { get { return timer; } }

    public delegate void TickHandler(Timer timer);
    public event TickHandler Tick = delegate(Timer timer) {};

    private void Awake(){
        gameController = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>();
        gameController.GamePause += Pause;
        gameController.GameResume += Resume;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            try
            {
                Tick(this);
            }
            catch(Exception e)
            {
                Debug.LogWarning("Exception in tick procedure. Stopping timer. Exception: " + e);
                Stop();
            }

            timer = 0f;
        }
    }

    private void OnDestroy(){
        gameController.GamePause -= Pause;
        gameController.GameResume -= Resume;
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

    public static Timer Start(float interval, TickHandler onTick)
    {
        Timer timer = (Timer) Timer.GetGameObject().AddComponent(typeof(Timer));
        timer.interval = interval;
        timer.Tick += onTick;
        return timer;
    }

    private static GameObject GetGameObject()
    {
        return GameObject.FindGameObjectWithTag(Tags.timer);
    }
}
