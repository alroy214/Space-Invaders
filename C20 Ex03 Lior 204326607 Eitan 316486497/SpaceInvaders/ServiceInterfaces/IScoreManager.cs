using System;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.GameEntities.Ships;

namespace Infrastructure.ServiceInterfaces
{
    public interface IScoreManager
    {
        void ResetScores();

        void AssignScores();

        void UpdateScore(int i_Points, PlayerFormation.ePlayer i_Player);

        void AddScoreBoardToUpdate(Action<int, PlayerFormation.ePlayer> i_Action);

        int[] GetScores(int i_CurrentNumberOfPlayers);
    }
}
