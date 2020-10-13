using System;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure.ObjectModel.Screens.Items
{
    public enum eValueChange
    {
        Increase,
        Decrease,
        Unchanged
    }

    public class ItemValueChangeEventArgs : EventArgs
    {
        protected eValueChange r_ValueChange;

        public ItemValueChangeEventArgs(eValueChange i_ValueChange)
        {
            r_ValueChange = i_ValueChange;
        }

        public eValueChange ValueChange
        {
            get { return r_ValueChange; }
        }
    }

    public class GameItem : Sprite
    {
        private event EventHandler<EventArgs> ActiveStateChanged;

        private event EventHandler OnClicked;

        private const int k_PulsePerSecond = 1;
        private const float k_PulseTargetScale = 0.5f;
        private const int k_ScrollWheelDeltaThreshold = 120;
        private const string k_PulseAnimationName = "PulseAnimation";
        protected readonly ISoundManager r_SoundManager;
        private readonly int r_NumberInScreen;
        protected Color m_ActiveColor;
        protected bool m_TouchLocked;
        private bool m_ItemActive;
        private PulseAnimator m_PulseAnimator;
        private bool m_ActivatedByMouse;
        private bool m_IsToggleItem;
        private bool m_RedirectsToScreen;
        public bool m_CannotBeSelectedByMouse;
        private bool m_IsItemPressed;
        private Keys m_KeyRedirection;

        public GameItem(string i_AssetName, GameScreen i_GameScreen, int i_ItemNumber)
            : base(i_AssetName, i_GameScreen)
        {
            r_SoundManager = m_GameScreen.Game.Services.GetService(typeof(ISoundManager)) as ISoundManager;
            r_NumberInScreen = i_ItemNumber;
            m_ItemActive = false;
            m_TouchLocked = false;
            m_IsToggleItem = false;
            m_ActivatedByMouse = false;
            m_IsItemPressed = false;
            m_IsItemPressed = false;
            m_CannotBeSelectedByMouse = false;
        }

        public override void Initialize()
        {
            base.Initialize();

            ActiveStateChanged += stateChanged;


            m_PulseAnimator = new PulseAnimator(k_PulseAnimationName, TimeSpan.Zero, k_PulseTargetScale, k_PulsePerSecond);
            Animations.Add(m_PulseAnimator);
            Animations.Enabled = true;
            m_PulseAnimator.Enabled = false;
            PositionOrigin = SourceRectangleCenter - new Vector2(Width/2, Height/2);
            RotationOrigin = SourceRectangleCenter;
        }

        public override void Update(GameTime i_GameTime)
        {
            if (ItemActive && m_IsItemPressed && GameScreen.InputManager.ButtonIsUp(eInputButtons.Left) && MouseHovering())
            {
                onItemClicked();
            }
            else if(GameScreen.InputManager.KeyReleased(m_KeyRedirection))
            {
                onItemClicked();
            }

            m_IsItemPressed = GameScreen.InputManager.ButtonIsDown(eInputButtons.Left);

            if (m_ItemActive)
            {
                if(m_IsToggleItem)
                {
                    int scrollWheelItemDelta = GameScreen.InputManager.ScrollWheelDelta / k_ScrollWheelDeltaThreshold; 

                    if(GameScreen.InputManager.KeyPressed(Keys.PageUp) || scrollWheelItemDelta == 1)
                    {
                        onItemClicked(new ItemValueChangeEventArgs(eValueChange.Increase));
                    }
                    else if(GameScreen.InputManager.KeyPressed(Keys.PageDown) || scrollWheelItemDelta == -1)
                    {
                        onItemClicked(new ItemValueChangeEventArgs(eValueChange.Decrease));
                    }
                }
                else if(GameScreen.InputManager.KeyPressed(Keys.Enter) && m_RedirectsToScreen)
                {
                    onItemClicked();
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
                r_SoundManager.PlayHoverSound();
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

        private void onItemClicked()
        {
            OnClicked?.Invoke(this, EventArgs.Empty);
        }

        private void onItemClicked(EventArgs e)
        {
            OnClicked?.Invoke(this, e);
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
