using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens
{
    public class PauseScreen : GameScreen
    {
        private ScreenHeader m_PauseMsg;

        public PauseScreen(Game i_Game) : base(i_Game)
        {
            UseGradientBackground = false;
            IsModal = true;
            IsOverlayed = true;
            BlackTintAlpha = 0.4f;
            m_PauseMsg = new ScreenHeader(this, @"Headers\Welcome", 0);
            m_PauseMsg.OffsetY = 250;
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);

            if (InputManager.KeyPressed(Keys.R))
            { 
                ExitScreen();
            }
        }
    }
}
