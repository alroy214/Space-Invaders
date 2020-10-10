using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.GameEntities.Ships;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Stats;
using Infrastructure.Managers;
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
        private readonly BarrierCluster r_BarrierCluster;
        private readonly PlayerFormation r_PlayerFormation;
        private readonly ScoreHeader r_ScoreHeader;
        private readonly IPlayManager r_PlayManager;
        private readonly IScoreManager r_ScoreManager;

        public PlayScreen(Game i_Game) : base(i_Game)
        {
            r_PlayManager = i_Game.Services.GetService(typeof(IPlayManager)) as IPlayManager;
            r_ScoreManager = i_Game.Services.GetService(typeof(IScoreManager)) as IScoreManager;
            r_Background = new Background(this);
            r_MotherShip = new MotherShip(this);
            r_EnemyMatrix = new EnemyMatrix(this);
            r_BarrierCluster = new BarrierCluster(this);
            r_PlayerFormation = new PlayerFormation(this);
            r_ScoreHeader = new ScoreHeader(this);
            r_PlayerFormation.OnAllShipDestroyed(HandleGameOver);
        }

        //FIX TRANSITIONS TODO
        public void HandleLevelWin(object sender, EventArgs e)
        {
            r_PlayManager.IncreaseDifficultyLevel();
            onScreenEnd();
            m_ScreensManager.SetCurrentScreen(new LevelTransitionScreen(Game));
        }

        public void HandleGameOver(object sender, EventArgs e)
        {
            SetScreen(new GameOverScreen(Game, r_ScoreHeader.GetPlayerScores()));
            r_PlayManager.PlayDifficultyLevel = PlayManager.k_DefaultDifficultyLevel;
            r_ScoreManager.ResetScores();
            onScreenEnd();
        }

        private void onScreenEnd()
        {
            CollisionsManager collisionsManager =
                Game.Services.GetService(typeof(ICollisionsManager)) as CollisionsManager;
            collisionsManager?.PurgeCollisions();
        }

        private void onPauseScreenChanged(object sender, StateChangedEventArgs e)
        {
            if (e.CurrentState.Equals(eScreenState.Closed)) 
            {
                Enabled = true;
            }
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);

            if (InputManager.KeyPressed(Keys.P))
            {
                PauseScreen pauseScreen = new PauseScreen(Game);
                pauseScreen.StateChanged += onPauseScreenChanged;
                ScreensManager.SetCurrentScreen(pauseScreen);
                Enabled = false;
            }
        }
    }
}
