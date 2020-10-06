using System;
using C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure.ObjectModel.Screens.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
/*
 * /*
        

        public async Task GameOverMessage()
        {
            string message = string.Format("{0} {3} {1} {3} {2} {3} {4}",
                r_ScoreBoards[0], r_ScoreBoards[1], getWinningPlayer(), Environment.NewLine, k_ThankYouMessage);

            await MessageBox.Show(k_MessageBoxTitle, message, new[] { k_MessageBoxButton });
        }

        */

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens
{
    public class GameOverScreen : NavigableScreen
    {
        private const string k_ThankYouMessage = "Thanks for playing! :)";
        private const string k_PlayerMessage = "Player ";
        private const string k_ScoreSeparatorMessage = ": ";
        private const string k_WinMessage = " Wins!";
        private const string k_TieMessage = "It's a tie!";
        private const string k_ExitMessage = "Press Esc Key to exit the game";
        private const string k_StartMessage = "Press the Home Key to start the game";
        private const string k_MenuMessage = "Press the M Key to show the settings";
        private int[] r_PlayerScores;
        private ScreenHeader m_ScreenHeader;

        public GameOverScreen(Game i_Game, int[] i_PlayerScores) : base(i_Game)
        {
            r_PlayerScores = i_PlayerScores;
            float viewportWidthMargin = GraphicsDevice.Viewport.Width / 2f;

            m_ScreenHeader = new ScreenHeader(this, @"Headers\Game Over", 0.3f, viewportWidthMargin, 50, true);

            TextItem scoreMessage = new TextItem(this, createScoreMessage(i_PlayerScores), new Vector2(viewportWidthMargin, 250),
                         CurrentNumberOfItemsOnScreen(), Color.DarkBlue, Color.AliceBlue, true)
                         { Scales = new Vector2(0.45f, 0.9f) };
            
            TextItem exitButton = new TextItem(this, k_ExitMessage, new Vector2(viewportWidthMargin, 350),
                                      CurrentNumberOfItemsOnScreen(), Color.PaleVioletRed) { KeyRedirection = Keys.Escape };
            AddGameItem(exitButton);
            exitButton.AddToOnClick(exitButton_OnClicked);
            TextItem startButton = new TextItem(this, k_StartMessage, new Vector2(viewportWidthMargin, 450),
                                       CurrentNumberOfItemsOnScreen(), Color.LightSeaGreen) { KeyRedirection = Keys.Home };
            startButton.AddToOnClick(startButton_OnClicked);
            AddGameItem(startButton);
            TextItem menuButton = new TextItem(this, k_MenuMessage, new Vector2(viewportWidthMargin, 550),
                                      CurrentNumberOfItemsOnScreen(), Color.DodgerBlue) { KeyRedirection = Keys.M };
            menuButton.AddToOnClick(menuButton_OnClicked);
            AddGameItem(menuButton);
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
