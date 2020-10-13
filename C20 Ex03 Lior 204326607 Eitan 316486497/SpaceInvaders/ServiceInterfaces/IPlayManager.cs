using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.GameEntities.Ships;

namespace Infrastructure.ServiceInterfaces
{
    public interface IPlayManager
    {
        int NumberOfPlayers { get; set; }

        int PlayDifficultyLevel { get; set; }

        void IncreaseDifficultyLevel();

        void ResetLives();

        void LifeLost(PlayerFormation.ePlayer i_Player);

        int GetNumberOfLives(PlayerFormation.ePlayer i_Player);

        int ToggleNumberOfPlayers();

        int GetEffectiveDifficultyLevel();
    }
}
