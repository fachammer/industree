using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    public float interval;

    private float timer;
    private TimeManager timeManager;

    public delegate void TickHandler();
    public event TickHandler Tick = delegate() {};

    private void Awake(){
        timeManager = GameObject.FindGameObjectWithTag(Tags.timeManager).GetComponent<TimeManager>();
        timeManager.Pause += Pause;
        timeManager.Resume += Resume;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            Tick();
            timer = 0f;
        }
    }

    private void OnDestroy(){
        timeManager.Pause -= Pause;
        timeManager.Resume -= Resume;
    }

    public void Pause()
    {
        enabled = false;
    }

    public void Stop()
    {
        Pause();
        Destroy(gameObject);
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
