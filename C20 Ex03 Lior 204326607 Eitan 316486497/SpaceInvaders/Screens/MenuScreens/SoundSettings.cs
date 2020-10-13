using System;
using C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure.ObjectModel.Screens.Items;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens.MenuScreens
{
    public class SoundSettings : MenuScreen
    {
        private const string k_HeaderAssetName = @"Headers\Sound Settings";
        private const float k_HeaderScale = 0.25f;
        private const string k_ToggleSoundMessage = "Toggle Sound: ";
        private const string k_BackgroundMusicMessage = "Background Music Volume: ";
        private const string k_SoundEffectsMessage = "Sound Effects Volume: ";
        private const string k_DoneMessage = "Done";
        private const int k_SoundsVolumeChange = 10;
        private readonly TextItem r_MusicToggleTextItem;
        private readonly ISoundManager r_SoundManager;

        public SoundSettings(Game i_Game) : base(i_Game)
        {
            SetScreenHeader(k_HeaderAssetName, k_HeaderScale);
            r_SoundManager = (ISoundManager)i_Game.Services.GetService(typeof(ISoundManager));
            r_SoundManager.OnSoundToggled += onMusicMuteKeyPressed;
            r_MusicToggleTextItem = AddOptionItem(k_ToggleSoundMessage, r_SoundManager.SoundToggle, 
                Color.MediumSeaGreen, soundToggle_OnClicked);
            AddOptionItem(k_BackgroundMusicMessage + r_SoundManager.GetBackgroundMusicVolumePercentage(),
                Color.CornflowerBlue, backgroundMusicMessage_OnClicked, true);
            AddOptionItem(k_SoundEffectsMessage + r_SoundManager.GetSoundEffectsVolumePercentage(),
                Color.Bisque, soundEffects_OnClicked, true);
            AddOptionItem(k_DoneMessage, Color.PaleVioletRed, done_OnClicked);
        }

        private void onMusicMuteKeyPressed(object sender, EventArgs e)
        {
            r_MusicToggleTextItem.TextMessage = GetDefaultToggleMessage(r_SoundManager.SoundToggle);
        }

        private void soundToggle_OnClicked(object sender, EventArgs e)
        {
            r_SoundManager.SoundToggle = !r_SoundManager.SoundToggle;
        }
        
        private void backgroundMusicMessage_OnClicked(object sender, EventArgs e)
        {
            int raiseOrLowerVolumeFlag = getRaiseOrLowerVolumeFlag(e);

            r_SoundManager.ChangeBackgroundMusicVolumeLevel((float) k_SoundsVolumeChange * raiseOrLowerVolumeFlag);
            ((TextItem)sender).TextMessage = k_BackgroundMusicMessage + r_SoundManager.GetBackgroundMusicVolumePercentage();
        }

        private void soundEffects_OnClicked(object sender, EventArgs e)
        {
            int raiseOrLowerVolumeFlag = getRaiseOrLowerVolumeFlag(e);

            r_SoundManager.ChangeSoundEffectsVolumeLevel((float) k_SoundsVolumeChange * raiseOrLowerVolumeFlag);
            ((TextItem)sender).TextMessage = k_SoundEffectsMessage + r_SoundManager.GetSoundEffectsVolumePercentage();
        }

        private int getRaiseOrLowerVolumeFlag(EventArgs i_ItemValue)
        {
            int raiseOrLowerVolumeFlag = 0;

            if (i_ItemValue is ItemValueChangeEventArgs itemValue)
            {
                switch (itemValue.ValueChange)
                {
                    case eValueChange.Increase:
                        {
                            raiseOrLowerVolumeFlag = 1;
                            break;
                        }
                    case eValueChange.Decrease:
                        {
                            raiseOrLowerVolumeFlag = -1;
                            break;
                        }
                    case eValueChange.Unchanged:
                        {
                            break;
                        }
                    default: { break; }
                }
            }
            else
            {
                raiseOrLowerVolumeFlag = 1;
            }

            return raiseOrLowerVolumeFlag;
        }

        private void done_OnClicked(object sender, EventArgs e)
        {
            ExitScreen();
        }
    }
}
