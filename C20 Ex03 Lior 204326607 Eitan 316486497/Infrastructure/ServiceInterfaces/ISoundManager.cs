using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Infrastructure.ServiceInterfaces
{
    public interface ISoundManager
    {
        bool SoundToggle { get; set; }
        
        SoundEffectInstance GetSoundEffect(string i_Name);

        Dictionary<string, SoundEffectInstance> SoundEffects { get; }

        void AddSoundEffect(SoundEffect i_SoundEffect, string i_SoundName);

        void RemoveSoundEffect(string i_SoundName);

        void PlaySoundEffect(string i_SoundEffect);

        void PlayHoverSound();

        void SetBackgroundMusic(SoundEffect i_SoundEffect);

        int GetBackgroundMusicVolumePercentage();

        int GetSoundEffectsVolumePercentage();

        void ChangeBackgroundMusicVolumeLevel(float i_VolumeChange);

        void ChangeSoundEffectsVolumeLevel(float i_VolumeChange);
    }
}
