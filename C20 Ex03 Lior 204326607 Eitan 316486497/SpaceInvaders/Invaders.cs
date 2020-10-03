using System;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships;
using C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Stats;
using Infrastructure.Managers;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Lior_204326607_Eitan_316486497
{
    public class Invaders : BaseGame
    {
        private int k_WindowSizeX = 800;
        private int k_WindowSizeY = 600;
        private SpriteBatch m_SpriteBatch;
        private Background m_Background;
        private PlayerShip m_PlayerShip1;
        private PlayerShip m_PlayerShip2;
        private MotherShip m_MotherShip;
        private EnemyMatrix m_EnemyMatrix;
        private ScoreHeader m_ScoreHeader;
        private ScoreManager m_ScoreManager;
        private BarrierCluster m_BarrierCluster;
        private int m_CurrentLivingPlayers;
        public const int k_NumberOfWalls = 4;
        private GameScreen welcomeScreen;
        public Invaders() : base()
        {
            r_Graphics.IsFullScreen = false;
            r_Graphics.PreferredBackBufferWidth = k_WindowSizeX;
            r_Graphics.PreferredBackBufferHeight = k_WindowSizeY;
            r_Graphics.ApplyChanges();
            Window.Title = "Invaders 👽";

            m_CurrentLivingPlayers = Enum.GetNames(typeof(PlayerShip.ePlayer)).Length;
            
             welcomeScreen = new WelcomeScreen(this);
             r_Screens.SetCurrentScreen(welcomeScreen);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
        }

        public async void HandleGameOver(object sender, EventArgs e)
        {
            if (m_CurrentLivingPlayers == 1 || !(sender is PlayerShip))
            {
                foreach (IGameComponent gameComp in Components)
                {
                    ((GameComponent)gameComp).Enabled = false;
                }

                if (!MessageBox.IsVisible)  
                {
            //        await m_ScoreHeader.GameOverMessage();
                    Exit();
                }
            }
            else
            {
                m_CurrentLivingPlayers--;
            }
        }
    }
}
