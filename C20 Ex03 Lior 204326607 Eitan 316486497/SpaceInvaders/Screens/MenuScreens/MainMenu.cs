using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure.ObjectModel.Screens.Items;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens.MenuScreens
{
    class MainMenu : MenuScreen
    {
        private const string k_HeaderAssetName = @"Headers\Main Menu";
        private const float k_HeaderScale = 0.3f;
        private readonly IPlayManager r_PlayManager;
        private const string k_ScreenSettingsMessage = "Screen Settings";
        private const string k_NumberOfPlayersMessage = "Player: ";
        private const string k_SoundSettingsMessage = "Sound Settings";
        private const string k_PlayGameMessage = "Play Game";
        private const string k_QuitGameMessage = "Quit Game";


        public MainMenu(Game i_Game) : base(i_Game)
        {
            r_PlayManager = (IPlayManager) i_Game.Services.GetService(typeof(IPlayManager));
            SetScreenHeader(k_HeaderAssetName, k_HeaderScale);
            AddOptionItem(k_ScreenSettingsMessage, Color.SeaGreen, screenSettings_OnClicked);
            AddOptionItem(k_NumberOfPlayersMessage + r_PlayManager.NumberOfPlayers, Color.CornflowerBlue, player_OnClicked, true);
            AddOptionItem(k_SoundSettingsMessage, Color.RosyBrown, soundSettings_OnClicked);
            AddOptionItem(k_PlayGameMessage, Color.LightSeaGreen, playGame_OnClicked);
            AddOptionItem(k_QuitGameMessage, Color.PaleVioletRed, quitGame_OnClicked);

        }

        private void screenSettings_OnClicked(object sender, EventArgs e)
        {
            m_ScreensManager.SetCurrentScreen(new ScreenSettings(Game));
       //     Clear();
        }

        private void player_OnClicked(object sender, EventArgs e)
        {
            ((TextItem)sender).TextMessage = k_NumberOfPlayersMessage + r_PlayManager.ToggleNumberOfPlayers();
        }

        private void soundSettings_OnClicked(object sender, EventArgs e)
        {
            m_ScreensManager.SetCurrentScreen(new SoundSettings(Game));
          //  Clear();
        }

        private void playGame_OnClicked(object sender, EventArgs e)
        {
            m_ScreensManager.SetCurrentScreen(new LevelTransitionScreen(Game));
            ExitScreen();
        }

        private void quitGame_OnClicked(object sender, EventArgs e)
        {
            Game.Exit();
        }
    }
}
