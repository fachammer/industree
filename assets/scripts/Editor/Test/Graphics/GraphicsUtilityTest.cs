using NUnit.Framework;
using NSubstitute;
using UnityEngine;

namespace Industree.Graphics.Test
{
    public class GraphicsUtilityTest
    {
        [Test]
        public void WhenCreateTextureWithColorIsCalledThenTextureWithOnePixelHeightAndWidthIsReturnedWhichHasTheGivenColor()
        {
            Color color = new Color(1f, 1f, 1f, 1f);
            Texture2D texture = GraphicsUtility.CreateTextureWithColor(color);

            Assert.AreEqual(color, texture.GetPixel(0, 0));
        }

        [Test]
        public void WhenCreateTextureWithColorIsCalledThenTextureWidthIsOne()
        {
            Texture2D texture = GraphicsUtility.CreateTextureWithColor(new Color(1f, 1f, 1f, 1f));

            Assert.AreEqual(1, texture.width);
        }

        [Test]
        public void WhenCreateTextureWithColorIsCalledThenTextureHeightIsOne()
        {
            Texture2D texture = GraphicsUtility.CreateTextureWithColor(new Color(1f, 1f, 1f, 1f));

            Assert.AreEqual(1, texture.height);
        }
    }
}
