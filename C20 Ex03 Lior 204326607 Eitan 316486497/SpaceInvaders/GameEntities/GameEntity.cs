using System;
using System.Collections.Generic;
using System.Numerics;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships;
using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Vector4 = Microsoft.Xna.Framework.Vector4;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities
{
    public abstract class GameEntity : Sprite
    {
        private Color[] m_Pixels;

        protected GameEntity(string i_AssetName, Game i_Game)
            : base(i_AssetName, i_Game)
        {
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            Pixels = new Color[Texture.Width * Texture.Height];
            Texture.GetData(Pixels);
        }

        public override void Draw(GameTime i_GameTime)
        {
            if(Visible)
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

        public Color[] Pixels
        {
            get
            {
                return m_Pixels;
            }
            set
            {
                m_Pixels = value;
            }
        }

        public bool IsPixelsCollided(Sprite i_SourceSprite)
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
                    Color destColor = destDestColor[(x - Bounds.X) + (y - Bounds.Y) * Texture.Width];
                    Color sourceColor = sourceDataColor[(x - i_SourceSprite.Bounds.X) + (y - i_SourceSprite.Bounds.Y) * i_SourceSprite.Texture.Width];

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
