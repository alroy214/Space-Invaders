using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders;
using Infrastructure.ObjectModel.Screens;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities
{
    public class Barrier : GameEntity, ICollidable2D
    {
        public event EventHandler<EventArgs> Disposed;
        private const string k_AssetName = @"Sprites\Barrier_44x32";
        private const float k_BulletRedactionPercentage = 0.35f;
        private const int k_BottomMarginBounds = 62;
        private const int k_ButtonMarginMultiplier = 2;
        private const float k_RightMarginMultiplier = 2.6f;
        private const float k_BarrierVelocity = 35f;
        private const float k_BarrierVelocityLevelMultiplier = 1.06f;
        private const int k_BarrierVelocityImmobileLevel = 1;
        private const int k_BarrierVelocityThresholdLevel = 2;
        private readonly int r_NumberInCluster;
        private readonly int r_TotalNumberInCluster;
        private readonly ISoundManager r_SoundManager;
        private Vector2 m_InitPosition;

        public Barrier(GameScreen i_GameScreen, int i_NumberInCluster, int i_TotalNumberInCluster)
            : base(k_AssetName, i_GameScreen)
        {
            r_SoundManager = i_GameScreen.Game.Services.GetService(typeof(ISoundManager)) as ISoundManager;
            r_NumberInCluster = i_NumberInCluster;
            r_TotalNumberInCluster = i_TotalNumberInCluster;
            float leveledVelocity = k_BarrierVelocity;
            if (i_GameScreen.Game.Services.GetService(typeof(IPlayManager)) is IPlayManager playerManager)
            {
                int currentDifficultyLevel = playerManager.GetEffectiveDifficultyLevel();
                if (currentDifficultyLevel == k_BarrierVelocityImmobileLevel)
                {
                    leveledVelocity = 0;
                }
                else if(currentDifficultyLevel != k_BarrierVelocityThresholdLevel)
                {
                    leveledVelocity = k_BarrierVelocity * (float) Math.Pow(k_BarrierVelocityLevelMultiplier, currentDifficultyLevel - k_BarrierVelocityThresholdLevel);
                }
            }
            Velocity = new Vector2(leveledVelocity, 0);
        }

        protected override void InitBounds()
        {
            base.InitBounds();
            
            if (r_NumberInCluster >= 0)
            {
                m_Position = new Vector2(
                    (float)GraphicsDevice.Viewport.Width / r_TotalNumberInCluster
                    + r_NumberInCluster * Width * k_RightMarginMultiplier,
                    GraphicsDevice.Viewport.Height - k_BottomMarginBounds - (k_ButtonMarginMultiplier * Height));
            }

            Color[] colorData = new Color[Texture.Height * Texture.Width];
            Texture.GetData(colorData);
            Texture = new Texture2D(Game.GraphicsDevice, (int)Width, (int)Height);
            Texture.SetData(colorData);
        }

        public override void Update(GameTime i_GameTime)
        {
            if (!m_Initialize)
            {
                m_InitPosition = Position;
                m_Initialize = true;
            }

            moveBarrier();
            base.Update(i_GameTime);
        }

        private void moveBarrier()
        {
            if (Position.X - m_InitPosition.X >= Width / 2
               || (Position.X + (Width / 2) <= m_InitPosition.X))
            {
                Velocity *= -1;
            }
        }

        protected override void InitOrigins()
        {
            m_PositionOrigin = new Vector2(Width / 2, Texture.Height);
            base.InitOrigins();
        }

        public override void Collided(ICollidable i_Collidable)
        {
            r_SoundManager.PlaySoundEffect(MusicUtils.k_BarrierHitSound);

            if (i_Collidable is Bullet bullet)
            {
                if (IsPixelsCollided(bullet))
                {
                    colliededWithBullet(bullet);
                    bullet.Visible = false;
                }
            }
            else if (i_Collidable is Enemy enemy)
            {
                if (IsPixelsCollided(enemy))
                {
                    colliededWithEnemy(enemy);
                }
            }
        }

        private void colliededWithEnemy(Enemy i_Enemy)
        {
            Rectangle intersectRectangle = Rectangle.Intersect(Bounds, i_Enemy.Bounds);
            Color[] colorData = new Color[Texture.Width * Texture.Height];
            Color[] colliededColorData = new Color[i_Enemy.Texture.Width * i_Enemy.Texture.Height];
            float leftHitX = intersectRectangle.Left;
            float rightHitX = intersectRectangle.Right;
            float topHitY = intersectRectangle.Top;
            float bottomHitY = intersectRectangle.Bottom;
            Texture.GetData(colorData);
            i_Enemy.Texture.GetData(colliededColorData);

            for (int y = (int)topHitY; y <= bottomHitY; y++)
            {
                for (int x = (int)leftHitX; x < rightHitX; x++)
                {
                    int colorIndex = Math.Clamp(x - Bounds.Left + ((y - Bounds.Top) * Texture.Width), 0, Texture.Width * Texture.Height - 1);
                    int colorEnemyIndex = Math.Clamp(x - i_Enemy.Bounds.Left + ((y - i_Enemy.Bounds.Top) * i_Enemy.Texture.Width), 0,
                        i_Enemy.Texture.Width * i_Enemy.Texture.Height - 1);

                    if (colorData[colorIndex].A != 0 &&
                         colliededColorData[colorEnemyIndex].A != 0)
                        {
                            colorData[colorIndex] = Color.Transparent;
                        }
                }
            }

            Texture.SetData(colorData);
        }

        private void colliededWithBullet(Bullet i_Bullet)
        {
            Rectangle intersectRectangle = Rectangle.Intersect(Bounds, i_Bullet.Bounds);
            Color[] colorData = new Color[Texture.Width * Texture.Height];
            float leftHitX = intersectRectangle.Left - TopLeftPosition.X;
            float rightHitX = intersectRectangle.Right - TopLeftPosition.X;
            float topHitY = intersectRectangle.Top - TopLeftPosition.Y;
            float bottomHitY = intersectRectangle.Bottom - TopLeftPosition.Y;
            Texture.GetData(colorData);

            if (i_Bullet is PlayerBullet)
            {
                topHitY = Math.Min(bottomHitY - i_Bullet.Height * k_BulletRedactionPercentage, topHitY);
            }
            else
            {
                bottomHitY = Math.Max(topHitY + i_Bullet.Height * k_BulletRedactionPercentage, bottomHitY);
            }

            for (int y = (int) topHitY; y < bottomHitY; y++)
            {
                for (int x = (int) leftHitX; x < rightHitX; x++)
                {
                    int colorIndex = Math.Clamp(x + (y * Texture.Width), 0, Texture.Width * Texture.Height - 1);
                    colorData[colorIndex] = Color.Transparent;
                }
            }

            Texture.SetData(colorData);
        }
    }
}
