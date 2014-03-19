using System;
using UnityEngine;

namespace Industree.Miscellaneous
{
    public delegate void NoArgsEventHandler(object sender);

    public static class Utilities
    {
        public delegate void NoArgsEventHandler(object sender);

        public static Transform GetMostOuterAncestor(Transform transform)
        {

            Transform ancestor = transform.parent;

            if (ancestor == null)
            {
                return transform;
            }

            while (ancestor.parent != null)
            {
                ancestor = ancestor.parent;
            }

            return ancestor;
        }

        public static float GetAxisRawDown(string axis, float previousValue, out float rawAxisValue)
        {
            float inputValue = UnityEngine.Input.GetAxisRaw(axis);
            rawAxisValue = inputValue;

            if (inputValue != 0 && previousValue == 0)
            {
                return inputValue;
            }

            return 0;
        }

        public static Texture2D MakeTexture2DWithColor(Color color)
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(1, 1, color);
            texture.Apply();

            return texture;
        }

        public static void DrawScreenCenteredTexture(Texture2D texture)
        {
            GUI.DrawTexture(new Rect(
                (Screen.width - texture.width) / 2,
                (Screen.height - texture.height) / 2,
                texture.width,
                texture.height),
                texture);
        }

        public static bool RandomBool()
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
                return true;
            return false;
        }
    }
}

