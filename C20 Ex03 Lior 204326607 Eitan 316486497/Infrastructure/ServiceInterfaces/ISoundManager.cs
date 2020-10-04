using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Infrastructure.ServiceInterfaces
{
    public interface ISoundManager
    {
        bool AreSoundsOn { get; set; }

        Dictionary<string, SoundEffectInstance> SoundEffects { get; }

        Dictionary<string, Song> Songs { get; }

        void AddSoundEffect(SoundEffect i_SoundEffect, string i_SoundName);

        void AddSong(Song i_Song, string i_SongName);

        void RemoveSoundEffect(string i_SoundName);

        void RemoveSong(string i_SongName);

        void PlaySoundEffect(string i_SoundEffect);

        Song GetSong(string i_Name);

        SoundEffectInstance GetSoundEffect(string i_Name);
    }
}
