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
    public class LevelTransitionScreen : NavigableScreen
    {
        private const float k_TransitionSeconds = 2.5f;
        private const string k_LevelMessageText = "Level: ";
        private const string k_SecondsMessageText = " seconds";
        private const string k_StartMessageText = "start in ";
        private const int k_TopMarginBetweenMessages = 100;
        private readonly TextItem r_StartButton;
        private float m_StartTimeScreen;
        private float m_TimeCounter;

        public LevelTransitionScreen(Game i_Game)
            : base(i_Game)
        {

            float widthMargin = GraphicsDevice.Viewport.Width / 2f;
            float heightMargin = GraphicsDevice.Viewport.Height / 2f;
            int currentDifficultyLevel = PlayManager.k_DefaultDifficultyLevel;
            if (i_Game.Services.GetService(typeof(IPlayManager)) is IPlayManager playerManager)
            {
                currentDifficultyLevel = playerManager.PlayDifficultyLevel;
            }
            TextItem levelButton = new TextItem(this, k_LevelMessageText + currentDifficultyLevel, 
                new Vector2(widthMargin, heightMargin - k_TopMarginBetweenMessages), CurrentNumberOfItemsOnScreen(),
                Color.White, Color.Black, true);
            AddGameItem(levelButton);
            r_StartButton = new TextItem(this, k_StartMessageText + Math.Ceiling(m_TimeCounter) + k_SecondsMessageText,
                new Vector2(widthMargin, heightMargin), CurrentNumberOfItemsOnScreen(),
                Color.DarkGray, Color.Black, true);
            AddGameItem(r_StartButton);
            m_StartTimeScreen = 0;
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            if(m_StartTimeScreen == 0)
            {
                m_StartTimeScreen = i_GameTime.TotalGameTime.Seconds;
            }

            m_TimeCounter = k_TransitionSeconds + m_StartTimeScreen - i_GameTime.TotalGameTime.Seconds;
            r_StartButton.TextMessage = k_StartMessageText + Math.Ceiling(m_TimeCounter) + k_SecondsMessageText;
            if (m_TimeCounter <= 0)
            {
                m_ScreensManager.SetCurrentScreen(new PlayScreen(Game));
                ExitScreen();
            }
        }
    }
}
