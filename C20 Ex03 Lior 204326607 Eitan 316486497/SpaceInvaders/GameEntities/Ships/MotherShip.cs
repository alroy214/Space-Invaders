using System;
using Infrastructure.ObjectModel.Animators;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships
{
    public class MotherShip : GameEntity, ICollidable2D, GameEntity.IScorableEntity
    {
        public event EventHandler<EventArgs> Disposed;
        private const string k_AssetName = @"Sprites\MotherShip_32x120";
        private const int k_MotherShipPointsValue = 600;
        private const int k_MaxSecondsToAppearance = 15;
        private const int k_MotherShipVelocity = 95;
        private const float k_AnimationBlinkTime = 0.2f;
        private const float k_AnimationDestroyTime = 3.0f;
        private const string k_AnimationDestroyName = "MotherShipDestroyAnimator";
        private readonly Random r_Random;
        private float m_NextAppearanceInSeconds;
        private bool m_IsInDestroyedState;

        public MotherShip(GameScreen i_GameScreen) : base(k_AssetName, i_GameScreen)
        {
            m_TintColor = Color.Red;
            r_Random = new Random();
        }

        public override void Initialize()
        {
            base.Initialize();
            BringToInitPosition();
        }

        public override void Update(GameTime i_GameTime)
        {
            m_NextAppearanceInSeconds -= (float) i_GameTime.ElapsedGameTime.TotalSeconds;
            if (m_NextAppearanceInSeconds <= 0)
            {
                m_Velocity.X = k_MotherShipVelocity;
                Visible = true;
            }

            if (Game.GraphicsDevice.Viewport.Width <= m_Position.X)
            {
                if(!m_IsInDestroyedState)
                {
                    BringToInitPosition();
                }
            }

            base.Update(i_GameTime);
        }

        public void BringToInitPosition()
        {
            Animations.Enabled = false;
            m_IsInDestroyedState = false;
            Visible = false;
            m_Position = new Vector2(0, Texture.Height);
            m_Velocity.X = 0;
            m_NextAppearanceInSeconds = generateRandomTimeToNextAppearance();
        }

        private float generateRandomTimeToNextAppearance()
        {
            return r_Random.Next(k_MaxSecondsToAppearance);
        }

        public override void Collided(ICollidable i_Collidable)
        {
            if (i_Collidable is PlayerBullet)
            {
                if (!Animations.Enabled)
                {
                    Animations.Restart();
                }
            }
        }

        protected override void InitAnimations()
        {
            ShrinkAnimator shrinkAnimator = new ShrinkAnimator(TimeSpan.FromSeconds(k_AnimationDestroyTime));
            BlinkAnimator blinkAnimator = new BlinkAnimator(TimeSpan.FromSeconds(k_AnimationBlinkTime), TimeSpan.FromSeconds(k_AnimationDestroyTime));
            FadeAnimator fadeAnimator = new FadeAnimator(TimeSpan.FromSeconds(k_AnimationDestroyTime));
            CompositeAnimator motherShipDestroyAnimator = new CompositeAnimator(k_AnimationDestroyName, TimeSpan.FromSeconds(k_AnimationDestroyTime), 
                this, shrinkAnimator, blinkAnimator, fadeAnimator);

            motherShipDestroyAnimator.Finished += destructionAnimation_Finished;
            Animations.Add(motherShipDestroyAnimator);
        }

        private void destructionAnimation_Finished(object sender, EventArgs e)
        {
            BringToInitPosition();
        }

        public int Score
        {
            get
            {
                return k_MotherShipPointsValue;
            }
        }

        public bool Destroyed
        {
            get
            {
                return m_IsInDestroyedState;
            }
            set
            {
                m_IsInDestroyedState = value;
            }
        }
    }
}