using System;
using C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure.ObjectModel.Screens.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens
{
    public class PauseScreen : NavigableScreen
    {
        private const string k_ContinueMessage = "Click the R Key to continue playing";
        private const string k_HeaderAsset = @"Headers\Game Paused";
        private const float k_HeaderScale = 0.35f;
        private const float k_BlackTintAlpha = 0.4f;
        private const float k_FadeActivationTime = 0.4f;
        private const float k_FadeDeactivationTime = 0.1f;
        private readonly TextItem r_ContinueMessage;

        public PauseScreen(Game i_Game) : base(i_Game)
        {
            UseGradientBackground = false;
            IsModal = true;
            IsOverlayed = true;
            BlackTintAlpha = k_BlackTintAlpha;
            SetScreenHeader(k_HeaderAsset, k_HeaderScale);
            r_ContinueMessage = new TextItem(this, k_ContinueMessage, 
                0, Color.LightGoldenrodYellow,
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
                m_ScreenHeader.Opacity = TransitionPosition;
                r_ContinueMessage.Opacity = TransitionPosition;
            }
        }

        private void deactivateScreen(object sender, EventArgs e)
        { 
            ExitScreen();
        }
    }
}