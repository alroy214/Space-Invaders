using System;
using C20_Ex02_Lior_204326607_Eitan_316486497.SpaceInvaders;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.GameEntities.Ships;
using Infrastructure.ObjectModel.Animators;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships
{
    public class PlayerShip : GameEntity, ICollidable2D
    {
        private event EventHandler ShipDestroyed;
        private const string k_NonFetalHitBlinkerAnimatorName = "PlayerShipNonFatalHitBlinkAnimator";
        private const string k_NonFetalHitAnimatorName = "PlayerShipNonFetalHitAnimator";
        private const string k_FetalHitAnimatorName = "PlayerShipFetalHitAnimator";
        private const float k_NonFatalHitAnimationTime = 2.0f;
        private const float k_NonFatalBlinkerTime = 0.125f;
        private const float k_FatalHitAnimationTime = 2.6f;
        private const int k_NumberOfRotationRoundsPerSecond = 6;
        private const float k_Velocity = 140f;
        private const int k_NumberOfBullets = 2;
        private const int k_MarginBottom = 30;
        private readonly BulletMagazine r_BulletMagazine;
        private readonly PlayerFormation.ePlayer r_CurrentPlayer;
        private readonly LifeCluster r_LifeCluster;
        private readonly ISoundManager r_SoundManager;
        private readonly IPlayManager r_PlayManager;
        private readonly int r_NumberInFormation;
        private CompositeAnimator m_FetalHitAnimator;
        private bool m_KeyboardPressLock;
        private bool m_MouseClickLock;
        private Keys m_CurrentRightKey;
        private Keys m_CurrentLeftKey;
        private Keys m_CurrentShootKey;

        public PlayerShip(string i_AssetName, GameScreen i_GameScreen, PlayerFormation.ePlayer i_Player, int i_NumberInFormation) : base(i_AssetName, i_GameScreen)
        {
            r_SoundManager = (ISoundManager)i_GameScreen.Game.Services.GetService(typeof(ISoundManager));
            r_PlayManager = (IPlayManager)i_GameScreen.Game.Services.GetService(typeof(IPlayManager));
            r_BulletMagazine = new PlayerBulletMagazine(i_GameScreen, k_NumberOfBullets, i_Player);
            int numberOfLives = r_PlayManager.GetNumberOfLives(i_Player);
            r_LifeCluster = new LifeCluster(AssetName, i_GameScreen, (int)i_Player, numberOfLives);
            r_NumberInFormation = i_NumberInFormation;
            r_CurrentPlayer = i_Player;
        }

        protected override void ScreenChanged(object i_Sender, EventArgs i_E)
        {
            Position = new Vector2(Math.Clamp(Position.X, getStartingPosition().X, 
                Game.GraphicsDevice.Viewport.Width - Width), getStartingPosition().Y);
        }


        public void OnShipDestroyed(EventHandler i_EventHandler)
        {
            ShipDestroyed += i_EventHandler;
        }

        public override void Update(GameTime i_GameTime)
        {
            keyboardMovement();
            if (r_CurrentPlayer == PlayerFormation.ePlayer.Player1)
            {
                mouseMovement();
            }

            shootBullet();
            base.Update(i_GameTime);
        }

        private void keyboardMovement()
        {
            if (GameScreen.InputManager.KeyHeld(m_CurrentLeftKey))
            {
                m_Velocity.X = k_Velocity * -1;
            }
            else if (GameScreen.InputManager.KeyHeld(m_CurrentRightKey))
            {
                m_Velocity.X = k_Velocity;
            }
            else
            {
                m_Velocity.X = 0;
            }

            Position = new Vector2(MathHelper.Clamp(Position.X,
                0, GraphicsDevice.Viewport.Width - Width), Position.Y);
        }

        private void mouseMovement()
        {
            Position = new Vector2(Math.Clamp(Position.X + GameScreen.InputManager.MousePositionDelta.X, 0,
                GraphicsDevice.Viewport.Width - Texture.Width), Position.Y);
        }

        private void shootBullet()
        {
            if (r_LifeCluster.LivesRemaining != 0)
            {
                if (!m_KeyboardPressLock && GameScreen.InputManager.KeyPressed(m_CurrentShootKey) ||
                   r_CurrentPlayer == PlayerFormation.ePlayer.Player1 && !m_MouseClickLock &&
                   GameScreen.InputManager.MouseState.LeftButton == ButtonState.Pressed)
                {
                    createBullet();
                }

                m_MouseClickLock = GameScreen.InputManager.MouseState.LeftButton == ButtonState.Pressed;
                m_KeyboardPressLock = GameScreen.InputManager.KeyPressed(Keys.Enter);
            }
        }

        private void createBullet()
        {
            if (r_BulletMagazine.ShootBullet(new Vector2(m_Position.X + Width / 2, m_Position.Y - Height / 2)))
            {
                r_SoundManager.PlaySoundEffect(MusicUtils.k_PlayerShipShootSound);
            }
        }

        private void bringToMostLeftPosition()
        {
            m_Position = new Vector2(0, GraphicsDevice.Viewport.Height - Texture.Height - k_MarginBottom);
        }

        protected override void InitBounds()
        {
            base.InitBounds();
            m_Position = getStartingPosition();
        }

        private Vector2 getStartingPosition()
        {
            return new Vector2(r_NumberInFormation * Width, GraphicsDevice.Viewport.Height - Texture.Height - k_MarginBottom);
        }

        public override void Collided(ICollidable i_Collidable)
        {
            base.Collided(i_Collidable);
            if (i_Collidable is EnemyBullet)
            {
                shipLifeLost();
            }
        }

        private void shipLifeLost()
        {
            r_SoundManager.PlaySoundEffect(MusicUtils.k_LifeDieSound);
            r_PlayManager.LifeLost(CurrentPlayer);
            if (r_LifeCluster.LifeShuttered() == 0)
            {
                Animations = m_FetalHitAnimator;
                Animations.Resume();
            }
            else
            {
                Animations.Restart();
                bringToMostLeftPosition();
            }
        }

        protected override void InitAnimations()
        {
            Animations.Enabled = true;
            initNonFatalHitAnimation();
            initFatalHitAnimation();
        }

        private void initNonFatalHitAnimation()
        {
            CompositeAnimator nonFetalHitAnimator = new CompositeAnimator(this, k_NonFetalHitAnimatorName);
            BlinkAnimator blinkAnimator = new BlinkAnimator(k_NonFetalHitBlinkerAnimatorName, 
                TimeSpan.FromSeconds(k_NonFatalBlinkerTime),
                TimeSpan.FromSeconds(k_NonFatalHitAnimationTime));
            nonFetalHitAnimator.Add(blinkAnimator);
            Animations = nonFetalHitAnimator;
        }

        private void initFatalHitAnimation()
        {
            RotateAnimator rotateAnimator = new RotateAnimator(TimeSpan.FromSeconds(k_FatalHitAnimationTime),
                k_NumberOfRotationRoundsPerSecond);
            FadeAnimator fadeAnimator = new FadeAnimator(TimeSpan.FromSeconds(k_FatalHitAnimationTime));
            m_FetalHitAnimator = new CompositeAnimator(k_FetalHitAnimatorName, 
                TimeSpan.FromSeconds(k_FatalHitAnimationTime), 
                this, fadeAnimator, rotateAnimator);
            m_FetalHitAnimator.Finished += fatalHitAnimation_Finished;
        }

        protected override void InitSourceRectangle()
        {
            base.InitSourceRectangle();
            CenterRectangle();
        }

        private void fatalHitAnimation_Finished(object sender, EventArgs e)
        {
            shipShattered();
            ShipDestroyed?.Invoke(this, EventArgs.Empty);
        }

        private void shipShattered()
        {
            Enabled = false;
            Visible = false;
        }

        public PlayerFormation.ePlayer CurrentPlayer
        {
            get
            {
                return r_CurrentPlayer;
            }
        }

        public Keys CurrentRightKey
        {
            set
            {
                m_CurrentRightKey = value;
            }
        }

        public Keys CurrentLeftKey
        {
            set
            {
                m_CurrentLeftKey = value;
            }
        }

        public Keys CurrentShootKey
        {
            set
            {
                m_CurrentShootKey = value;
            }
        }
    }
}