using UnityEngine;
using System.Collections;

public class Levelable : MonoBehaviour {

    public int level;
    public float[] levelUpTimes;
    public int maxLevel;

    public delegate void LevelUpHandler(Levelable levelable);
    public event LevelUpHandler LevelUp;

    private Timer levelUpTimer;

	// Use this for initialization
	void Start () {
        levelUpTimer = Timer.Instantiate(levelUpTimes[0], OnLevelUpTick);
        LevelUp += OnLevelUp;
	}

    void OnLevelUp(Levelable levelable)
    {
        levelable.level++;

        if (levelable.level >= maxLevel)
        {
            levelUpTimer.Stop();
        }
    }

    void OnLevelUpTick()
    {
        LevelUp(this);
        levelUpTimer.interval = levelUpTimes[level];
    }
}
