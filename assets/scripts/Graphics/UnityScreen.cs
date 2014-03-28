using UnityEngine;
namespace Industree.Graphics
{
    public class UnityScreen : IScreen
    {
        private static UnityScreen instance;

        private UnityScreen() { }

        public int Width
        {
            get { return Screen.width; }
        }

        public int Height
        {
            get { return Screen.height; }
        }

        public static UnityScreen GetInstance()
        {
            if (instance == null)
                instance = new UnityScreen();

            return instance;
        }
    }
}
