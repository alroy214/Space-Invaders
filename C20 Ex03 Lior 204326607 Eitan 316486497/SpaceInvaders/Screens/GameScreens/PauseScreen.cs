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
        private const string k_HeaderAsset = @"Headers\Game Paused";
        private const int k_HeaderWidthOffset = 20;
        private const int k_HeaderHeightOffset = 130;
        private const float k_HeaderScale = 0.35f;
        private const float k_BlackTintAlpha = 0.4f;
        private const float k_FadeActivationTime = 0.4f;
        private const float k_FadeDeactivationTime = 0.1f;
        private readonly ScreenHeader r_PauseMessage;
        private readonly TextItem r_ContinueMessage;

        public PauseScreen(Game i_Game) : base(i_Game)
        {
            UseGradientBackground = false;
            IsModal = true;
            IsOverlayed = true;
            BlackTintAlpha = k_BlackTintAlpha;
            float viewportWidthMargin = i_Game.Window.ClientBounds.Width / 2f;
            r_PauseMessage = new ScreenHeader(this, k_HeaderAsset, k_HeaderScale,
                viewportWidthMargin - k_HeaderWidthOffset, k_HeaderHeightOffset, true);
            r_ContinueMessage = new TextItem(this, k_continueMessage, 0, Color.LightGoldenrodYellow,
                Color.Black, Keys.R, true);
            AddGameItem(r_ContinueMessage);
            r_ContinueMessage.AddToOnClick(deactivateScreen);
            UseFadeTransition = true;
            BlendState = BlendState.NonPremultiplied;
            ActivationLength = TimeSpan.FromSeconds(k_FadeActivationTime);
            DeactivationLength = TimeSpan.FromSeconds(k_FadeDeactivationTime);
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            if (Math.Abs(TransitionPosition - 1) > 0)
            {
                r_PauseMessage.Opacity = TransitionPosition;
                r_ContinueMessage.Opacity = TransitionPosition;
            }
        }

        private void deactivateScreen(object sender, EventArgs e)
        { 
            ExitScreen();
        }
    }
}