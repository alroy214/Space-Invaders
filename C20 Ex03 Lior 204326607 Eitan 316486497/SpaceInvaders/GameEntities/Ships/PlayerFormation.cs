using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships;
using Infrastructure.ObjectModel.Screens;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.GameEntities.Ships
{
    class PlayerFormation
    {
        private event EventHandler AllShipDestroyed;
        private readonly PlayerShip[] r_PlayerShip;
        private int m_CurrentLivingPlayers;

        public PlayerFormation(GameScreen i_GameScreen)
        {
            PlayerShip.ePlayer[] players = ((PlayerShip.ePlayer[]) Enum.GetValues(typeof(PlayerShip.ePlayer)));
            m_CurrentLivingPlayers = players.Length;
            r_PlayerShip = new PlayerShip[m_CurrentLivingPlayers];
            for(int i = 0; i < m_CurrentLivingPlayers; i++)
            {
                r_PlayerShip[i] = new PlayerShip(i_GameScreen, players[i]);
                r_PlayerShip[i].OnShipDestroyed(HandleShipDestroyed);
            }
        }

        public void OnAllShipDestroyed(EventHandler i_EventHandler)
        {
            AllShipDestroyed += i_EventHandler;
        }

        public void HandleShipDestroyed(object sender, EventArgs e)
        {
            if (m_CurrentLivingPlayers == 1)
            {
                AllShipDestroyed?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                m_CurrentLivingPlayers--;
            }
        }
    }
}
