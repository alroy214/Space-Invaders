using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Managers
{
    public class SoundManager //: ISoundManager
    {
        private Dictionary<string, SoundEffectInstance> m_SoundsEffects = new Dictionary<string, SoundEffectInstance>();
        private Dictionary<string, Song> m_Songs = new Dictionary<string, Song>();
        private bool m_isGameSoundOn = true;

        public bool IsGameSoundOn
        {
            get { return this.m_isGameSoundOn; }
            set { this.m_isGameSoundOn = value; }
        }

        public Dictionary<string, SoundEffectInstance> SoundEffects
        {
            get { return this.m_SoundsEffects; }
        }

        public Dictionary<string, Song> Songs
        {
            get { return this.m_Songs; }
        }

        /*
        public SoundManager(Game i_Game) : base(i_Game)
        {
        }
        */

        public void PlaySoundEffect(string i_SoundEffect)
        {
            if (this.m_isGameSoundOn)
            {
                //this.m_SoundsEffects[i_SoundEffect].Play();
            }
        }

        /*
        protected override void RegisterAsService()
        {
            this.Game.Services.AddService(typeof(ISoundManager), this);
        }
        */

        public void AddSoundEffect(SoundEffect i_SoundEffect, string i_SoundName)
        {
            this.m_SoundsEffects.Add(i_SoundName, i_SoundEffect.CreateInstance());
        }

        public void AddSong(Song i_Song, string i_SongName)
        {
            this.m_Songs.Add(i_SongName, i_Song);
        }

        public void RemoveSoundEffect(string i_SoundName)
        {
            this.m_SoundsEffects.Remove(i_SoundName);
        }

        public void RemoveSong(string i_SongName)
        {
            this.m_Songs.Remove(i_SongName);
        }

        public Song GetSong(string i_Name)
        {
            return this.m_Songs[i_Name];
        }

        public SoundEffectInstance GetSoundEffect(string i_Name)
        {
            return this.m_SoundsEffects[i_Name];
        }
    }
}
