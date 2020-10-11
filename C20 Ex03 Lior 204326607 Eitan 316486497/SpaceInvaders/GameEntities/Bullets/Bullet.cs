using System;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
namespace C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities
{
    public class Bullet : GameEntity, ICollidable2D
    {
        private const string k_AssetName = @"Sprites\Bullet";
        private const float k_InitialBulletVelocity = 140;
        protected IScoreManager m_ScoreManager;

        protected Bullet(GameScreen i_GameScreen) : base(k_AssetName, i_GameScreen)
        {
            Velocity = new Vector2(0, k_InitialBulletVelocity);
            Visible = false;
        }

        public override void Initialize()
        {
            base.Initialize();
            m_ScoreManager = ((IScoreManager)Game.Services.GetService(typeof(IScoreManager)));
        }

        public override void Update(GameTime i_GameTime)
        {
            if (Visible && IsOutOfBounds())
            {
                Visible = false;
            }

            base.Update(i_GameTime);
        }
    }

    public class PlayerBullet : Bullet
    {
        private readonly PlayerShip.ePlayer r_CurrentPlayer;

        public PlayerBullet(GameScreen i_GameScreen, PlayerShip.ePlayer i_CurrentPlayer) : base(i_GameScreen)
        {
            r_CurrentPlayer = i_CurrentPlayer;
            Velocity *= -1;
            TintColor = Color.Red;
        }

        public override void Collided(ICollidable i_Collidable)
        {
            if (i_Collidable is EnemyBullet)
            {
                Visible = false;
            }

            if (i_Collidable is IScorableEntity entity && m_ScoreManager != null && !entity.Destroyed)
            {
                entity.Destroyed = true;
                m_ScoreManager.UpdateScore(entity.Score, r_CurrentPlayer);
                Visible = false;
            }

        }
    }

    public class EnemyBullet : Bullet
    {
        private const int k_PlayerLostLifePenaltyScore = -600;
        private const int k_ProbabilityToShatter = 1;

        public EnemyBullet(GameScreen i_GameScreen) : base(i_GameScreen)
        {
            TintColor = Color.Blue;
        }

        public override void Collided(ICollidable i_Collidable)
        {
            if (i_Collidable is PlayerBullet)
            {
                if (new Random().Next(k_ProbabilityToShatter) == 0)
                {
                    Visible = false;
                }
            }

            if (i_Collidable is PlayerShip playerShip && m_ScoreManager != null)
            {
                Visible = false;
                m_ScoreManager.UpdateScore(k_PlayerLostLifePenaltyScore, playerShip.CurrentPlayer);
            }
        }
    }
}
