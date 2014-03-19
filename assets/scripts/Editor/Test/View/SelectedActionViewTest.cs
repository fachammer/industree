﻿using NUnit.Framework;
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
            fakeSelectedAction.IconBounds.Returns(new Rect());
            IPlayer player = Substitute.For<IPlayer>();
            player.SelectedAction.Returns(fakeSelectedAction);

            ISelectedActionViewData data = Substitute.For<ISelectedActionViewData>();
            ITexture fakeTexture = Substitute.For<ITexture>();
            data.IconOverlay.Returns(fakeTexture);

            IGuiRenderer gui = Substitute.For<IGuiRenderer>();

            SelectedActionView selectedActionView = new SelectedActionView(player, data, gui);

            selectedActionView.Draw();

            gui.Received().DrawTexture(fakeTexture, fakeSelectedAction.IconBounds);
        }
    }
}