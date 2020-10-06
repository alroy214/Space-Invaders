using System;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities
{
    public abstract class GameEntity : Sprite
    {
        protected GameEntity(string i_AssetName, GameScreen i_GameScreen)
            : base(i_AssetName, i_GameScreen)
        {
        }

        public override void Draw(GameTime i_GameTime)
        {
            if (Visible)
            {
                base.Draw(i_GameTime);
            }
        }

        public virtual bool IsOutOfBounds()
        {
            bool outOfBounds = false;
            float topY = TopLeftPosition.Y;
            float topX = TopLeftPosition.X;

            if(topY < 0 || Game.Window.ClientBounds.Height < topY + Height || topX < 0
               || Game.Window.ClientBounds.Width < topX + Width)
            {
                outOfBounds = true;
            }

            return outOfBounds;
        }

        public interface IScorableEntity
        {
            public int Score { get; }

            public bool Destroyed { get; set; }
        }

        public int GetPixelAt(int i_X, int i_Y)
        {
            return Math.Clamp((i_X - Bounds.X) + (i_Y - Bounds.Y) * Texture.Width, 0, (Texture.Width * Texture.Height) - 1);
        }

        public bool IsPixelsCollided(GameEntity i_SourceSprite)
        {
            bool pixelsCollided = false;
            Color[] destDestColor = new Color[Texture.Width * Texture.Height];
            Color[] sourceDataColor = new Color[i_SourceSprite.Texture.Width * i_SourceSprite.Texture.Height];
            Texture.GetData(destDestColor);
            i_SourceSprite.Texture.GetData(sourceDataColor);
            int xDest = Math.Max(Bounds.X, i_SourceSprite.Bounds.X);
            int xSource = Math.Min(Bounds.X + Bounds.Width, i_SourceSprite.Bounds.X + i_SourceSprite.Bounds.Width);
            int yDest = Math.Max(Bounds.Y, i_SourceSprite.Bounds.Y);
            int ySource = Math.Min(Bounds.Y + Bounds.Height, i_SourceSprite.Bounds.Y + i_SourceSprite.Bounds.Height);

            for (int y = yDest; y < ySource; ++y)
            {
                for (int x = xDest; x < xSource; ++x)
                {
                    Color destColor = destDestColor[GetPixelAt(x, y)];
                    Color sourceColor = sourceDataColor[i_SourceSprite.GetPixelAt(x, y)];

                    if (destColor.A != 0 && sourceColor.A != 0)
                    {
                        pixelsCollided = true;
                    }
                }
            }

            return pixelsCollided;
        }
    }
}
