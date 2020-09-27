using System;
using C20_Ex02_Lior_204326607_Eitan_316486497.SpaceInvaders;
using Infrastructure.ObjectModel.Animators;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships
{
    public class PlayerShip : GameEntity, ICollidable2D
    {
        private event EventHandler ShipDestroyed;
        private const string k_AssetName1 = @"Sprites\Ship01_32x32";
        private const Keys k_RightKeyPlayer1 = Keys.P;
        private const Keys k_LeftKeyPlayer1 = Keys.I;
        private const Keys k_ShootKeyPlayer1 = Keys.D9;
        private const string k_AssetName2 = @"Sprites\Ship02_32x32";
        private const Keys k_RightKeyPlayer2 = Keys.R;
        private const Keys k_LeftKeyPlayer2 = Keys.W;
        private const Keys k_ShootKeyPlayer2 = Keys.D3;
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
        private const int k_DefaultNumberOfLives = 3;
        private readonly BulletMagazine r_BulletMagazine;
        private readonly ePlayer r_CurrentPlayer;
        private readonly LifeCluster r_LifeCluster;
        private CompositeAnimator m_FetalHitAnimator;
        private IInputManager m_InputManager;
        private bool m_KeyboardPressLock;
        private bool m_MouseClickLock;
        private readonly Keys r_CurrentRightKey;
        private readonly Keys r_CurrentLeftKey;
        private readonly Keys r_CurrentShootKey;

        public PlayerShip(Game i_Game, ePlayer i_Player) : base(i_Player == ePlayer.Player1 ? k_AssetName1 : k_AssetName2, i_Game)
        {
            r_BulletMagazine = new PlayerBulletMagazine(i_Game, k_NumberOfBullets, i_Player);
            r_LifeCluster = new LifeCluster(AssetName, i_Game, (int) i_Player, k_DefaultNumberOfLives);
            r_CurrentPlayer = i_Player;
            switch (r_CurrentPlayer)
            {
                case ePlayer.Player1:
                    {
                        r_CurrentRightKey = k_RightKeyPlayer1;
                        r_CurrentLeftKey = k_LeftKeyPlayer1;
                        r_CurrentShootKey = k_ShootKeyPlayer1;
                        break;
                    }
                case ePlayer.Player2:
                    {
                        r_CurrentRightKey = k_RightKeyPlayer2;
                        r_CurrentLeftKey = k_LeftKeyPlayer2;
                        r_CurrentShootKey = k_ShootKeyPlayer2;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        public enum ePlayer
        {
            Player1,
            Player2
        }

        public override void Initialize()
        {
            base.Initialize();
            m_InputManager = Game.Services.GetService(typeof(IInputManager)) as IInputManager;
            ShipDestroyed += ((Invaders)Game).HandleGameOver;
        }

        public override void Update(GameTime i_GameTime)
        {
            keyboardMovement();
            if (r_CurrentPlayer == ePlayer.Player1)
            {
                mouseMovement();
            }

            shootBullet();
            base.Update(i_GameTime);
        }

        private void keyboardMovement()
        {
            float positionX;

            if (m_InputManager.KeyHeld(r_CurrentLeftKey))
            {
                m_Velocity.X = k_Velocity * -1;
            }
            else if (m_InputManager.KeyHeld(r_CurrentRightKey))
            {
                m_Velocity.X = k_Velocity;
            }
            else
            {
                m_Velocity.X = 0;
            }

            positionX = MathHelper.Clamp(Position.X, 0, GraphicsDevice.Viewport.Width - Width);
            Position = new Vector2(positionX, Position.Y);
        }

        private void mouseMovement()
        {
            Position = new Vector2(Math.Clamp(Position.X + m_InputManager.MousePositionDelta.X, 0, GraphicsDevice.Viewport.Width - Texture.Width), Position.Y);
        }

        private void shootBullet()
        {
            if (r_LifeCluster.LivesRemaining != 0)
            {
                if (!m_KeyboardPressLock && m_InputManager.KeyPressed(r_CurrentShootKey) ||
                   r_CurrentPlayer == ePlayer.Player1 && !m_MouseClickLock && m_InputManager.MouseState.LeftButton == ButtonState.Pressed)
                {
                    createBullet();
                }

                m_MouseClickLock = m_InputManager.MouseState.LeftButton == ButtonState.Pressed;
                m_KeyboardPressLock = m_InputManager.KeyPressed(Keys.Enter);
            }
        }

        private void createBullet()
        {
            r_BulletMagazine.ShootBullet(new Vector2(m_Position.X + Width / 2, m_Position.Y - Height / 2));
        }

        private void bringToOriginPosition()
        {
            Position = m_OriginPosition;
        }

        public override void Collided(ICollidable i_Collidable)
        {
            base.Collided(i_Collidable);
            if (i_Collidable is EnemyBullet)
            {
                if (r_LifeCluster.LifeShuttered() == 0)
                {
                    Animations = m_FetalHitAnimator;
                    Animations.Resume();
                }
                else
                {
                    Animations.Restart();
                    bringToOriginPosition();
                }
            }
        }

        protected override void InitBounds()
        {
            base.InitBounds();
            m_Position = new Vector2(GraphicsDevice.Viewport.Width - Texture.Width,
                GraphicsDevice.Viewport.Height - Texture.Height - k_MarginBottom);
            InitOrigins();
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
                TimeSpan.FromSeconds(k_NonFatalBlinkerTime), TimeSpan.FromSeconds(k_NonFatalHitAnimationTime));

            nonFetalHitAnimator.Add(blinkAnimator);
            Animations = nonFetalHitAnimator;
        }

        private void initFatalHitAnimation()
        {
            RotateAnimator rotateAnimator = new RotateAnimator(TimeSpan.FromSeconds(k_FatalHitAnimationTime), k_NumberOfRotationRoundsPerSecond);
            FadeAnimator fadeAnimator = new FadeAnimator(TimeSpan.FromSeconds(k_FatalHitAnimationTime));

            m_FetalHitAnimator = new CompositeAnimator(k_FetalHitAnimatorName, TimeSpan.FromSeconds(k_FatalHitAnimationTime), 
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
            Dispose();
        }

        public ePlayer CurrentPlayer
        {
            get
            {
                return r_CurrentPlayer;
            }
        }
    }
}