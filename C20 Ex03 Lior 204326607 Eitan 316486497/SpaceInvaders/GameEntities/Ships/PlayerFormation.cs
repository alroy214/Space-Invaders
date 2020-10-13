using System;
using System.ComponentModel;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.GameEntities.Ships
{
    public class PlayerFormation : GameComponent
    {
        private const string k_AssetName1 = @"Sprites\Ship01_32x32";
        private const string k_PlayerOneDescription = "One";
        private const Keys k_RightKeyPlayer1 = Keys.O;
        private const Keys k_LeftKeyPlayer1 = Keys.I;
        private const Keys k_ShootKeyPlayer1 = Keys.D9;
        private const string k_AssetName2 = @"Sprites\Ship02_32x32";
        private const string k_PlayerTwoDescription = "Two";
        private const Keys k_RightKeyPlayer2 = Keys.E;
        private const Keys k_LeftKeyPlayer2 = Keys.W;
        private const Keys k_ShootKeyPlayer2 = Keys.D3;
        private event EventHandler AllShipDestroyed;
        private readonly PlayerShip[] r_PlayerShips;
        private int m_CurrentLivingPlayers;

        public PlayerFormation(GameScreen i_GameScreen) : base(i_GameScreen.Game)
        {
            ePlayer[] players = (ePlayer[]) Enum.GetValues(typeof(ePlayer));
            IPlayManager playManager = (IPlayManager)i_GameScreen.Game.Services.GetService(typeof(IPlayManager));
            m_CurrentLivingPlayers = Math.Min(playManager.NumberOfPlayers, players.Length);
            r_PlayerShips = new PlayerShip[m_CurrentLivingPlayers];
            int numberInFormation = 0;

            for (int i = 0; i < r_PlayerShips.Length; i++)
            {
                if (playManager.GetNumberOfLives(players[i]) == 0)
                {
                    m_CurrentLivingPlayers--;
                }
                else
                {
                    r_PlayerShips[i] = createShips(i_GameScreen, players[i], numberInFormation++);
                    r_PlayerShips[i].OnShipDestroyed(HandleShipDestroyed);
                }
            }
        }

        public enum ePlayer
        {
            [Description(k_PlayerOneDescription)]
            Player1 = 0,
            [Description(k_PlayerTwoDescription)]
            Player2 = 1
        }

        private static PlayerShip createShips(GameScreen i_GameScreen, ePlayer i_Player, int i_CurrentNumberInFormation)
        {
            Keys currentShootKey = Keys.None;
            Keys currentLeftKey = Keys.None;
            Keys currentRightKey = Keys.None;
            string assetName = "";
            switch (i_Player)
            {
                case ePlayer.Player1:
                    {
                        currentRightKey = k_RightKeyPlayer1;
                        currentLeftKey = k_LeftKeyPlayer1;
                        currentShootKey = k_ShootKeyPlayer1;
                        assetName = k_AssetName1;
                        break;
                    }
                case ePlayer.Player2:
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

            return new PlayerShip(assetName, i_GameScreen, i_Player, i_CurrentNumberInFormation)
                       {
                           CurrentRightKey = currentRightKey,
                           CurrentLeftKey = currentLeftKey,
                           CurrentShootKey = currentShootKey
                       };
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