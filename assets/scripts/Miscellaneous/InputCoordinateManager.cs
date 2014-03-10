using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace assets.scripts.Miscellaneous
{
    public static class InputCoordinateManager
    {
        public static Vector2[] GetInputCoordinates()
        {
            if (Input.touchCount > 0)
            {
                return Input.touches.Select<Touch, Vector2>((touch) => touch.position).ToArray<Vector2>();
            }

            return new Vector2[] { new Vector2(Input.mousePosition.x, Screen.width - Input.mousePosition.y) };
        }
    }
}
