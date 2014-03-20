using NUnit.Framework;
using NSubstitute;
using Industree.Graphics;
using Industree.Data.View;
using UnityEngine;
using System;
using Industree.Facade;

namespace Industree.View.Test
{
    public class SelectedActionViewTest
    {
        [Test]
        public void WhenDrawIsCalledThenGuiRendererRecievesCallToDrawTexture()
        {
            IAction fakeSelectedAction = Substitute.For<IAction>();
            fakeSelectedAction.IconBounds.Returns(new Rect(0, 0, 1, 1));
            ITexture fakeTexture = Substitute.For<ITexture>();
            fakeSelectedAction.SelectedOverlayIcon.Returns(fakeTexture);

            IPlayer player = Substitute.For<IPlayer>();
            player.SelectedAction.Returns(fakeSelectedAction);

            IGuiRenderer gui = Substitute.For<IGuiRenderer>();

            SelectedActionView selectedActionView = new SelectedActionView(player, gui);


            selectedActionView.Draw();


            gui.Received().DrawTexture(fakeTexture, new Rect(0, 0, 1, 1));
        }
    }
}
