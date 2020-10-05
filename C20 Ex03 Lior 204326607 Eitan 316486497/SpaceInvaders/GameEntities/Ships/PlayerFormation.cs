using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.GameEntities.Ships
{
    class PlayerFormation
    {
        private const string k_AssetName1 = @"Sprites\Ship01_32x32";
        private const Keys k_RightKeyPlayer1 = Keys.P;
        private const Keys k_LeftKeyPlayer1 = Keys.I;
        private const Keys k_ShootKeyPlayer1 = Keys.D9;
        private const string k_AssetName2 = @"Sprites\Ship02_32x32";
        private const Keys k_RightKeyPlayer2 = Keys.R;
        private const Keys k_LeftKeyPlayer2 = Keys.W;
        private const Keys k_ShootKeyPlayer2 = Keys.D3;
        private event EventHandler AllShipDestroyed;
        private readonly PlayerShip[] r_PlayerShips;
        private int m_CurrentLivingPlayers;

        public PlayerFormation(GameScreen i_GameScreen)
        {
            PlayerShip.ePlayer[] players = ((PlayerShip.ePlayer[]) Enum.GetValues(typeof(PlayerShip.ePlayer)));
            m_CurrentLivingPlayers = players.Length;
            r_PlayerShips = new PlayerShip[m_CurrentLivingPlayers];

            for(int i = 0; i < m_CurrentLivingPlayers; i++)
            {
                Keys currentShootKey = Keys.None;
                Keys currentLeftKey = Keys.None;
                Keys currentRightKey = Keys.None;
                string assetName = "";

                switch(players[i])
                {
                    case PlayerShip.ePlayer.Player1:
                        {
                            currentRightKey = k_RightKeyPlayer1;
                            currentLeftKey = k_LeftKeyPlayer1;
                            currentShootKey = k_ShootKeyPlayer1;
                            assetName = k_AssetName1;
                            break;
                        }
                    case PlayerShip.ePlayer.Player2:
                        {
                            currentRightKey = k_RightKeyPlayer2;
                            currentLeftKey = k_LeftKeyPlayer2;
                            currentShootKey = k_ShootKeyPlayer2;
                            assetName = k_AssetName2;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                r_PlayerShips[i] = new PlayerShip(assetName, i_GameScreen, players[i])
                                      {
                                          CurrentRightKey = currentRightKey,
                                          CurrentLeftKey = currentLeftKey,
                                          CurrentShootKey = currentShootKey
                                      };

                r_PlayerShips[i].OnShipDestroyed(HandleShipDestroyed);
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
