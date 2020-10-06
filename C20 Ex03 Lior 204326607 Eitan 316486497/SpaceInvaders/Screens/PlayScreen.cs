using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.GameEntities.Ships;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Stats;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens
{
    public class PlayScreen :  GameScreen
    {
        private readonly Background r_Background;
        private readonly MotherShip r_MotherShip;
        private readonly EnemyMatrix r_EnemyMatrix;
        private readonly ScoreManager r_ScoreManager;
        private readonly BarrierCluster r_BarrierCluster;
        private readonly PlayerFormation r_PlayerFormation;
        private readonly ScoreHeader r_ScoreHeader;
        private readonly PauseScreen r_PauseScreen;
        private bool m_IsPaused;

        public PlayScreen(Game i_Game) : base(i_Game)
        {
            r_Background = new Background(this);
            r_ScoreManager = new ScoreManager(this);
            r_MotherShip = new MotherShip(this);
            r_EnemyMatrix = new EnemyMatrix(this);
            r_BarrierCluster = new BarrierCluster(this);
            r_PlayerFormation = new PlayerFormation(this);
            r_ScoreHeader = new ScoreHeader(this);
            r_PauseScreen = new PauseScreen(Game);
            r_PlayerFormation.OnAllShipDestroyed(HandleGameOver);
            r_PauseScreen.StateChanged += onPauseScreenChanged;
            m_IsPaused = false;
        }

        public void HandleGameOver(object sender, EventArgs e)
        {
            m_ScreensManager.SetCurrentScreen(new GameOverScreen(Game, r_ScoreHeader.GetPlayerScores()));
            ExitScreen();
        }

        private void onPauseScreenChanged(object sender, StateChangedEventArgs e)
        {
            if (e.CurrentState.Equals(eScreenState.Closed))
            {
                m_IsPaused = false;
            }
        }

        public override void Update(GameTime i_GameTime)
        {
            if (!m_IsPaused)
            {
                base.Update(i_GameTime);

                if (InputManager.KeyPressed(Keys.P))
                {
                    ScreensManager.SetCurrentScreen(r_PauseScreen);
                    m_IsPaused = true;
                }
            }
        }
    }
}
