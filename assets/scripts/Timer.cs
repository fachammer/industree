using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    public float interval;

    public delegate void TickHandler();
    public event TickHandler Tick;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            Tick();
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
        return timer;
    }
}
