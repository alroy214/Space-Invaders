using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex03_Lior_204326607_Eitan_316486497;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;

namespace C20_Ex02_Lior_204326607_Eitan_316486497.SpaceInvaders
{
    class LifeCluster
    {
        private readonly Life[] r_Lives;
        private int m_CurrentPlayerLives;

        public LifeCluster(string i_AssetName, GameScreen i_GameScreen, int i_CurrentPlayer, int i_PlayerLives)
        {
            m_CurrentPlayerLives = i_PlayerLives;
            r_Lives = new Life[i_PlayerLives + 1];
            initLives(i_AssetName, i_GameScreen, i_CurrentPlayer);

        }

        private void initLives(string i_AssetName, GameScreen i_GameScreen, int i_CurrentPlayer)
        {
            for (int i = 1; i < r_Lives.Length; i++)
            {
                r_Lives[i] = new Life(i_AssetName, i_GameScreen, i, i_CurrentPlayer);
            }
        }

        public int LifeShuttered()
        {
            if(m_CurrentPlayerLives > 0)
            {
                r_Lives[m_CurrentPlayerLives].Visible = false;
                m_CurrentPlayerLives--;
            }
            return m_CurrentPlayerLives;
        }

        public int LivesRemaining
        {
            get
            {
                return m_CurrentPlayerLives;
            }
        }
    }
}
