using UnityEngine;
using System.Collections;

public class Levelable : MonoBehaviour {

    public int initialLevel;
    public float[] levelUpTimes;
    public int maxLevel;

    private int level;

    public int Level {
    	get { return level; }
    }

    public delegate void LevelUpHandler(Levelable levelable);
    public event LevelUpHandler LevelUp = delegate(Levelable levelable){ };
    
	public void Start () {
		level = initialLevel;
        Timer.Instantiate(levelUpTimes[level - 1], OnLevelUpTick);
	}

    private void OnLevelUpTick(Timer timer)
    {
        if(level < maxLevel){
            level++;
            LevelUp(this);

            if(level != maxLevel)
                timer.interval = levelUpTimes[level - 1];
        }

        else {
            timer.Stop();
        }
    }
}
