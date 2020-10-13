using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using Infrastructure.ObjectModel;
using System.Linq;

namespace Infrastructure.Managers
{
    public class SoundManager : GameService, ISoundManager
    {
        public EventHandler OnSoundToggled { get; set; }
        private const bool k_MusicOnFromStart = true;
        private const int k_MaxSoundsVolumeLevel = 100;
        private const int k_MinSoundsVolumeLevel = 0;
        private readonly Dictionary<string, SoundEffectInstance> r_SoundsEffects;
        private SoundEffectInstance m_CurrentBackgroundMusic;
        private string m_CurrentHoverSoundEffect;
        private bool m_SoundEffectsEnabled;

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
                OnSoundToggled?.Invoke(this, EventArgs.Empty);
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
            if (m_SoundEffectsEnabled && r_SoundsEffects.ContainsKey(i_SoundEffect))
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
            return r_SoundsEffects.ContainsKey(i_Name) ? r_SoundsEffects[i_Name] : null;
        }

        public int GetBackgroundMusicVolumePercentage()
        {
            if(m_CurrentBackgroundMusic == null)
            {
                return 0;
            }

            return (int) (m_CurrentBackgroundMusic.Volume * 100);
        }

        public int GetSoundEffectsVolumePercentage()
        {
            if (r_SoundsEffects == null || r_SoundsEffects.Count == 0)
            {
                return 0;
            }

            return (int) (r_SoundsEffects.First().Value.Volume * 100);
        }

        private float changeVolumeLevel(float i_CurrentVolume, float i_VolumeChange)
        {
            i_CurrentVolume = i_CurrentVolume * 100 + i_VolumeChange;;
            if (i_CurrentVolume > k_MaxSoundsVolumeLevel)
            {
                i_CurrentVolume = k_MinSoundsVolumeLevel;
            }
            else if (i_CurrentVolume < k_MinSoundsVolumeLevel)
            {
                i_CurrentVolume = k_MaxSoundsVolumeLevel;
            }

            return i_CurrentVolume / 100;
        }

        public void ChangeBackgroundMusicVolumeLevel(float i_VolumeChange)
        {
            if (m_CurrentBackgroundMusic != null)
            {
                m_CurrentBackgroundMusic.Volume = changeVolumeLevel(m_CurrentBackgroundMusic.Volume, i_VolumeChange);
            }
        }

        public void ChangeSoundEffectsVolumeLevel(float i_VolumeChange)
        {
            if (r_SoundsEffects != null && r_SoundsEffects.Count != 0)
            {
                float newVolumeLevel = changeVolumeLevel(r_SoundsEffects.First().Value.Volume, i_VolumeChange);

                foreach (SoundEffectInstance soundEffectInstance in r_SoundsEffects.Values)
                {
                    soundEffectInstance.Volume = newVolumeLevel;
                }
            }
        }
    }
}
