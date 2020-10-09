using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure.ObjectModel.Screens.Items;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens
{
    public class PauseScreen : NavigableScreen
    {
        private string k_continueMessage = "Click the R Key to continue playing";
        private const string k_HeaderAsset = @"Headers\Welcome";
        private const int k_HeaderWidthOffset = 40;
        private const int k_HeaderHeightOffset = 60;
        private const float k_HeaderScale = 0.35f;
        private const float k_BlackTintAlpha = 0.4f;

        public PauseScreen(Game i_Game) : base(i_Game)
        {
            UseGradientBackground = false;
            IsModal = true;
            IsOverlayed = true;
            BlackTintAlpha = k_BlackTintAlpha;
            float viewportWidthMargin = i_Game.Window.ClientBounds.Width / 2f;
            ScreenHeader pauseMessage = new ScreenHeader(this, k_HeaderAsset, k_HeaderScale,
                viewportWidthMargin - k_HeaderWidthOffset, k_HeaderHeightOffset, true);
            TextItem continueMessage = new TextItem(this, k_continueMessage, 0, Color.DodgerBlue,
                Color.AntiqueWhite, Keys.R, true);
            AddGameItem(continueMessage);
            continueMessage.AddToOnClick(deactivateScreen);
        }

        private void deactivateScreen(object sender, EventArgs e)
        {
            Deactivate();
        }
    }
}