using System;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace Infrastructure.ServiceInterfaces
{
    public interface ISoundManager
    {
        EventHandler OnSoundToggled { get; set; }

        bool SoundToggle { get; set; }

        Dictionary<string, SoundEffectInstance> SoundEffects { get; }

        void AddSoundEffect(SoundEffect i_SoundEffect, string i_SoundName);

        void PlaySoundEffect(string i_SoundEffect);

        void PlayHoverSound();

        void SetHoverSoundEffect(string i_SoundName);

        void SetBackgroundMusic(SoundEffect i_SoundEffect);

        int GetBackgroundMusicVolumePercentage();

        int GetSoundEffectsVolumePercentage();

        void ChangeBackgroundMusicVolumeLevel(float i_VolumeChange);

        void ChangeSoundEffectsVolumeLevel(float i_VolumeChange);
    }
}
