using System;
using System.Windows.Forms.VisualStyles;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities;
using C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure.ObjectModel.Screens.Items;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens.MenuScreens;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens
{
    public class WelcomeScreen : NavigableScreen
    {
        private const string k_HeaderAssetName = @"Headers\Welcome2";
        private const float k_HeaderScale = 0.5f;
        private const string k_StartMessage = "Press Enter Key to start the game";
        private const string k_ExitMessage = "Press Esc Key to exit the game";
        private const string k_MenuMessage = "Press the M Key to show the settings";
        private readonly Background r_Background;
        private TextItem startButton;
        private TextItem exitButton;
        private TextItem menuButton;

        public WelcomeScreen(Game i_Game) : base(i_Game)
        {
            r_Background = new Background(this);
            SetScreenHeader(k_HeaderAssetName, k_HeaderScale);
            startButton = new TextItem(this, k_StartMessage, NumberOfItemsOnScreen(), Color.LightSeaGreen, Keys.Enter);
            startButton.AddToOnClick(startButton_OnClicked);
            AddGameItem(startButton);
            exitButton = new TextItem(this, k_ExitMessage, NumberOfItemsOnScreen(), Color.PaleVioletRed, Keys.Escape);
            AddGameItem(exitButton);
            exitButton.AddToOnClick(exitButton_OnClicked);
            menuButton = new TextItem(this, k_MenuMessage, NumberOfItemsOnScreen(), Color.DodgerBlue, Keys.M);
            menuButton.AddToOnClick(menuButton_OnClicked);
            AddGameItem(menuButton);

            ActivationLength = TimeSpan.FromMilliseconds(500);
            DeactivationLength = TimeSpan.FromMilliseconds(300);
            BlendState = BlendState.NonPremultiplied;
            UseFadeTransition = false;
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);

            if (Math.Abs(TransitionPosition - 1) > 0)
            {
                r_Background.Opacity = TransitionPosition;
                m_ScreenHeader.Opacity = TransitionPosition;
            }
        }

        private void startButton_OnClicked(object sender, EventArgs e)
        {
            m_ScreensManager.SetCurrentScreen(new LevelTransitionScreen(Game));
        }

        private void exitButton_OnClicked(object sender, EventArgs e)
        {
            Game.Exit();
        }

        private void menuButton_OnClicked(object sender, EventArgs e)
        {
            m_ScreensManager.SetCurrentScreen(new MainMenu(Game));
        }
    }
}
