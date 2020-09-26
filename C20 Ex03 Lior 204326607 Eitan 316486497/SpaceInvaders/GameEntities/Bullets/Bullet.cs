using System;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities
{
    public class Bullet : GameEntity, ICollidable2D
    {
        private const string k_AssetName = @"Sprites\Bullet";
        private const float k_InitialBulletVelocity = 140;
        protected ScoreManager m_ScoreManager;

        protected Bullet(Game i_Game)
            : base(k_AssetName, i_Game)
        {
            Velocity = new Vector2(0, k_InitialBulletVelocity);
            Visible = false;
        }

        public override void Initialize()
        {
            base.Initialize();
            m_ScoreManager = Game.Services.GetService(typeof(ScoreManager)) as ScoreManager;
        }

        public override void Update(GameTime i_GameTime)
        {
            if(Visible && IsOutOfBounds())
            {
                Visible = false;
            }

            base.Update(i_GameTime);
        }
    }

    public class PlayerBullet : Bullet
    {
        private readonly PlayerShip.ePlayer r_CurrentPlayer;

        public PlayerBullet(Game i_Game, PlayerShip.ePlayer i_CurrentPlayer)
            : base(i_Game)
        {
            r_CurrentPlayer = i_CurrentPlayer;
            Velocity *= -1;
            TintColor = Color.Red;
        }

        public override void Collided(ICollidable i_Collidable)
        {
            if(i_Collidable is EnemyBullet)
            {
                Visible = false;
            }

            if(i_Collidable is IScorableEntity entity && m_ScoreManager != null && !entity.Destroyed)
            {
                entity.Destroyed = true;
                m_ScoreManager.UpdateScoreForHit(entity.Score, r_CurrentPlayer);
                Visible = false;
            }

        }
    }

    public class EnemyBullet : Bullet
    {
        private const int k_ProbabilityToShatter = 1;

        public EnemyBullet(Game i_Game)
            : base(i_Game)
        {
            TintColor = Color.Blue;
        }

        public override void Collided(ICollidable i_Collidable)
        {
            if(i_Collidable is PlayerBullet)
            {
                if(new Random().Next(k_ProbabilityToShatter) == 0)
                {
                    Visible = false;
                }
            }

            if(i_Collidable is PlayerShip playerShip && m_ScoreManager != null)
            {
                m_ScoreManager.UpdateScoreForLosingLife(playerShip.CurrentPlayer);
                Visible = false;
            }
        }
    }
}
