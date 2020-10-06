using System;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;

namespace C20_Ex03_Lior_204326607_Eitan_316486497
{
    public class PlayManager : GameService, IPlayManager
    {
        public const int k_DefaultNumberOfPlayers = 2;
        public const int k_DefaultDifficultyLevel = 1;
        public int m_CurrentNumberOfPlayers;
        public int m_PlayDifficultyLevel;

        public PlayManager(Game i_Game)
            : base(i_Game)
        {
            m_CurrentNumberOfPlayers = k_DefaultNumberOfPlayers;
            m_PlayDifficultyLevel = k_DefaultDifficultyLevel;
        }

        protected override void RegisterAsService()
        {
            Game.Services.AddService(typeof(IPlayManager), this);
        }

        public int GetEffectiveDifficultyLevel()
        {
            return m_CurrentNumberOfPlayers % 5;
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