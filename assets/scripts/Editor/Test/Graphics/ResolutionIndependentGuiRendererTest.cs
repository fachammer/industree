using NUnit.Framework;
using NSubstitute;
using UnityEngine;

namespace Industree.Graphics.Internal.Test
{
    public class ResolutionIndependentGuiRendererTest
    {
        [Test]
        public void WhenDrawTextureIsCalledThenGuiRendererRecievesCallWithProperlyScaledRectangle()
        {
            IScreen screen = Substitute.For<IScreen>();
            screen.Width.Returns(100);
            screen.Height.Returns(100);
            IGuiRenderer innerRenderer = Substitute.For<IGuiRenderer>();
            ResolutionIndependentGuiRenderer renderer = new ResolutionIndependentGuiRenderer(screen, innerRenderer);
            Rect inputBounds = new Rect(0, 0, 1, 1);
            Rect expectedOutputBounds = new Rect(0, 0, screen.Width, screen.Height);
            Texture fakeTexture = Substitute.For<Texture>();

            renderer.DrawTexture(fakeTexture, inputBounds);

            innerRenderer.Received().DrawTexture(fakeTexture, expectedOutputBounds);
        }

        [Test]
        public void WhenDrawTextureWithDepthIsCalledThenGuiRendererRecievesCallWithProperlyScaledRectangle()
        {
            IScreen screen = Substitute.For<IScreen>();
            screen.Width.Returns(100);
            screen.Height.Returns(100);
            IGuiRenderer innerRenderer = Substitute.For<IGuiRenderer>();
            ResolutionIndependentGuiRenderer renderer = new ResolutionIndependentGuiRenderer(screen, innerRenderer);
            Rect inputBounds = new Rect(0, 0, 1, 1);
            Rect expectedOutputBounds = new Rect(0, 0, screen.Width, screen.Height);
            Texture fakeTexture = Substitute.For<Texture>();

            renderer.DrawTexture(fakeTexture, inputBounds, 0);

            innerRenderer.Received().DrawTexture(fakeTexture, expectedOutputBounds, 0);
        }

        [Test]
        public void WhenDrawTextIsCalledThenGuiRendererRecievesCallWithProperlyScaledFontInGuiStyle()
        {
            IScreen screen = Substitute.For<IScreen>();
            screen.Width.Returns(100);
            screen.Height.Returns(100);
            IGuiRenderer innerRenderer = Substitute.For<IGuiRenderer>();
            ResolutionIndependentGuiRenderer renderer = new ResolutionIndependentGuiRenderer(screen, innerRenderer);
            GUIStyle inputGuiStyle = new GUIStyle();
            inputGuiStyle.fontSize = 100;
            int expectedOutputFontSize = 75;
            Rect inputBounds = new Rect(0, 0, 1, 1);
            Rect expectedOutputBounds = new Rect(0, 0, 100, 100);

            innerRenderer.WhenForAnyArgs(r => r.DrawText("", new Rect(), new GUIStyle())).Do(callInfo => {
                Assert.AreEqual("", callInfo.Arg<string>());
                Assert.AreEqual(expectedOutputBounds, callInfo.Arg<Rect>());
                Assert.AreEqual(expectedOutputFontSize, callInfo.Arg<GUIStyle>().fontSize);
                Assert.Pass();
            });

            renderer.DrawText("", inputBounds, inputGuiStyle);

            Assert.Fail();
        }
    }
}
