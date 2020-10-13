using System;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships
{
    public class MotherShip : GameEntity, ICollidable2D, GameEntity.IScorableEntity
    {
        private const string k_AssetName = @"Sprites\MotherShip_32x120";
        private const int k_MotherShipPointsValue = 600;
        private const int k_MaxSecondsToAppearance = 5;
        private const int k_MotherShipVelocity = 95;
        private const float k_AnimationBlinkTime = 0.2f;
        private const float k_AnimationDestroyTime = 3.0f;
        private readonly ISoundManager r_SoundManager;
        private readonly Random r_Random;
        private float m_NextAppearanceInSeconds;
        private bool m_InDestroyedState;

        public MotherShip(GameScreen i_GameScreen) : base(k_AssetName, i_GameScreen)
        {
            r_SoundManager = (ISoundManager)i_GameScreen.Game.Services.GetService(typeof(ISoundManager));
            m_TintColor = Color.Red;
            r_Random = new Random();
        }

        public override void Initialize()
        {
            base.Initialize();
            BringToInitPosition();
        }

        protected override void ScreenChanged(object i_Sender, EventArgs i_E)
        {
            Position = new Vector2(Math.Clamp(Position.X, 0,
                Game.GraphicsDevice.Viewport.Width - Width), Texture.Height);
        }

        public override void Update(GameTime i_GameTime)
        {
            m_NextAppearanceInSeconds -= (float) i_GameTime.ElapsedGameTime.TotalSeconds;
            if (m_NextAppearanceInSeconds <= 0)
            {
                m_Velocity.X = k_MotherShipVelocity;
                Visible = true;
            }

            if(Game.GraphicsDevice.Viewport.Width <= m_Position.X && !Animations.Enabled)
            {
                BringToInitPosition();
            }

            base.Update(i_GameTime);
        }

        public void BringToInitPosition()
        {
            m_NextAppearanceInSeconds = r_Random.Next(k_MaxSecondsToAppearance);
            m_Position = new Vector2(0, Texture.Height);
            Animations.Enabled = false;
            m_InDestroyedState = false;
            m_Velocity.X = 0;
            Visible = false;
        }

        public override void Collided(ICollidable i_Collidable)
        {
            if (i_Collidable is PlayerBullet && !Animations.Enabled)
            {
                r_SoundManager.PlaySoundEffect(MusicUtils.k_MotherShipKillSound);
                Animations.Restart();
            }
        }

        protected override void InitAnimations()
        {
            ShrinkAnimator shrinkAnimator = new ShrinkAnimator(TimeSpan.FromSeconds(k_AnimationDestroyTime));
            BlinkAnimator blinkAnimator = new BlinkAnimator(TimeSpan.FromSeconds(k_AnimationBlinkTime), TimeSpan.FromSeconds(k_AnimationDestroyTime));
            FadeAnimator fadeAnimator = new FadeAnimator(TimeSpan.FromSeconds(k_AnimationDestroyTime));
            Animations.Add(shrinkAnimator);
            Animations.Add(blinkAnimator);
            Animations.Add(fadeAnimator);
            fadeAnimator.Finished += destructionAnimation_Finished;
            Animations.Restart();
        }

        public override void Draw(GameTime i_GameTime)
        {
            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            m_SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
            base.Draw(i_GameTime);
            m_SpriteBatch.End();
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
                return m_InDestroyedState;
            }
            set
            {
                m_InDestroyedState = value;
            }
        }
    }
}