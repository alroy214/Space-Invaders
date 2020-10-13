using System;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.GameEntities.Ships;
using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;

namespace C20_Ex03_Lior_204326607_Eitan_316486497
{
    public class ScoreManager : GameService, IScoreManager
    {
        private Action<int, PlayerFormation.ePlayer> ScoreChanged;
        private readonly int[] r_PlayerScores;

        public ScoreManager(Game i_Game) : base(i_Game)
        {
            r_PlayerScores = new int[Enum.GetValues(typeof(PlayerFormation.ePlayer)).Length];
        }

        protected override void RegisterAsService()
        {
            Game.Services.AddService(typeof(IScoreManager), this);
        }

        public void ResetScores()
        {
            for(int i = 0; i < r_PlayerScores.Length; i++)
            {
                r_PlayerScores[i] = 0;
            }
        }

        public void AssignScores()
        {
            for (int i = 0; i < r_PlayerScores.Length; i++)
            {
                if (Enum.GetValues(typeof(PlayerFormation.ePlayer)) is PlayerFormation.ePlayer[] players)
                {
                    ScoreChanged?.Invoke(r_PlayerScores[i], players[i]);
                }
            }
        }

        public void UpdateScore(int i_Points, PlayerFormation.ePlayer i_Player)
        {
            int newScore = Math.Max(0, r_PlayerScores[(int)i_Player] + i_Points);
            r_PlayerScores[(int)i_Player] = newScore;
            ScoreChanged?.Invoke(newScore, i_Player);
        }

        public void AddScoreBoardToUpdate(Action<int, PlayerFormation.ePlayer> i_ScoreBoard)
        {
            ScoreChanged += i_ScoreBoard;
        }

        public int[] GetScores(int i_CurrentNumberOfPlayers)
        {
            int[] scores = new int[i_CurrentNumberOfPlayers];
            for(int i = 0; i < i_CurrentNumberOfPlayers; i++)
            {
                scores[i] = r_PlayerScores[i];
            }

            return scores;
        }
    }
}