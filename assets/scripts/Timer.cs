using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    public float interval;

    private float timer;
    private GameController gameController;

    public delegate void TickHandler();
    public event TickHandler Tick = delegate() {};

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
            Tick();
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
