using System;
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
        private readonly PlayerFormation r_PlayerFormation;
        private readonly BarrierCluster r_BarrierCluster;
        private readonly ScoreHeader r_ScoreHeader;
        private readonly IPlayManager r_PlayManager;
        private readonly IScoreManager r_ScoreManager;
        private readonly ISoundManager r_SoundManager;

        public PlayScreen(Game i_Game) : base(i_Game)
        {
            r_PlayManager = (IPlayManager)i_Game.Services.GetService(typeof(IPlayManager));
            r_ScoreManager = (IScoreManager)i_Game.Services.GetService(typeof(IScoreManager));
            r_SoundManager = (ISoundManager)i_Game.Services.GetService(typeof(ISoundManager));
            r_Background = new Background(this);
            r_MotherShip = new MotherShip(this);
            r_EnemyMatrix = new EnemyMatrix(this);
            r_BarrierCluster = new BarrierCluster(this);
            r_PlayerFormation = new PlayerFormation(this);
            r_ScoreHeader = new ScoreHeader(this);
            r_EnemyMatrix.OnAllEnemiesDestroyed(HandleLevelWin);
            r_EnemyMatrix.OnEnemyCausedGameOver(HandleGameOver);
            r_PlayerFormation.OnAllShipDestroyed(HandleGameOver);
        }
        
        public void HandleLevelWin(object sender, EventArgs e)
        {
            Dispose(true);
            r_PlayManager.IncreaseDifficultyLevel();
            r_SoundManager.PlaySoundEffect(MusicUtils.k_LevelWinSound);
            m_ScreensManager.SetCurrentScreen(new LevelTransitionScreen(Game));
        }

        public void HandleGameOver(object sender, EventArgs e)
        {
            Dispose(true);
            r_SoundManager.PlaySoundEffect(MusicUtils.k_GameOverSound);
            m_ScreensManager.SetCurrentScreen(new GameOverScreen(Game));
            r_PlayManager.PlayDifficultyLevel = PlayManager.k_DefaultDifficultyLevel;
            r_ScoreManager.ResetScores();
            r_PlayManager.ResetLives();
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
                PauseScreen pauseScreen;

                Enabled = false;
                pauseScreen = new PauseScreen(Game);
                pauseScreen.StateChanged += onPauseScreenChanged;
                ScreensManager.SetCurrentScreen(pauseScreen);
            }
        }
    }
}
