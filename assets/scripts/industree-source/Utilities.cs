using System;
using UnityEngine;


public static class Utilities
{
	public static Transform getMostOuterAncestor(Transform transform){
	
		Transform ancestor = transform.parent;
		
		if(ancestor == null){
			return transform;
		}
		
		while(ancestor != null){
			ancestor = ancestor.parent;
		}
		
		return ancestor;
	}

    public static float GetAxisRawDown(string axis, float previousValue, out float rawAxisValue)
    {
        float inputValue = Input.GetAxisRaw(axis);
        rawAxisValue = inputValue;

        if (inputValue != 0 && previousValue == 0)
        {
            return inputValue;
        }

        return 0;
    }
}

