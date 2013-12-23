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

    public static Texture2D MakeTexture2DWithColor(Color color){
		Texture2D texture = new Texture2D(1, 1);
		texture.SetPixel(1, 1, color);
		texture.Apply();

		return texture;
	}
}

