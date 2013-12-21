using UnityEngine;
using System.Collections;

public class Levelable : MonoBehaviour {

    public int level;
    public float[] levelUpTimes;
    public int maxLevel;

    public delegate void LevelUpHandler(Levelable levelable);
    public event LevelUpHandler LevelUp;

    private Timer levelUpTimer;

	public void Start () {
        levelUpTimer = Timer.Instantiate(levelUpTimes[level - 1], OnLevelUpTick);
	}

    private void OnLevelUpTick()
    {
        if(level < maxLevel){
            level++;
            LevelUp(this);

            if(level != maxLevel)
                levelUpTimer.interval = levelUpTimes[level - 1];
        }

        else {
            levelUpTimer.Stop();
        }
    }
}
