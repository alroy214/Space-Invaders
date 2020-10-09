using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure;
using C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure.ObjectModel.Screens.Items;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens.MenuScreens
{
    class SoundSettings : MenuScreen
    {
        private const string k_HeaderAssetName = @"Headers\Sound Settings";
        private const float k_HeaderScale = 0.25f;
        private const string k_ToggleSoundMessage = "ToggleSound: ";
        private const string k_BackgroundMusicMessage = "Background Music Volume: ";
        private const string k_SoundEffectsMessage = "Sound Effects Volume: ";
        private const string k_DoneMessage = "Done";
        private readonly ISoundManager r_SoundManager;

        public SoundSettings(Game i_Game) : base(i_Game)
        {
            SetScreenHeader(k_HeaderAssetName, k_HeaderScale);
            r_SoundManager = (ISoundManager)i_Game.Services.GetService(typeof(ISoundManager));
            AddOptionItem(k_ToggleSoundMessage, r_SoundManager.SoundToggle, Color.MediumSeaGreen, soundToggle_OnClicked);
            AddOptionItem(k_BackgroundMusicMessage, Color.CornflowerBlue, backgroundMusicMessage_OnClicked, true);
            AddOptionItem(k_SoundEffectsMessage, Color.Bisque, soundEffects_OnClicked, true);
            AddOptionItem(k_DoneMessage, Color.PaleVioletRed, done_OnClicked);
        }

        private void soundToggle_OnClicked(object sender, EventArgs e)
        {
            r_SoundManager.SoundToggle = !r_SoundManager.SoundToggle;
            ((TextItem)sender).TextMessage = GetDefaultToggleMessage(r_SoundManager.SoundToggle);
        }
        
        private void backgroundMusicMessage_OnClicked(object sender, EventArgs e)
        {
            ((TextItem)sender).TextMessage = k_BackgroundMusicMessage;
        }

        private void soundEffects_OnClicked(object sender, EventArgs e)
        {
            ((TextItem)sender).TextMessage = k_SoundEffectsMessage;
        }

        private void done_OnClicked(object sender, EventArgs e)
        {
            ExitScreen();
        }
    }
}
