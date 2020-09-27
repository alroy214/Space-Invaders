using System;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships;
using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;

namespace C20_Ex03_Lior_204326607_Eitan_316486497
{
    public class ScoreManager : GameService, IScoreManager
    {
        public Action<int, PlayerShip.ePlayer> ScoreChanged { get; set; }
        private const int k_PlayerLoseLifeScore = -600;

        public ScoreManager(Game i_Game)
            : base(i_Game)
        {
        }

        public void UpdateScoreForHit(int i_Points, PlayerShip.ePlayer i_Player)
        {
            ScoreChanged?.Invoke(i_Points, i_Player);
        }

        public void UpdateScoreForLosingLife(PlayerShip.ePlayer i_Player)
        {
            ScoreChanged?.Invoke(k_PlayerLoseLifeScore, i_Player);
        }

        public void AddScoreBoardToUpdate(Action<int, PlayerShip.ePlayer> i_ScoreBoard)
        {
            ScoreChanged += i_ScoreBoard;
        }
    }
}