using System;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships;
using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;

namespace C20_Ex03_Lior_204326607_Eitan_316486497
{
    public class PlayManager : GameService, IPlayManager
    {
        public const int k_DefaultNumberOfPlayers = 2;
        public const int k_DefaultDifficultyLevel = 1;
        public const int k_DefaultNumberOfLives = 3;
        private const int k_LevelModulo = 4;
        private readonly int[] m_CurrentLives;
        private int m_CurrentNumberOfPlayers;
        private int m_PlayDifficultyLevel;

        public PlayManager(Game i_Game) : base(i_Game)
        {
            m_CurrentLives = new int[Enum.GetValues(typeof(PlayerShip.ePlayer)).Length];
            m_CurrentNumberOfPlayers = k_DefaultNumberOfPlayers;
            m_PlayDifficultyLevel = k_DefaultDifficultyLevel;
            ResetLives();
        }

        protected override void RegisterAsService()
        {
            Game.Services.AddService(typeof(IPlayManager), this);
        }

        public void ResetLives()
        {
            for(int i = 0; i < m_CurrentLives.Length; i++)
            {
                m_CurrentLives[i] = k_DefaultNumberOfLives;
            }
        }

        public void LifeLost(PlayerShip.ePlayer i_Player)
        {
            m_CurrentLives[(int)i_Player] = Math.Max(0, m_CurrentLives[(int)i_Player] - 1);
        }

        public int GetNumberOfLives(PlayerShip.ePlayer i_Player)
        {
            return m_CurrentLives[(int)i_Player];
        }

        public void IncreaseDifficultyLevel()
        {
            m_PlayDifficultyLevel++;
        }

        public int ToggleNumberOfPlayers()
        {
            if (m_CurrentNumberOfPlayers == 1)
            {
                m_CurrentNumberOfPlayers++;
            }
            else
            {
                m_CurrentNumberOfPlayers--;
            }

            return m_CurrentNumberOfPlayers;
        }

        public int GetEffectiveDifficultyLevel()
        {
            int modulo = m_PlayDifficultyLevel % k_LevelModulo;
            if (modulo == 0)
            {
                modulo = k_LevelModulo;
            }
            return modulo;
        }

        public int NumberOfPlayers
        {
            get
            {
                return m_CurrentNumberOfPlayers;
            }
            set
            {
                m_CurrentNumberOfPlayers = value;
            }
        }

        public int PlayDifficultyLevel
        {
            get
            {
                return m_PlayDifficultyLevel;
            }
            set
            {
                m_PlayDifficultyLevel = value <= 0 ? 1 : value;
            }
        }
    }
}