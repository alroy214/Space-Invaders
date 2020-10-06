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
        private bool m_IsSoundToggledOn;

        public SoundManager(Game i_Game) : base(i_Game)
        {
            m_IsSoundToggledOn = k_IsMusicOnFromStart;
            r_SoundsEffects = new Dictionary<string, SoundEffectInstance>();
            loadSoundEffects();
            setBackgroundMusicState(m_IsSoundToggledOn);
        }


        protected override void RegisterAsService()
        {
            this.Game.Services.AddService(typeof(ISoundManager), this);
        }

        private void loadSoundEffects()
        {
            AddSoundEffect(Game.Content.Load<SoundEffect>(@"C:/temp/XNA_Assets/Ex03/Sounds/SSGunShot"), "SSGunShot");
            AddSoundEffect(Game.Content.Load<SoundEffect>(@"C:/temp/XNA_Assets/Ex03/Sounds/EnemyGunShot"), "EnemyGunShot");
            AddSoundEffect(Game.Content.Load<SoundEffect>(@"C:/temp/XNA_Assets/Ex03/Sounds/EnemyKill"), "EnemyKill");
            AddSoundEffect(Game.Content.Load<SoundEffect>(@"C:/temp/XNA_Assets/Ex03/Sounds/MotherShipKill"), "MotherShipKill");
            AddSoundEffect(Game.Content.Load<SoundEffect>(@"C:/temp/XNA_Assets/Ex03/Sounds/BarrierHit"), "BarrierHit");
            AddSoundEffect(Game.Content.Load<SoundEffect>(@"C:/temp/XNA_Assets/Ex03/Sounds/GameOver"), "GameOver");
            AddSoundEffect(Game.Content.Load<SoundEffect>(@"C:/temp/XNA_Assets/Ex03/Sounds/LevelWin"), "LevelWin");
            AddSoundEffect(Game.Content.Load<SoundEffect>(@"C:/temp/XNA_Assets/Ex03/Sounds/LifeDie"), "LifeDie");
            AddSoundEffect(Game.Content.Load<SoundEffect>(@"C:/temp/XNA_Assets/Ex03/Sounds/MenuMove"), "MenuMove");
            AddSoundEffect(Game.Content.Load<SoundEffect>(@"C:/temp/XNA_Assets/Ex03/Sounds/BGMusic"), "BGMusic");
        }

        public bool ToggledSoundOn
        {
            get { return this.m_IsSoundToggledOn; }
            set { this.m_IsSoundToggledOn = value; }
        }

        public Dictionary<string, SoundEffectInstance> SoundEffects
        {
            get { return this.r_SoundsEffects; }
        }

        private void setBackgroundMusicState(bool i_On)
        {
            SoundEffectInstance bgMusic = GetSoundEffect("BGMusic");
            if (i_On)
            {
                bgMusic.IsLooped = true;
                bgMusic.Play();
            }
            else
            {
                bgMusic.Stop();
            }
        }

        public void PlaySoundEffect(string i_SoundEffect)
        {
            if (this.m_IsSoundToggledOn)
            {
                this.r_SoundsEffects[i_SoundEffect].Play();
            }
        }      
        
        public void AddSoundEffect(SoundEffect i_SoundEffect, string i_SoundName)
        {
            this.r_SoundsEffects.Add(i_SoundName, i_SoundEffect.CreateInstance());
        }

        public void RemoveSoundEffect(string i_SoundName)
        {
            this.r_SoundsEffects.Remove(i_SoundName);
        }

        public SoundEffectInstance GetSoundEffect(string i_Name)
        {
            return this.r_SoundsEffects[i_Name];
        }
    }
}
