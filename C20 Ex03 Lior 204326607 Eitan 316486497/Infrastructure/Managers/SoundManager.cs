using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.ObjectModel;
using System.Linq;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Managers
{
    public class SoundManager : GameService, ISoundManager
    {
        private const bool k_MusicOnFromStart = false;
        private readonly Dictionary<string, SoundEffectInstance> r_SoundsEffects;
        private SoundEffectInstance m_CurrentBackgroundMusic;
        private string m_CurrentHoverSoundEffect;
        private bool m_SoundEffectsEnabled;
        private const int k_MaxSoundsVolumeLevel = 100;
        private const int k_MinSoundsVolumeLevel = 0;

        public SoundManager(Game i_Game) : base(i_Game)
        {
            r_SoundsEffects = new Dictionary<string, SoundEffectInstance>();
            m_SoundEffectsEnabled = k_MusicOnFromStart;
        }

        protected override void RegisterAsService()
        {
            Game.Services.AddService(typeof(ISoundManager), this);
        }

        public bool SoundToggle
        {
            get
            {
                return m_SoundEffectsEnabled;
            }
            set
            {
                m_SoundEffectsEnabled = value;
                toggleBackgroundMusic();
            }
        }

        public Dictionary<string, SoundEffectInstance> SoundEffects
        {
            get
            {
                return r_SoundsEffects;
            }
        }

        private void toggleBackgroundMusic()
        {
            if (m_SoundEffectsEnabled)
            {
                m_CurrentBackgroundMusic?.Play();
            }
            else
            {
                m_CurrentBackgroundMusic?.Pause();
            }
        }
        
        public void SetBackgroundMusic(SoundEffect i_SoundEffect)
        {
            m_CurrentBackgroundMusic = i_SoundEffect.CreateInstance();
            m_CurrentBackgroundMusic.IsLooped = true;
            if (m_SoundEffectsEnabled)
            {
                m_CurrentBackgroundMusic.Play();
            }
        }

        public void SetHoverSoundEffect(string i_SoundName)
        {
            m_CurrentHoverSoundEffect = i_SoundName;
        }

        public void PlayHoverSound()
        {
            PlaySoundEffect(m_CurrentHoverSoundEffect);
        }

        public void PlaySoundEffect(string i_SoundEffect)
        {
            if (m_SoundEffectsEnabled)
            {
                r_SoundsEffects[i_SoundEffect]?.Play();
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

        public int GetBackgroundMusicVolumePercentage()
        {
            return (int) (this.m_CurrentBackgroundMusic.Volume * 100);
        }

        public int GetSoundEffectsVolumePercentage()
        {
            return (int) (r_SoundsEffects.First().Value.Volume * 100);
        }

        public void ChangeBackgroundMusicVolumeLevel(float i_VolumeChange)
        {
            float backgroundMusicOldVolumeLevel = m_CurrentBackgroundMusic.Volume * 100;
            float backgroundMusicNewVolumeLevel = MathHelper.Clamp(backgroundMusicOldVolumeLevel + i_VolumeChange, k_MinSoundsVolumeLevel, k_MaxSoundsVolumeLevel);
            
            m_CurrentBackgroundMusic.Volume = backgroundMusicNewVolumeLevel / 100;
        }

        public void ChangeSoundEffectsVolumeLevel(float i_VolumeChange)
        {
            float soundEffectsOldVolumeLevel = r_SoundsEffects.First().Value.Volume * 100;
            float soundEffectsNewVolumeLevel = MathHelper.Clamp(soundEffectsOldVolumeLevel + i_VolumeChange, k_MinSoundsVolumeLevel, k_MaxSoundsVolumeLevel);

            foreach (SoundEffectInstance soundEffectInstance in r_SoundsEffects.Values)
            {
                soundEffectInstance.Volume = soundEffectsNewVolumeLevel / 100;
            }
        }
    }
}
