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
        private const bool k_IsMusicOnFromStart = false;
        private readonly Dictionary<string, SoundEffectInstance> r_SoundsEffects;
        private SoundEffectInstance m_CurrentBackgroundMusic;
        private bool m_IsSoundEffectsOn;

        public SoundManager(Game i_Game) : base(i_Game)
        {
            r_SoundsEffects = new Dictionary<string, SoundEffectInstance>();
            m_IsSoundEffectsOn = k_IsMusicOnFromStart;
        }

        protected override void RegisterAsService()
        {
            Game.Services.AddService(typeof(ISoundManager), this);
        }

        public bool SoundToggle
        {
            get
            {
                return m_IsSoundEffectsOn;
            }
            set
            {
                m_IsSoundEffectsOn = value;
            }
        }

        public Dictionary<string, SoundEffectInstance> SoundEffects
        {
            get
            {
                return r_SoundsEffects;
            }
        }
        
        public void SetBackgroundMusic(SoundEffect i_SoundEffect)
        {
            m_CurrentBackgroundMusic = i_SoundEffect.CreateInstance();
            m_CurrentBackgroundMusic.IsLooped = true;
            if(m_IsSoundEffectsOn)
            {
                m_CurrentBackgroundMusic.Play();
            }
        }

        public void PlaySoundEffect(string i_SoundEffect)
        {
            if (m_IsSoundEffectsOn)
            {
                r_SoundsEffects[i_SoundEffect].Play();
            }
        }      
        
        public void AddSoundEffect(SoundEffect i_SoundEffect, string i_SoundName)
        {
            r_SoundsEffects.Add(i_SoundName, i_SoundEffect.CreateInstance());
        }

        public void RemoveSoundEffect(string i_SoundName)
        {
            r_SoundsEffects.Remove(i_SoundName);
        }

        public SoundEffectInstance GetSoundEffect(string i_Name)
        {
            return r_SoundsEffects[i_Name];
        }
    }
}
