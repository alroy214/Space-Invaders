using System;
using System.Threading.Tasks;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships;
using C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Stats;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using MessageBox = Microsoft.Xna.Framework.Input.MessageBox;

namespace C20_Ex03_Lior_204326607_Eitan_316486497
{
    public class Invaders : BaseGame
    {
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

        public Invaders() : base()
        {
            m_CurrentLivingPlayers = Enum.GetNames(typeof(PlayerShip.ePlayer)).Length;
        }

        protected override void Initialize()
        {
            m_ScoreManager = new ScoreManager(this);
            m_Background = new Background(this);
            m_MotherShip = new MotherShip(this);
            m_EnemyMatrix = new EnemyMatrix(this);
            m_ScoreHeader = new ScoreHeader(this);
            m_PlayerShip1 = new PlayerShip(this, PlayerShip.ePlayer.Player1);
            m_PlayerShip2 = new PlayerShip(this, PlayerShip.ePlayer.Player2);
            m_BarrierCluster = new BarrierCluster(this);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
        }

        public async void HandleGameOver(object sender, EventArgs e)
        {
            if(m_CurrentLivingPlayers == 1 || !(sender is PlayerShip))
            {
                foreach (IGameComponent gameComp in Components)
                {
                    ((GameComponent)gameComp).Enabled = false;
                }

                if (!MessageBox.IsVisible)
                {
                    await m_ScoreHeader.GameOverMessage();
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
