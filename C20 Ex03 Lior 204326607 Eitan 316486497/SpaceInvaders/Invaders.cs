using System;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships;
using C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Managers;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Stats;
using Infrastructure.Managers;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Lior_204326607_Eitan_316486497
{
    public class Invaders : BaseGame
    {
        private const int k_WindowSizeX = 800;
        private const int k_WindowSizeY = 600;
        private const string k_WindowTitle = "Invaders 👽";
        private SpriteBatch m_SpriteBatch;

        private readonly PlayManager r_PlayManager;
        private readonly ScoreManager r_ScoreManager;

        public Invaders() : base()
        {
            r_PlayManager = new PlayManager(this);
            r_ScoreManager = new ScoreManager(this);
            //   r_playManager.PlayDifficultyLevel = 8;
            r_Graphics.IsFullScreen = false;
            r_Graphics.PreferredBackBufferWidth = k_WindowSizeX;
            r_Graphics.PreferredBackBufferHeight = k_WindowSizeY;
            r_Graphics.ApplyChanges();
            Window.Title = k_WindowTitle;
           // this.Window.AllowUserResizing = true;
            GameScreen welcomeScreen = new WelcomeScreen(this);
            GameScreen boopScreen = new GameOverScreen(this, new []{10, 1}); 
            r_Screens.SetCurrentScreen(boopScreen);
            r_Screens.SetCurrentScreen(welcomeScreen);

        }

        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            loadSoundEffects();
            base.LoadContent();
        }

        private void loadSoundEffects()
        {
            r_SoundManager.AddSoundEffect(Content.Load<SoundEffect>(@"C:/temp/XNA_Assets/Ex03/Sounds/SSGunShot"), "SSGunShot");
            r_SoundManager.AddSoundEffect(Content.Load<SoundEffect>(@"C:/temp/XNA_Assets/Ex03/Sounds/EnemyGunShot"), "EnemyGunShot");
            r_SoundManager.AddSoundEffect(Content.Load<SoundEffect>(@"C:/temp/XNA_Assets/Ex03/Sounds/EnemyKill"), "EnemyKill");
            r_SoundManager.AddSoundEffect(Content.Load<SoundEffect>(@"C:/temp/XNA_Assets/Ex03/Sounds/MotherShipKill"), "MotherShipKill");
            r_SoundManager.AddSoundEffect(Content.Load<SoundEffect>(@"C:/temp/XNA_Assets/Ex03/Sounds/BarrierHit"), "BarrierHit");
            r_SoundManager.AddSoundEffect(Content.Load<SoundEffect>(@"C:/temp/XNA_Assets/Ex03/Sounds/GameOver"), "GameOver");
            r_SoundManager.AddSoundEffect(Content.Load<SoundEffect>(@"C:/temp/XNA_Assets/Ex03/Sounds/LevelWin"), "LevelWin");
            r_SoundManager.AddSoundEffect(Content.Load<SoundEffect>(@"C:/temp/XNA_Assets/Ex03/Sounds/LifeDie"), "LifeDie");
            r_SoundManager.AddSoundEffect(Content.Load<SoundEffect>(@"C:/temp/XNA_Assets/Ex03/Sounds/MenuMove"), "MenuMove");
            r_SoundManager.SetBackgroundMusic(Content.Load<SoundEffect>(@"C:/temp/XNA_Assets/Ex03/Sounds/BGMusic"));
            r_SoundManager.SetHoverSoundEffect("MenuMove");
        }
    }
}
