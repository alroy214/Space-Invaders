using System;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities
{
    public class Background : GameEntity
    {
        private const string k_AssetName = @"Sprites\BG_Space01_1024x768";
        private const float k_BackgroundTintAlpha = 0.4f;

        public Background(GameScreen i_GameScreen)
            : base(k_AssetName, i_GameScreen)
        {
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            Vector4 backGroundTint = Vector4.One;

            backGroundTint.W = k_BackgroundTintAlpha;
            TintColor = new Color(backGroundTint);
        }

        protected override void InitBounds()
        {
            base.InitBounds();

            float scaleX = Game.GraphicsDevice.Viewport.Width / WidthBeforeScale;
            float scaleY = Game.GraphicsDevice.Viewport.Height / HeightBeforeScale;
            Scales = new Vector2(scaleX, scaleY);

            DrawOrder = int.MinValue;
        }
    }
}