using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    public float interval;

    private float timer;
    private GameController gameController;

    public float TimeSinceLastTick {
        get { return timer; }
    }

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
            Tick(this);
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

    public static Timer AddTimer(GameObject gameObject, float interval, TickHandler onTick)
    {
        Timer timer = (Timer) gameObject.AddComponent(typeof(Timer));
        timer.interval = interval;
        timer.Tick += onTick;
        return timer;
    }
}
