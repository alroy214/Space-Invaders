using System;
using System.Linq;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace C20_Ex03_Lior_204326607_Eitan_316486497
{
    public class ScoreBoard : Sprite
    {
        private const string k_AssetName = @"Sprites\Ship01_32x32";
        private const string k_ScorePrefix1 = "P1 Score: ";
        private const string k_ScorePrefix2 = "P2 Score: ";
        private const int k_ScorePositionX1 = 5;
        private const int k_ScorePositionY1 = 5;
        private const int k_ScorePositionX2 = 5;
        private const int k_ScorePositionY2 = 25;
        private readonly PlayerShip.ePlayer r_CurrentPlayer;
        private string m_PlayString;
        private SpriteFont m_Font;
        private int m_PlayerScore;

        public ScoreBoard(Game i_Game, PlayerShip.ePlayer i_Player) : base(k_AssetName, i_Game)
        {
            r_CurrentPlayer = i_Player;
            m_PlayerScore = 0;
        }

        public override void Initialize()
        {
            base.Initialize();
            (Game.Services.GetService(typeof(ScoreManager)) as ScoreManager)?.AddScoreBoardToUpdate(updateScore);
        }

        protected override void InitBounds()
        {
            base.InitBounds();
            switch (r_CurrentPlayer)
            {
                case PlayerShip.ePlayer.Player1:
                    {
                        m_PlayString = k_ScorePrefix1;
                        Position = new Vector2(k_ScorePositionX1, k_ScorePositionY1);
                        TintColor = Color.Blue;
                        break;
                    }
                case PlayerShip.ePlayer.Player2:
                    {
                        m_PlayString = k_ScorePrefix2;
                        Position = new Vector2(k_ScorePositionX2, k_ScorePositionY2);
                        TintColor = Color.Green;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void updateScore(int i_Score, PlayerShip.ePlayer i_Player)
        {
            if(r_CurrentPlayer == i_Player)
            {
                m_PlayerScore = Math.Max(0, m_PlayerScore + i_Score);
            }
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            m_Font = Game.Content.Load<SpriteFont>(@"Fonts\Consolas");
        }

        public override void Draw(GameTime i_GameTime)
        {
            m_SpriteBatch.Begin();
            m_SpriteBatch.DrawString(m_Font, ToString(), Position, TintColor);
            m_SpriteBatch.End();
        }

        public override string ToString()
        {
            return m_PlayString + m_PlayerScore;
        }

        public int PlayerScore
        {
            get
            {
                return m_PlayerScore;
            }
        }
    }
}
