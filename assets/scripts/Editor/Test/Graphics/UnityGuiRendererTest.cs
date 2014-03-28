using NUnit.Framework;
using NSubstitute;
using Industree.Graphics.Internal;
using UnityEngine;
using System;

namespace Industree.Graphics.Test
{
    public class UnityGuiRendererTest
    {
        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void WhenDrawTextureIsCalledWithNullTextureThenNullReferenceExceptionIsThrown()
        {   
            new UnityGuiRenderer().DrawTexture(null, new Rect());
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void WhenDrawTextIsCalledWithNullStringThenNullReferenceExceptionIsThrown()
        {
            new UnityGuiRenderer().DrawText(null, new Rect(), new GUIStyle());
        }
    }
}
