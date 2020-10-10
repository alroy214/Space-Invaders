using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure;
using C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure.ObjectModel.Screens.Items;
using Infrastructure.Managers;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens.MenuScreens
{
    class SoundSettings : MenuScreen
    {
        private const string k_HeaderAssetName = @"Headers\Sound Settings";
        private const float k_HeaderScale = 0.25f;
        private const string k_ToggleSoundMessage = "Toggle Sound: ";
        private const string k_BackgroundMusicMessage = "Background Music Volume: ";
        private const string k_SoundEffectsMessage = "Sound Effects Volume: ";
        private const string k_DoneMessage = "Done";
        private readonly ISoundManager r_SoundManager;
        private readonly IInputManager r_InputManager; // for asking what KeyPressed
        private const int k_SoundsVolumeChange = 10;

        public SoundSettings(Game i_Game) : base(i_Game)
        {
            SetScreenHeader(k_HeaderAssetName, k_HeaderScale);
            r_SoundManager = (ISoundManager)i_Game.Services.GetService(typeof(ISoundManager));
            r_InputManager = i_Game.Services.GetService(typeof(IInputManager)) as IInputManager;
            AddOptionItem(k_ToggleSoundMessage + "Off", Color.MediumSeaGreen, soundToggle_OnClicked, true);
            AddOptionItem(k_BackgroundMusicMessage + r_SoundManager.GetBackgroundMusicVolumePercentage(), Color.CornflowerBlue, backgroundMusicMessage_OnClicked, true);
            AddOptionItem(k_SoundEffectsMessage + r_SoundManager.GetSoundEffectsVolumePercentage(), Color.Bisque, soundEffects_OnClicked, true);
            AddOptionItem(k_DoneMessage, Color.PaleVioletRed, done_OnClicked);
        }

        private void soundToggle_OnClicked(object sender, EventArgs e)
        {
            r_SoundManager.SoundToggle = !r_SoundManager.SoundToggle;
            ((TextItem)sender).TextMessage = k_ToggleSoundMessage + (r_SoundManager.SoundToggle ? "On" : "Off");
        }
        
        private void backgroundMusicMessage_OnClicked(object sender, EventArgs e)
        {
            int raiseOrLowerVolumeFlag = getRaiseOrLowerVolumeFlag();

            this.r_SoundManager.ChangeBackgroundMusicVolumeLevel((float) k_SoundsVolumeChange * raiseOrLowerVolumeFlag);
            ((TextItem)sender).TextMessage = k_BackgroundMusicMessage + r_SoundManager.GetBackgroundMusicVolumePercentage();
        }

        private void soundEffects_OnClicked(object sender, EventArgs e)
        {
            int raiseOrLowerVolumeFlag = getRaiseOrLowerVolumeFlag();

            this.r_SoundManager.ChangeSoundEffectsVolumeLevel((float) k_SoundsVolumeChange * raiseOrLowerVolumeFlag);
            ((TextItem)sender).TextMessage = k_SoundEffectsMessage + r_SoundManager.GetSoundEffectsVolumePercentage();
        }

        private int getRaiseOrLowerVolumeFlag()
        {
            int raiseOrLowerVolumeFlag = 0;

            if (this.r_InputManager.KeyPressed(Microsoft.Xna.Framework.Input.Keys.PageUp))
            {
                raiseOrLowerVolumeFlag = 1;
            }
            else if (this.r_InputManager.KeyPressed(Microsoft.Xna.Framework.Input.Keys.PageDown))
            {
                raiseOrLowerVolumeFlag = -1;
            }

            return raiseOrLowerVolumeFlag;
        }

        private void done_OnClicked(object sender, EventArgs e)
        {
            ExitScreen();
        }
    }
}
