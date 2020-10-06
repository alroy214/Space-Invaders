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
//using Microsoft.Xna.Framework.Media;

namespace C20_Ex03_Lior_204326607_Eitan_316486497
{
    public class Invaders : BaseGame
    {
        private const int k_WindowSizeX = 800;
        private const int k_WindowSizeY = 600;
        private const string k_WindowTitle = "Invaders 👽";
        private SpriteBatch m_SpriteBatch;

        private readonly PlayManager r_PlayManager;

        public Invaders() : base()
        {
            r_PlayManager = new PlayManager(this);
         //   r_playManager.PlayDifficultyLevel = 8;
            r_Graphics.IsFullScreen = false;
            r_Graphics.PreferredBackBufferWidth = k_WindowSizeX;
            r_Graphics.PreferredBackBufferHeight = k_WindowSizeY;
            r_Graphics.ApplyChanges();
            Window.Title = k_WindowTitle;
           // this.Window.AllowUserResizing = true;
            GameScreen welcomeScreen = new WelcomeScreen(this);
            //GameScreen welcomeScreen = new GameOverScreen(this, new []{10, 1}); 
            r_Screens.SetCurrentScreen(welcomeScreen);

        }

        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
        }
    }
}
