using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.ObjectModel;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Managers
{
    public class SoundManager : GameService, ISoundManager
    {
        private const bool k_IsMusicOnFromStart = true;
        private readonly Dictionary<string, SoundEffectInstance> r_SoundsEffects;
        private readonly Dictionary<string, Song> r_Songs;
        private bool m_IsGameSoundOn;

        public SoundManager(Game i_Game) : base(i_Game)
        {
            m_IsGameSoundOn = k_IsMusicOnFromStart;
            r_SoundsEffects = new Dictionary<string, SoundEffectInstance>();
            r_Songs = new Dictionary<string, Song>();
        }


        protected override void RegisterAsService()
        {
            this.Game.Services.AddService(typeof(ISoundManager), this);
        }

        public bool IsGameSoundOn
        {
            get { return this.m_IsGameSoundOn; }
            set { this.m_IsGameSoundOn = value; }
        }

        public bool AreSoundsOn { get; set; }

        public Dictionary<string, SoundEffectInstance> SoundEffects
        {
            get { return this.r_SoundsEffects; }
        }

        public Dictionary<string, Song> Songs
        {
            get { return this.r_Songs; }
        }

        public void PlaySoundEffect(string i_SoundEffect)
        {
            if (this.m_IsGameSoundOn)
            {
                //this.r_SoundsEffects[i_SoundEffect].Play();
            }
        }

        
        

        public void AddSoundEffect(SoundEffect i_SoundEffect, string i_SoundName)
        {
            this.r_SoundsEffects.Add(i_SoundName, i_SoundEffect.CreateInstance());
        }

        public void AddSong(Song i_Song, string i_SongName)
        {
            this.r_Songs.Add(i_SongName, i_Song);
        }

        public void RemoveSoundEffect(string i_SoundName)
        {
            this.r_SoundsEffects.Remove(i_SoundName);
        }

        public void RemoveSong(string i_SongName)
        {
            this.r_Songs.Remove(i_SongName);
        }

        public Song GetSong(string i_Name)
        {
            return this.r_Songs[i_Name];
        }

        public SoundEffectInstance GetSoundEffect(string i_Name)
        {
            return this.r_SoundsEffects[i_Name];
        }
    }
}
