using UnityEngine;
namespace Industree.Graphics
{
    public static class GraphicsUtility
    {

        public static Texture2D CreateTextureWithColor(Color color)
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(1, 1, color);
            texture.Apply();
            return texture;
        }
    }
}
