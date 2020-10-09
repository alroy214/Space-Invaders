using System;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace C20_Ex03_Lior_204326607_Eitan_316486497
{
    public class ScoreBoard : GameEntity
    {
        private const string k_AssetName = @"Screens\Blank";
        private const int k_OffsetX = 5;
        private const int k_OffsetY = 5;
        private readonly PlayerShip.ePlayer r_CurrentPlayer;
        private string m_PlayerString;
        private SpriteFont m_Font;
        private int m_PlayerScore;

        public ScoreBoard(GameScreen i_GameScreen, PlayerShip.ePlayer i_Player) : base(k_AssetName, i_GameScreen)
        {
            r_CurrentPlayer = i_Player;
            m_PlayerScore = 0;
        }

        public override void Initialize()
        {
            base.Initialize();
            (Game.Services.GetService(typeof(IScoreManager)) as IScoreManager)?.AddScoreBoardToUpdate(updateScore);
        }

        private void updateScore(int i_NewScore, PlayerShip.ePlayer i_Player)
        {
            if (r_CurrentPlayer == i_Player)
            {
                m_PlayerScore = i_NewScore;
            }
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            m_Font = Game.Content.Load<SpriteFont>(@"Fonts\Consolas");
        }

        protected override void InitBounds()
        {
            base.InitBounds();
            Position = new Vector2(k_OffsetX, k_OffsetY + m_Font.LineSpacing * (int)r_CurrentPlayer);
        }

        public override void Draw(GameTime i_GameTime)
        {
            m_SpriteBatch.DrawString(m_Font, ToString(), Position, TintColor);
        }

        public override string ToString()
        {
            return m_PlayerString + m_PlayerScore;
        }

        public int PlayerScore
        {
            get
            {
                return m_PlayerScore;
            }
        }

        public string PlayerString
        {
            set
            {
                m_PlayerString = value;
            }
        }
    }
}