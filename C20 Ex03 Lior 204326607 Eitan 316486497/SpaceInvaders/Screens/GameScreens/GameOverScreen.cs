using System;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities;
using C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure.ObjectModel.Screens.Items;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens.MenuScreens;
using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens
{
    public class GameOverScreen : NavigableScreen
    {
        private const string k_HeaderAssetName = @"Headers\Game Over";
        private const float k_HeaderScale = 0.3f;
        private const float k_ScoreScaleX = 0.45f;
        private const float k_ScoreScaleY = 0.9f;
        private const int k_HeaderOffsetY = 40;
        private const string k_ThankYouMessage = "Thanks for playing! :)";
        private const string k_PlayerMessage = "Player ";
        private const string k_ScoreSeparatorMessage = ": ";
        private const string k_WinMessage = " Wins!";
        private const string k_TieMessage = "It's a tie!";
        private const string k_ExitMessage = "Press Esc Key to exit the game";
        private const string k_StartMessage = "Press the Home Key to start the game";
        private const string k_MenuMessage = "Press the M Key to show the settings";
        private const float k_ActivationTime = 1f;
        private readonly int[] r_PlayerScores;
        private readonly Background r_Background;
        private readonly Sprite r_ScreenHeader;
        private readonly TextItem r_ScoreMessage;

        public GameOverScreen(Game i_Game) : base(i_Game)
        {
            r_Background = new Background(this);
            int numberOfPlayers = ((IPlayManager)i_Game.Services.GetService(typeof(IPlayManager))).NumberOfPlayers;
            r_PlayerScores = ((IScoreManager)Game.Services.GetService(typeof(IScoreManager))).GetScores(numberOfPlayers);
            r_ScreenHeader = CreateHeaderSprite(k_HeaderAssetName, k_HeaderScale);
            r_ScoreMessage = new TextItem(this, createScoreMessage(r_PlayerScores), NumberOfItemsOnScreen(),
                                          Color.DarkBlue, Color.AliceBlue, true);
            TextItem exitButton = new TextItem(this, k_ExitMessage, NumberOfItemsOnScreen(), Color.PaleVioletRed, Keys.Escape);
            AddGameItem(exitButton);
            exitButton.AddToOnClick(exitButton_OnClicked);
            TextItem startButton = new TextItem(this, k_StartMessage, NumberOfItemsOnScreen(), Color.LightSeaGreen, Keys.Home);
            startButton.AddToOnClick(startButton_OnClicked);
            AddGameItem(startButton);
            TextItem menuButton = new TextItem(this, k_MenuMessage, NumberOfItemsOnScreen(), Color.DodgerBlue, Keys.M);
            menuButton.AddToOnClick(menuButton_OnClicked);
            AddGameItem(menuButton);
            ActivationLength = TimeSpan.FromSeconds(k_ActivationTime);
            UseFadeTransition = true;
            Game.Window.ClientSizeChanged += changePosition;
        }

        private void changePosition(object i_Sender, EventArgs i_E)
        {
            changePosition();
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            if (Math.Abs(TransitionPosition - 1) > 0)
            {
                r_ScreenHeader.Scales = new Vector2(TransitionPosition) * k_HeaderScale;
            }
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            r_ScoreMessage.Scales = new Vector2(k_ScoreScaleX, k_ScoreScaleY);
            changePosition();
        }

        private void changePosition()
        {
            r_ScoreMessage.Position = new Vector2(GraphicsDevice.Viewport.Width / 2f - r_ScoreMessage.Width / 2f,
                GraphicsDevice.Viewport.Height / 2f - r_ScoreMessage.Height);

            r_ScreenHeader.Position = new Vector2(GraphicsDevice.Viewport.Width / 2f - r_ScreenHeader.Width / 2f,
                GraphicsDevice.Viewport.Height / 2f - r_ScreenHeader.Height - r_ScoreMessage.Texture.Height - k_HeaderOffsetY);
        }

        private string createScoreMessage(int[] i_PlayerScores)
        {
            string scoreMessage = "";
            for (int i = 0; i < i_PlayerScores.Length; i++)
            {
                scoreMessage += string.Concat(k_PlayerMessage, i + 1, k_ScoreSeparatorMessage, i_PlayerScores[i], Environment.NewLine);
            }

            scoreMessage = string.Concat(scoreMessage, getWinningPlayerMessage(), Environment.NewLine, k_ThankYouMessage);
            return scoreMessage;
        }

        private void startButton_OnClicked(object sender, EventArgs e)
        {
            m_ScreensManager.SetCurrentScreen(new LevelTransitionScreen(Game));
        }

        private void exitButton_OnClicked(object sender, EventArgs e)
        {
            Game.Exit();
        }

        private void menuButton_OnClicked(object sender, EventArgs e)
        {
            m_ScreensManager.SetCurrentScreen(new MainMenu(Game));
        }

        private string getWinningPlayerMessage()
        {
            int highestPlayerScore = r_PlayerScores.Length > 0 ? r_PlayerScores[0] : 0;
            int currentWinner = 0;
            bool itsATie = false;
            for (int i = 1; i < r_PlayerScores.Length; i++)
            {
                if(highestPlayerScore < r_PlayerScores[i])
                {
                    currentWinner = i;
                    highestPlayerScore = r_PlayerScores[i];
                    itsATie = false;
                }
                else if(highestPlayerScore == r_PlayerScores[i])
                {
                    itsATie = true;
                }
            }

            return itsATie ? k_TieMessage : string.Concat(k_PlayerMessage, currentWinner + 1, k_WinMessage);
        }
    }
}