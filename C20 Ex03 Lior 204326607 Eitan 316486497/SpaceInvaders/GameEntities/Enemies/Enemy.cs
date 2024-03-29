﻿using System;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders;
using Infrastructure.ObjectModel.Animators;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities
{
    public class Enemy : GameEntity, ICollidable2D, GameEntity.IScorableEntity
    {
        private event EventHandler EnemyWentBelowBorder;
        private event Action<bool, bool, bool> EnemyDied;
        private event Action TurnEnemies;
        private const string k_EnemyCollectionAssetName = @"Sprites\EnemyCollection_64x92";
        private const int k_NumberOfCells = 2;
        private const int k_NumberOfTypes = 3;
        private const int k_MaxNumberOfBullets = 1;
        private const int k_RandomShootValue = 700;
        private const float k_AnimationDestroyTime = 1.7f;
        private const int k_NumberOfAnimationRoundsPerSecond = 5;
        private const string k_AnimationDestroyName = "EnemyDestroyAnimator";
        private const EnemyMatrix.eDirection k_InitDirection = EnemyMatrix.eDirection.Right;
        private const float k_EnemyBelowMarginMultiplier = (float)1.6;
        private const float k_EnemyJumpTimeMultiplier = (float)0.95;
        private const int k_EnemyPixelHeight = 32;
        private const int k_ShipPixelHeight = 32;
        private const int k_ShipAbovePixelHeight = 32;
        private const int k_ShipBelowPixelHeight = 30;
        private const int k_JumpDownMargin = 56;
        private const float k_InitJumpTime = 0.5f;
        private const int k_MatrixTopMargin = k_ShipPixelHeight + k_ShipAbovePixelHeight + k_ShipBelowPixelHeight;
        private readonly int r_RowPosition;
        private readonly int r_ColPosition;
        private readonly EnemyMatrix.eEnemyType r_EnemyType;
        private readonly BulletMagazine r_BulletMagazine;
        private readonly ISoundManager r_SoundManager;
        private readonly Random r_Random;
        private readonly int r_EnemyPoints;
        private EnemyMatrix.eDirection m_CurrentDirection;
        private CompositeAnimator m_EnemyDestroyAnimator;
        private CellAnimator m_CellAnimator;
        private float m_CurrentJumpTime;
        private float m_NextJumpTime;
        private float m_WidthJump;
        private bool m_IsMostRight;
        private bool m_IsMostLeft;
        private bool m_IsMostBottom;
        private bool m_Destroyed;

        public Enemy(GameScreen i_GameScreen, EnemyMatrix.eEnemyType i_EnemyType, Color i_TintColor, int i_EnemyPoints, int i_RowPosition, int i_ColPosition)
            : base(k_EnemyCollectionAssetName, i_GameScreen)
        {
            r_SoundManager = (ISoundManager)i_GameScreen.Game.Services.GetService(typeof(ISoundManager));
            r_Random = new Random();
            r_EnemyType = i_EnemyType;
            TintColor = i_TintColor;
            r_EnemyPoints = i_EnemyPoints;
            r_RowPosition = i_RowPosition;
            r_ColPosition = i_ColPosition;
            m_CurrentJumpTime = k_InitJumpTime;
            m_CurrentDirection = k_InitDirection;
            r_BulletMagazine = new EnemyBulletMagazine(i_GameScreen, k_MaxNumberOfBullets);
            m_IsMostRight = false;
            m_IsMostLeft = false;
            m_Destroyed = false;
        }

        protected override void ScreenChanged(object i_Sender, EventArgs i_E)
        {
        }

        private Vector2 getStartingPosition()
        {
            float xPosition = r_ColPosition * k_EnemyPixelHeight * k_EnemyBelowMarginMultiplier;
            float yPosition = r_RowPosition * k_EnemyPixelHeight * k_EnemyBelowMarginMultiplier + k_MatrixTopMargin;

            return new Vector2(xPosition, yPosition);
        }

        public override void Initialize()
        {
            base.Initialize();
            m_WidthJump = Width / 2;
        }

        public void AddOnEnemyWentBelowBorder(EventHandler i_EventHandler)
        {
            EnemyWentBelowBorder += i_EventHandler;
        }

        protected override void InitSourceRectangle()
        {
            SourceRectangle = new Rectangle(0, (int)r_EnemyType * (int)HeightBeforeScale, (int)WidthBeforeScale, (int)HeightBeforeScale);
            CenterRectangle();
        }

        protected override void InitBounds()
        {
            base.InitBounds();
            m_Position = getStartingPosition();
            WidthBeforeScale = (float)Texture.Width / k_NumberOfCells;
            HeightBeforeScale = (float)Texture.Height / k_NumberOfTypes;
            InitSourceRectangle();
        }

        public void ChangeDirection()
        {
            m_CurrentJumpTime *= k_EnemyJumpTimeMultiplier;
            m_CellAnimator.CellTime = TimeSpan.FromSeconds(m_CurrentJumpTime);
            switch (m_CurrentDirection)
            {
                case EnemyMatrix.eDirection.Right:
                    {
                        m_CurrentDirection = EnemyMatrix.eDirection.Left;
                        Position += new Vector2(-Width / 2, Height / 2);
                        break;
                    }
                case EnemyMatrix.eDirection.Left:
                    {
                        m_CurrentDirection = EnemyMatrix.eDirection.Right;
                        Position += new Vector2(Width / 2, Height / 2);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            if (!m_Destroyed && m_IsMostBottom && Position.Y > Game.GraphicsDevice.Viewport.Height - Height - k_JumpDownMargin)
            {
                EnemyWentBelowBorder?.Invoke(this, EventArgs.Empty);
            }
        }

        public void AddActionToTurnEnemies(Action i_ActionToPreform)
        {
            TurnEnemies += i_ActionToPreform;
        }

        public void AddActionToEnemyDied(Action<bool, bool, bool> i_ActionToPreform)
        {
            EnemyDied += i_ActionToPreform;
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            shootBullet();
            checkBorderCollision();
            m_NextJumpTime -= (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            if (m_NextJumpTime <= 0)
            {
                float newPositionX;

                m_NextJumpTime = m_CurrentJumpTime;
                switch (m_CurrentDirection)
                {
                    case EnemyMatrix.eDirection.Right:
                        {
                            newPositionX = Position.X + m_WidthJump;
                            break;
                        }
                    case EnemyMatrix.eDirection.Left:
                        {
                            newPositionX = Position.X - m_WidthJump;
                            break;
                        }
                    default:
                        {
                            newPositionX = Position.X;
                            break;
                        }
                }

                newPositionX = MathHelper.Clamp(newPositionX, 0, Game.GraphicsDevice.Viewport.Width - Width);
                Position = new Vector2(newPositionX, Position.Y);
            }
        }

        private void shootBullet()
        {
            if (r_Random.Next(k_RandomShootValue) == 0 && r_BulletMagazine.ShootBullet(Position))
            {
                r_SoundManager.PlaySoundEffect(MusicUtils.k_EnemyShootSound);
            }
        }

        private void checkBorderCollision()
        {
            if (m_IsMostRight && m_CurrentDirection == EnemyMatrix.eDirection.Right && 
                Position.X + m_WidthJump > Game.GraphicsDevice.Viewport.Width - Width || m_IsMostLeft
                && m_CurrentDirection == EnemyMatrix.eDirection.Left && Position.X - m_WidthJump < 0)
            {
                TurnEnemies?.Invoke();
            }
        }

        public override void Collided(ICollidable i_Collidable)
        {
            if (i_Collidable is PlayerBullet && !m_EnemyDestroyAnimator.Enabled)
            {
                r_SoundManager.PlaySoundEffect(MusicUtils.k_EnemyKillSound);
                m_EnemyDestroyAnimator.Restart();
            }
        }

        protected override void InitAnimations()
        {
            RotateAnimator rotateAnimator = new RotateAnimator(TimeSpan.FromSeconds(k_AnimationDestroyTime), k_NumberOfAnimationRoundsPerSecond);
            ShrinkAnimator shrinkAnimator = new ShrinkAnimator(TimeSpan.FromSeconds(k_AnimationDestroyTime));

            m_EnemyDestroyAnimator = new CompositeAnimator(k_AnimationDestroyName, TimeSpan.FromSeconds(k_AnimationDestroyTime),
                this, rotateAnimator, shrinkAnimator);
            m_CellAnimator = new CellAnimator(TimeSpan.FromSeconds(m_CurrentJumpTime),
                r_RowPosition % 2, k_NumberOfCells, TimeSpan.Zero);
            Animations.Add(m_CellAnimator);
            Animations.Add(m_EnemyDestroyAnimator);
            Animations.Enabled = true;
            m_EnemyDestroyAnimator.Enabled = false;
            m_EnemyDestroyAnimator.Finished += destructionAnimation_Finished;
        }

        private void destructionAnimation_Finished(object sender, EventArgs e)
        {
            Visible = false;
            Enabled = false;
        }

        public int Score
        {
            get
            {
                return r_EnemyPoints;
            }
        }

        public bool Destroyed
        {
            get
            {
                return m_Destroyed;
            }
            set
            {
                m_Destroyed = value;
                if (m_Destroyed)
                {
                    EnemyDied?.Invoke(m_IsMostLeft, m_IsMostRight, m_IsMostBottom);
                }
            }
        }

        public bool IsMostRight
        {
            set
            {
                m_IsMostRight = value;
            }
        }

        public bool IsMostLeft
        {
            set
            {
                m_IsMostLeft = value;
            }
        }

        public bool IsMostBottom
        {
            set
            {
                m_IsMostBottom = value;
            }
        }
    }
}