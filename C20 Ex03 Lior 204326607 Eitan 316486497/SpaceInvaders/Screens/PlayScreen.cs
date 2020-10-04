using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Stats;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens
{
    class PlayScreen :  GameScreen
    {
        private Background m_Background;
        private PlayerShip m_PlayerShip1;
        private PlayerShip m_PlayerShip2;
        private MotherShip m_MotherShip;
        private EnemyMatrix m_EnemyMatrix;
        private ScoreHeader m_ScoreHeader;
        private ScoreManager m_ScoreManager;
        private BarrierCluster m_BarrierCluster;
        private PauseScreen m_PauseScreen;
        private bool m_IsPaused;

        public PlayScreen(Game i_Game) : base(i_Game)
        {
            m_Background = new Background(this);
            m_ScoreManager = new ScoreManager(this);
            m_MotherShip = new MotherShip(this);
            m_EnemyMatrix = new EnemyMatrix(this);
            m_ScoreHeader = new ScoreHeader(this);
            m_PlayerShip1 = new PlayerShip(this, PlayerShip.ePlayer.Player1);
            m_PlayerShip2 = new PlayerShip(this, PlayerShip.ePlayer.Player2);
            m_BarrierCluster = new BarrierCluster(this);
            m_PauseScreen = new PauseScreen(Game);
            m_PauseScreen.StateChanged += onPauseScreenChanged;
            m_IsPaused = false;
        }

        private void onPauseScreenChanged(object sender, StateChangedEventArgs e)
        {
            if(e.CurrentState.Equals(eScreenState.Closed))
            {
                m_IsPaused = false;
            }
        }

        public override void Update(GameTime i_GameTime)
        {
            if(!m_IsPaused)
            {
                base.Update(i_GameTime);

                if(InputManager.KeyPressed(Keys.P))
                {
                    ScreensManager.SetCurrentScreen(m_PauseScreen);
                    m_IsPaused = true;
                }
            }
        }
    }
}
