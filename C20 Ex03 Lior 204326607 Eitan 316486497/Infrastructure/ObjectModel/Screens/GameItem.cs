using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Managers;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure.ObjectModel.Screens.Items
{
    public class GameItem : Sprite
    {
        private event EventHandler<EventArgs> ActiveStateChanged;

        private event EventHandler OnClicked;

        private const int k_PulsePerSecond = 1;
        private const float k_PulseTargetScale = 0.5f;
        private const float k_OriginOffsetX = 3.5f;
        private const float k_OriginOffsetY = 3.5f;
        private const string k_PulseAnimationName = "PulseAnimation";
        private readonly int r_NumberInScreen;
        private bool m_ItemActive;
        private PulseAnimator m_PulseAnimator;
        protected Color m_ActiveColor;
        protected bool m_TouchLocked;
        protected ISoundManager m_SoundManager;
        private bool m_ActivatedByMouse;
        private bool m_IsToggleItem;
        private bool m_RedirectsToScreen;
        public bool m_CannotBeSelectedByMouse;
        private bool m_IsItemPressed;
        private Keys m_KeyRedirection;

        public GameItem(string i_AssetName, GameScreen i_GameScreen, int i_ItemNumber)
            : base(i_AssetName, i_GameScreen)
        {
            r_NumberInScreen = i_ItemNumber;
            m_ItemActive = false;
            m_TouchLocked = false;
            m_IsToggleItem = false;
            m_RedirectsToScreen = false;
            m_ActivatedByMouse = false;
            m_IsItemPressed = false;   
        m_CannotBeSelectedByMouse = false;
        }

        public override void Initialize()
        {
            base.Initialize();

            ActiveStateChanged += stateChanged;

            m_SoundManager = m_GameScreen.Game.Services.GetService(typeof(ISoundManager)) as ISoundManager;
        }

        protected override void InitAnimations()
        {
            Animations.Enabled = true;
            m_PulseAnimator = new PulseAnimator(k_PulseAnimationName, TimeSpan.Zero, k_PulseTargetScale, k_PulsePerSecond);
            Animations.Add(m_PulseAnimator);
            m_PulseAnimator.Enabled = false;
            m_PositionOrigin = new Vector2(m_PositionOrigin.X + Width / k_OriginOffsetX, m_PositionOrigin.Y + Height / k_OriginOffsetY);
            CenterRectangle();
        }

        public override void Update(GameTime i_GameTime)
        {
            if (ItemActive && m_IsItemPressed && GameScreen.InputManager.ButtonIsUp(eInputButtons.Left) && MouseHovering())
            {
                onItemClicked(true);
            }
            else if(GameScreen.InputManager.KeyReleased(m_KeyRedirection))
            {
                onItemClicked(false);
            }

            m_IsItemPressed = GameScreen.InputManager.ButtonIsDown(eInputButtons.Left);

            if (m_ItemActive)
            {
                if(m_IsToggleItem)
                {
                    int scrollWheelItemDelta = GameScreen.InputManager.ScrollWheelDelta / 120; //Make const

                    if(GameScreen.InputManager.KeyPressed(Keys.PageUp)
                       || GameScreen.InputManager.KeyPressed(Keys.PageDown) //TODO Change to include sliders
                       || scrollWheelItemDelta == 1 || scrollWheelItemDelta == -1)
                    {
                        onItemClicked(false);
                    }
                }
                else if(m_RedirectsToScreen && GameScreen.InputManager.KeyPressed(Keys.Enter))
                {
                    onItemClicked(false);
                }
            }

            base.Update(i_GameTime);
        }

        public bool CheckMouseActivation()
        {
            bool itemChangedToActive = false;
            if(!m_TouchLocked)
            {
                if(MouseHovering())
                {
                    m_ActivatedByMouse = true;
                    if(!m_ItemActive && !m_CannotBeSelectedByMouse)
                    {
                        ItemActive = true;
                        itemChangedToActive = true;
                    }
                }
                else
                {
                    if (m_ItemActive && m_ActivatedByMouse)
                    {
                        ItemActive = false;
                        m_ActivatedByMouse = false;
                    }

                    m_CannotBeSelectedByMouse = false;
                }
            }

            return itemChangedToActive;
        }

        public void SilentlyActivateItem()
        {
            m_ItemActive = true;
            activateItem();
        }

        private void activateItem()
        {
            if (m_PulseAnimator != null && !m_PulseAnimator.Enabled)
            {
                m_PulseAnimator.Restart();
            }

            TintColor = m_ActiveColor;
        }

        private void stateChanged(object sender, EventArgs e)
        {
            if(m_ItemActive)
            {
                activateItem();
                m_SoundManager.PlayHoverSound();
            }
            else
            {
                if(m_PulseAnimator.Enabled)
                {
                    m_PulseAnimator.Pause();
                    m_PulseAnimator.Reset();
                }

                TintColor = Color.White;
                m_ActivatedByMouse = false;
            }
        }

        public bool MouseHovering()
        {
            return PositionBounds.Contains(new Vector2(m_GameScreen.InputManager.MouseState.X, GameScreen.InputManager.MouseState.Y));
        }

        public void AddToOnClick(EventHandler i_Handler)
        {
            OnClicked += i_Handler;
        }

        private void onItemClicked(bool i_InvokedByMouse)
        {
            OnClicked?.Invoke(this, EventArgs.Empty);
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

        public bool ItemActive
        {
            get
            {
                return m_ItemActive;
            }
            set
            {
                m_ItemActive = value;
                ActiveStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool IsToggleItem
        {
            get
            {
                return m_IsToggleItem;
            }
            set
            {
                m_IsToggleItem = value;
            }
        }

        public bool RedirectsToScreen
        {
            get
            {
                return m_RedirectsToScreen;
            }
            set
            {
                m_RedirectsToScreen = value;
            }
        }

        public Keys KeyRedirection
        {
            get
            {
                return m_KeyRedirection;
            }
            set
            {
                m_KeyRedirection = value;
            }
        }

        public int NumberInScreen
        {
            get
            {
                return r_NumberInScreen;
            }
        }
    }
}
