using System;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;

namespace C20_Ex03_Lior_204326607_Eitan_316486497
{
    public class ScoreManager : GameService, IScoreManager
    {
        public Action<int, PlayerShip.ePlayer> ScoreChanged { get; set; }
        private readonly int[] r_PlayerScores;

        public ScoreManager(Game i_Game) : base(i_Game)
        {
            r_PlayerScores = new int[Enum.GetValues(typeof(PlayerShip.ePlayer)).Length];
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
            PlayerShip.ePlayer[] players = Enum.GetValues(typeof(PlayerShip.ePlayer)) as PlayerShip.ePlayer[];
            for (int i = 0; i < r_PlayerScores.Length; i++)
            {
                if(players != null)
                {
                    ScoreChanged?.Invoke(r_PlayerScores[i], players[i]);
                }
            }
        }

        public void UpdateScore(int i_Points, PlayerShip.ePlayer i_Player)
        {
            int newScore = Math.Max(0, r_PlayerScores[(int)i_Player] + i_Points);
            r_PlayerScores[(int)i_Player] = newScore;
            ScoreChanged?.Invoke(newScore, i_Player);
        }

        public void AddScoreBoardToUpdate(Action<int, PlayerShip.ePlayer> i_ScoreBoard)
        {
            ScoreChanged += i_ScoreBoard;
        }
    }
}