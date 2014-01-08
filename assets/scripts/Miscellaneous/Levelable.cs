using UnityEngine;
using System.Collections;

public class Levelable : MonoBehaviour {

    public int initialLevel;
    public int maxLevel;

    private int level;

    public int Level {
    	get { return level; }
    }

    public delegate void LevelUpHandler(Levelable levelable);
    public event LevelUpHandler LeveledUp = delegate(Levelable levelable){ };
    
	public void Start () {
		level = initialLevel;
	}

    public void LevelUp()
    {
        if (level < maxLevel)
        {
            level++;
            LeveledUp(this);
        }
    }
}
