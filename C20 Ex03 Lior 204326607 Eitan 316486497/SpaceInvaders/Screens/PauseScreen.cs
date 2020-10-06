using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens
{
    public class PauseScreen : GameScreen
    {
        
        private ScreenHeader m_PauseMsg; //TODO FIX

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
        }/*
        SpriteFont m_FontCalibri;
        Vector2 m_MsgPosition = new Vector2(70, 300);

        public PauseScreen(Game i_Game)
            : base(i_Game)
        {
            this.IsModal = true;
            this.IsOverlayed = true;
            this.UseGradientBackground = true;
            this.BlackTintAlpha = 0.65f;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            SpriteBatch.Begin();
            SpriteBatch.DrawString(
                m_FontCalibri,
                @"
[ Instructions ]
Use the arrows to move the walking square.
Try to avoid reaching thr right of the screen..

R - Resume Game",
                m_MsgPosition,
                Color.White);

            SpriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(InputManager.KeyPressed(Keys.R))
            {
                this.ExitScreen();
            }
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            m_FontCalibri = ContentManager.Load<SpriteFont>(@"Fonts\Consolas");
        }*/
    }
}