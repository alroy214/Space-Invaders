using System;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities;
using C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure.ObjectModel.Screens.Items;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens.MenuScreens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens
{
    public class GameOverScreen : NavigableScreen
    {
        private const string k_HeaderAssetName = @"Headers\Game Over";
        private const float k_HeaderScale = 0.3f;
        private const int k_HeaderOffsetY = 50;
        private const string k_ThankYouMessage = "Thanks for playing! :)";
        private const string k_PlayerMessage = "Player ";
        private const string k_ScoreSeparatorMessage = ": ";
        private const string k_WinMessage = " Wins!";
        private const string k_TieMessage = "It's a tie!";
        private const string k_ExitMessage = "Press Esc Key to exit the game";
        private const string k_StartMessage = "Press the Home Key to start the game";
        private const string k_MenuMessage = "Press the M Key to show the settings";
        private readonly int[] r_PlayerScores;
        private readonly Background r_Background;
        private readonly ScreenHeader r_ScreenHeader;
        private readonly TextItem r_ScoreMessage;

        public GameOverScreen(Game i_Game, int[] i_PlayerScores) : base(i_Game)
        {
            r_Background = new Background(this);
            r_PlayerScores = i_PlayerScores;
            r_ScreenHeader = new ScreenHeader(this, k_HeaderAssetName, k_HeaderScale, GraphicsDevice.Viewport.Width / 2f, k_HeaderOffsetY, true);
            r_ScoreMessage = new TextItem(this, createScoreMessage(i_PlayerScores), NumberOfItemsOnScreen(),
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
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            r_ScoreMessage.Scales = new Vector2(0.45f, 0.9f);
            r_ScoreMessage.Position = new Vector2(GraphicsDevice.Viewport.Width / 2f - r_ScoreMessage.Width / 2,
                GraphicsDevice.Viewport.Height / 2f - r_ScoreMessage.Height);
        }

        private string createScoreMessage(int[] i_PlayerScores)
        {
            string scoreMessage = "";

            for (int i = 0; i < i_PlayerScores.Length; i++)
            {
                scoreMessage += string.Format("{0}{1}{2}{3}{4}", k_PlayerMessage, i + 1, k_ScoreSeparatorMessage, i_PlayerScores[i], Environment.NewLine);
            }

            scoreMessage = string.Format("{0}{1}{2}{3}", scoreMessage, getWinningPlayerMessage(), Environment.NewLine, k_ThankYouMessage);

            return scoreMessage;
        }

        private void startButton_OnClicked(object sender, EventArgs e)
        {
            m_ScreensManager.SetCurrentScreen(new LevelTransitionScreen(Game));
            ExitScreen();
        }

        private void exitButton_OnClicked(object sender, EventArgs e)
        {
            Game.Exit();
        }

        private void menuButton_OnClicked(object sender, EventArgs e)
        {
            SetScreen(new MainMenu(Game), false);
        }

        private string getWinningPlayerMessage()
        {
            string winner;
            int highestPlayerScore = r_PlayerScores[0];
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

            if (itsATie)
            {
                winner = k_TieMessage;
            }
            else
            {
                winner = String.Format("{0}{1}{2}", k_PlayerMessage, ++currentWinner, k_WinMessage);
            }
            return winner;
        }

    }
}
