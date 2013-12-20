using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    public float interval;

    public delegate void TickHandler();
    public event TickHandler Tick = delegate() {};

    private float timer;
    private TimeManager timeManager;

    void Start(){
        timeManager = GameObject.FindGameObjectWithTag(Tags.timeManager).GetComponent<TimeManager>();
        timeManager.Pause += Pause;
        timeManager.Resume += Resume;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            Tick();
            timer = 0f;
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
        GameObject timerTemplate = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>().timer;
        Timer timer = ((GameObject)Instantiate(timerTemplate)).GetComponent<Timer>();
        timer.enabled = true;
        timer.interval = interval;
        timer.Tick += onTick;
        return timer;
    }
}
