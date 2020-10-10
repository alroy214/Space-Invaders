using System;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Stats
{
    public class ScoreHeader
    {
        private const string k_ScorePrefix1 = "P1 Score: ";
        private const string k_ScorePrefix2 = "P2 Score: ";
        private readonly ScoreBoard[] r_ScoreBoards;

        public ScoreHeader(GameScreen i_GameScreen)
        {
            PlayerShip.ePlayer[] players = ((PlayerShip.ePlayer[])Enum.GetValues(typeof(PlayerShip.ePlayer)));

            int numberOfPlayers  =
                i_GameScreen.Game.Services.GetService(typeof(IPlayManager)) is IPlayManager playManager
                    ? Math.Min(playManager.NumberOfPlayers, players.Length)
                    : Math.Min(PlayManager.k_DefaultNumberOfPlayers, players.Length);


            IScoreManager scoreManager = i_GameScreen.Game.Services.GetService(typeof(IScoreManager)) as IScoreManager;

            r_ScoreBoards = new ScoreBoard[numberOfPlayers];
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Color tintColor = Color.Purple;
                string playerString = "";
                switch (players[i])
                {
                    case PlayerShip.ePlayer.Player1:
                        {
                            playerString = k_ScorePrefix1;
                            tintColor = Color.Blue;
                            break;
                        }
                    case PlayerShip.ePlayer.Player2:
                        {
                            playerString = k_ScorePrefix2;
                            tintColor = Color.Green;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                r_ScoreBoards[i] = new ScoreBoard(i_GameScreen, players[i])
                                       {
                                           TintColor = tintColor,
                                           PlayerString = playerString
                                       };
                scoreManager?.AddScoreBoardToUpdate(r_ScoreBoards[i].UpdateScore);
            }

            scoreManager?.AssignScores();
        }

        public int[] GetPlayerScores()
        {
            int[] playerScores = new int[r_ScoreBoards.Length];

            for(int i = 0; i < r_ScoreBoards.Length; i++)
            {
                playerScores[i] = r_ScoreBoards[i].PlayerScore;
            }

            return playerScores;
        }

    }
}