using System;
using System.Linq;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace C20_Ex03_Lior_204326607_Eitan_316486497
{
    public class Life : GameEntity
    {
        private const float k_Opacity = 0.5f;
        private const int k_InitTopMargin = 10;
        private const int k_LifeTopMargin = 5;
        private const float k_ScaleX = 0.5f;
        private const float k_ScaleY = 0.5f;
        private const float k_LeftMarginMultiplier = 1.5f;
        private readonly int r_NumberInCluster;
        private readonly int r_NumberOfPlayer;

        public Life(string i_AssetName, GameScreen i_GameScreen, int i_NumberInCluster, int i_NumberOfPlayer) : base(i_AssetName, i_GameScreen)
        {
            r_NumberInCluster = i_NumberInCluster;
            r_NumberOfPlayer = i_NumberOfPlayer;
        }
        
        public override void Draw(GameTime i_GameTime)
        {
            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            m_SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
            base.Draw(i_GameTime);
            m_SpriteBatch.End();
        }
        
        protected override void InitBounds()
        {
            base.InitBounds();
            Opacity = k_Opacity;
            Scales = new Vector2(k_ScaleX, k_ScaleY);
            Position = new Vector2(Game.Window.ClientBounds.Width - (Width * r_NumberInCluster * k_LeftMarginMultiplier),
                k_InitTopMargin + r_NumberOfPlayer * (k_LifeTopMargin + Height));
        }

    }
}
