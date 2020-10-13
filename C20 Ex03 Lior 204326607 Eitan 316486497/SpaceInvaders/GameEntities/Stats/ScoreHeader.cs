using System;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.GameEntities.Ships;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Stats
{
    public class ScoreHeader : GameComponent
    {
        private const string k_ScorePrefix1 = "P1 Score: ";
        private const string k_ScorePrefix2 = "P2 Score: ";
        private readonly ScoreBoard[] r_ScoreBoards;

        public ScoreHeader(GameScreen i_GameScreen) : base(i_GameScreen.Game)
        {
            PlayerFormation.ePlayer[] players = (PlayerFormation.ePlayer[]) Enum.GetValues(typeof(PlayerFormation.ePlayer));
            int numberOfPlayers = Math.Min(((IPlayManager)i_GameScreen.Game.Services.GetService(typeof(IPlayManager))).NumberOfPlayers, players.Length);
            IScoreManager scoreManager = i_GameScreen.Game.Services.GetService(typeof(IScoreManager)) as IScoreManager;
            r_ScoreBoards = new ScoreBoard[numberOfPlayers];
            for (int i = 0; i < numberOfPlayers; i++)
            {
                r_ScoreBoards[i] = createScoreBoard(i_GameScreen, players[i]);
                scoreManager?.AddScoreBoardToUpdate(r_ScoreBoards[i].UpdateScore);
            }

            scoreManager?.AssignScores();
        }

        private static ScoreBoard createScoreBoard(GameScreen i_GameScreen, PlayerFormation.ePlayer i_Player)
        {
            Color tintColor = Color.Purple;
            string playerString = "";
            switch (i_Player)
            {
                case PlayerFormation.ePlayer.Player1:
                    {
                        playerString = k_ScorePrefix1;
                        tintColor = Color.Blue;
                        break;
                    }
                case PlayerFormation.ePlayer.Player2:
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

            return new ScoreBoard(i_GameScreen, i_Player)
                       {
                           TintColor = tintColor,
                           PlayerString = playerString
                       };
        }
    }
}