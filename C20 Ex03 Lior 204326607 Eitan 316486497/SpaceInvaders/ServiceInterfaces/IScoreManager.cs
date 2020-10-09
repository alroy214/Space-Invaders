//*** Guy Ronen © 2008-2011 ***//
using System;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships;

namespace Infrastructure.ServiceInterfaces
{
    public interface IScoreManager
    {
        Action<int, PlayerShip.ePlayer> ScoreChanged { get; set; }
        
        void UpdateScore(int i_Points, PlayerShip.ePlayer i_Player);

        void AddScoreBoardToUpdate(Action<int, PlayerShip.ePlayer> i_Action);
    }
}
