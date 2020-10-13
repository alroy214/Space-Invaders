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
            Vector4 backGroundTint = Vector4.One;

            base.LoadContent();
            backGroundTint.W = k_BackgroundTintAlpha;
            TintColor = new Color(backGroundTint);
        }

        protected override void InitBounds()
        {
            float scaleX;
            float scaleY;

            base.InitBounds();
            scaleX = Game.GraphicsDevice.Viewport.Width / WidthBeforeScale;
            scaleY = Game.GraphicsDevice.Viewport.Height / HeightBeforeScale;
            Scales = new Vector2(scaleX, scaleY);
            DrawOrder = int.MinValue;
        }
    }
}