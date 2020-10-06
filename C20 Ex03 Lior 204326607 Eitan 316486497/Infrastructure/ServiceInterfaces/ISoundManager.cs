using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Infrastructure.ServiceInterfaces
{
    public interface ISoundManager
    {
        bool ToggledSoundOn { get; set; }
        
        SoundEffectInstance GetSoundEffect(string i_Name);

        Dictionary<string, SoundEffectInstance> SoundEffects { get; }

        void AddSoundEffect(SoundEffect i_SoundEffect, string i_SoundName);

        void RemoveSoundEffect(string i_SoundName);

        void PlaySoundEffect(string i_SoundEffect);

    }
}
