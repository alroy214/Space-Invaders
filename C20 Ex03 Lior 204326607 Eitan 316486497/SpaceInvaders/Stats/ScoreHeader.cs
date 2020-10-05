using System;
using System.Threading.Tasks;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Stats
{
    public class ScoreHeader
    {
        private const string k_ThankYouMessage = "Thanks for playing! :)";
        private const string k_MessageBoxTitle = "Game Over!";
        private const string k_MessageBoxButton = "Done";
        private const string k_WinMessagePlayer1 = "Player 1 Wins!";
        private const string k_WinMessagePlayer2 = "Player 2 Wins!";
        private const string k_TieMessage = "It's a tie!";
        private readonly ScoreBoard[] r_ScoreBoards;

        public ScoreHeader(GameScreen i_GameScreen)
        {
            r_ScoreBoards = new ScoreBoard[2];
            r_ScoreBoards[0] = new ScoreBoard(i_GameScreen, PlayerShip.ePlayer.Player1);
            r_ScoreBoards[1] = new ScoreBoard(i_GameScreen, PlayerShip.ePlayer.Player2);
        }

        public async Task GameOverMessage()
        {
            string message = string.Format("{0} {3} {1} {3} {2} {3} {4}",
                r_ScoreBoards[0], r_ScoreBoards[1], getWinningPlayer(), Environment.NewLine, k_ThankYouMessage);

            await MessageBox.Show(k_MessageBoxTitle, message, new[] { k_MessageBoxButton });
        }

        private string getWinningPlayer()
        {
            string winner;

            if (r_ScoreBoards[0].PlayerScore > r_ScoreBoards[1].PlayerScore)
            {
                winner = k_WinMessagePlayer1;
            }
            else if (r_ScoreBoards[0].PlayerScore < r_ScoreBoards[1].PlayerScore)
            {
                winner = k_WinMessagePlayer2;
            }
            else
            {
                winner = k_TieMessage;
            }
            return winner;
        }
    }
}