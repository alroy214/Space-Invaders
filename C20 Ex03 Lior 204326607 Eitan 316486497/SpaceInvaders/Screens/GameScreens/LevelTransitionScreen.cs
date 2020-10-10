using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities;
using C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure.ObjectModel.Screens.Items;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens
{
    public class LevelTransitionScreen : GameScreen
    {
        private const float k_TransitionSeconds = 2.5f;
        private const string k_LevelMessageText = "Level: ";
        private const string k_SecondsMessageText = " seconds";
        private const string k_SecondMessageText = " second";
        private const string k_StartMessageText = "Start in ";
        private const int k_TopMarginBetweenMessages = 100;
        private readonly Background r_Background;
        private readonly TextItem r_StartButton;
        private readonly TextItem r_LevelButton;
        private float m_StartTimeScreen;
        private float m_TimeCounter;

        public LevelTransitionScreen(Game i_Game)
            : base(i_Game)
        {
            r_Background = new Background(this);
            int currentDifficultyLevel = PlayManager.k_DefaultDifficultyLevel;
            if (i_Game.Services.GetService(typeof(IPlayManager)) is IPlayManager playerManager)
            {
                currentDifficultyLevel = playerManager.PlayDifficultyLevel;
            }

            m_TimeCounter = k_TransitionSeconds;
            r_LevelButton = new TextItem(this, k_LevelMessageText + currentDifficultyLevel,
                Color.White, Color.Black, true);
            r_StartButton = new TextItem(this, Math.Ceiling(m_TimeCounter) + k_SecondsMessageText, 
                Color.DarkGray, Color.Black, true, k_StartMessageText);
            m_StartTimeScreen = 0;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            float widthMargin = GraphicsDevice.Viewport.Width / 2f;
            float heightMargin = GraphicsDevice.Viewport.Height / 2f;
            r_LevelButton.Position = new Vector2(widthMargin - r_LevelButton.Width / 2, heightMargin - k_TopMarginBetweenMessages);
            r_StartButton.Position = new Vector2(widthMargin - r_StartButton.Width / 2, heightMargin);
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            m_TimeCounter -= (float) i_GameTime.ElapsedGameTime.TotalSeconds;
            if (m_TimeCounter <= 0)
            {
                ExitScreen();
                m_ScreensManager.SetCurrentScreen(new PlayScreen(Game));
            }
            else
            {
                r_StartButton.TextMessage = (float)Math.Ceiling(m_TimeCounter) + (m_TimeCounter > 1 ? k_SecondsMessageText : k_SecondMessageText);
            }
        }
    }
}
