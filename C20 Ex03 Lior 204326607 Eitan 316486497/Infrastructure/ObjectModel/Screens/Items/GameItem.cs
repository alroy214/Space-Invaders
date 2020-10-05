using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using SharpDX.Direct2D1.Effects;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure.ObjectModel.Screens.Items
{
    public class GameItem : Sprite
    {
        private event EventHandler<EventArgs> ActiveStateChanged;
        private readonly int r_NumberInScreen;
        private const int k_PulsePerSecond = 1;
        private const float k_PulseTargetScale = 0.5f;
        private const string k_PulseAnimationName = "PulseAnimation";
        private bool m_IsItemActive;
        private bool m_IsSoundEnabled; //TODO
        private PulseAnimator m_PulseAnimator;
        protected Color m_ActiveColor;
        public bool m_IsTouchUnlocked;

        public GameItem(string i_AssetName, GameScreen i_GameScreen, int i_ItemNumber) : base(i_AssetName, i_GameScreen)
        {
            r_NumberInScreen = i_ItemNumber;
            m_IsSoundEnabled = true;
            m_IsItemActive = false;
            m_IsTouchUnlocked = true;
        }

        public override void Initialize()
        {
            base.Initialize();

            ActiveStateChanged += StateChanged;

            if (m_IsSoundEnabled)
            {
                //m_SoundManager = m_GameScreen.Game.Services.GetService(typeof(ISoundManager)) as ISoundMananger;
            }
        }

        protected override void InitAnimations()
        {
            Animations.Enabled = true;
            m_PulseAnimator = new PulseAnimator(k_PulseAnimationName, TimeSpan.Zero, k_PulseTargetScale, k_PulsePerSecond);
            Animations.Add(m_PulseAnimator);
            m_PulseAnimator.Enabled = false;
            CenterRectangle();
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            bool isMouseHovering = IsMouseHoveringOverItem();
            if (isMouseHovering != IsItemActive && m_IsTouchUnlocked)
            {
                m_IsItemActive = isMouseHovering;
                ActiveStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public Color ActiveColor
        {
            get
            {
                return m_ActiveColor;
            }
            set
            {
                m_ActiveColor = value;
            }
        }

        public bool IsItemActive
        {
            get
            {
                return m_IsItemActive;
            }
            set
            {
                m_IsItemActive = value;
                ActiveStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void StateChanged(object sender, EventArgs e)
        {
            if(m_IsItemActive)
            {
                if(!m_PulseAnimator.Enabled)
                {
                    m_PulseAnimator.Restart();
                }

                TintColor = m_ActiveColor;

                if(m_IsItemActive)
                {
                 //   m_SoundManager.PlaySoundEffect(m_SoundOnHover);
                }
            }
            else
            {
                if(m_PulseAnimator.Enabled)
                {
                    m_PulseAnimator.Pause();
                    m_PulseAnimator.Reset();
                }

                TintColor = Color.White;
            }
        }

        public bool IsMouseHoveringOverItem()
        {
            return Bounds.Contains(new Vector2(m_GameScreen.InputManager.MouseState.X, GameScreen.InputManager.MouseState.Y));
        }

    }
}
