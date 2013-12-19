using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    public float interval;

    public delegate void TickHandler();
    public event TickHandler Tick;

    private float timer;
    private bool running; 

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            Tick();
        }

        if (Tick.GetInvocationList().Length == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Pause()
    {
        enabled = false;
    }

    public void Stop()
    {
        Pause();
        Destroy(this.gameObject);
    }

    public void Resume()
    {
        enabled = true;
    }

    public static Timer Instantiate(float interval, TickHandler onTick)
    {
        GameObject timerTemplate = GameObject.FindGameObjectWithTag(Tags.timer);
        Timer timer = ((GameObject)Instantiate(timerTemplate)).GetComponent<Timer>();
        timer.enabled = true;
        timer.interval = interval;
        timer.Tick += onTick;
        timer.running = true;
        return timer;
    }
}
