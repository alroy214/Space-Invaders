//*** Guy Ronen © 2008-2011 ***//
using System;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships;

namespace Infrastructure.ServiceInterfaces
{
    public interface IPlayManager
    {
        int NumberOfPlayers { get; set; }

        int PlayDifficultyLevel { get; set; }

        void IncreaseDifficultyLevel();

        void ResetLives();

        void LifeLost(PlayerShip.ePlayer i_Player);

        int GetNumberOfLives(PlayerShip.ePlayer i_Player);

        int ToggleNumberOfPlayers();

        int GetEffectiveDifficultyLevel();
    }
}
