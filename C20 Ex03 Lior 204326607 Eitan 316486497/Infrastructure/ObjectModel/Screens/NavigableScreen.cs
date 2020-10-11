using System;
using System.Collections.Generic;
using C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure.ObjectModel.Screens.Items;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens
{
    public class NavigableScreen : GameScreen
    {
        private const int k_DefaultItemHeightMargin = 25;
        private const Keys k_DefaultNavigateUpKey = Keys.Up;
        private const Keys k_DefaultNavigateDownKey = Keys.Down;
        private const bool k_MouseEnabledByDefault = true;
        private const bool k_KeyboardEnabledByDefault = true;
        private const int k_NoItemSelectedIndex = -1;
        protected readonly List<GameItem> r_GameItems;
        protected float m_OffsetX;
        protected float m_OffsetY;
        private Vector2 m_FirstPosition;
        private float m_ItemHeightMargin;
        public int m_CurrentActiveItem;
        private int m_PreviousActiveItem;
        private bool m_LastInputWasByAKeyboard;
        private Keys m_NavigateUpKey;
        private Keys m_NavigateDownKey;
        private bool m_MouseEnabled;
        private bool m_KeyBoardEnabled;

        public NavigableScreen(Game i_Game) : base(i_Game)
        {
            r_GameItems = new List<GameItem>();
            m_CurrentActiveItem = 0;
            m_PreviousActiveItem = 0;
            m_LastInputWasByAKeyboard = false;
            m_NavigateUpKey = k_DefaultNavigateUpKey;
            m_NavigateDownKey = k_DefaultNavigateDownKey;
            m_ItemHeightMargin = k_DefaultItemHeightMargin; 
            m_MouseEnabled = k_MouseEnabledByDefault; 
            m_KeyBoardEnabled = k_KeyboardEnabledByDefault;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            activateFirstItem();
        }

        public override void Initialize()
        {
            base.Initialize();
            initializeFirstPosition();
            initializeAllPositions();
        }
        
        private void initializeFirstPosition()
        {
           m_FirstPosition = CenterOfViewPort + new Vector2(-m_OffsetX, m_OffsetY);
        }

        private void initializeAllPositions()
        {
            if (r_GameItems != null)
            {
                foreach(GameItem item in r_GameItems)
                {
                    item.Position = m_FirstPosition + new Vector2(-item.Width / 2f, item.NumberInScreen * (m_ItemHeightMargin + item.Texture.Height / 2f));
                }
            }
        }

        private void activateFirstItem()
        {
            if (r_GameItems.Count != 0 && m_KeyBoardEnabled)
            {
                r_GameItems[m_CurrentActiveItem].SilentlyActivateItem();
                m_LastInputWasByAKeyboard = true;
            }
        }

        public override void Update(GameTime i_GameTime)
        {
            updateItemNavigation();
            checkIfActiveItemChanged();
            base.Update(i_GameTime);
        }

        protected override void UpdateTransition(GameTime i_GameTime)
        {
            base.UpdateTransition(i_GameTime);
            foreach(GameItem item in r_GameItems)
            {
                item.Opacity = TransitionPosition;
            }
        }

        private void updateItemNavigation()
        {
            if (MouseEnabled)
            {
                updateMouseNavigation();
            }

            if (KeyBoardEnabled)
            {
                updateKeyboardNavigation();
            }
        }

        private void updateMouseNavigation()
        {
            bool foundAnActiveItem = false;

            foreach (GameItem item in r_GameItems)
            {
                if (item.CheckMouseActivation())
                {
                    m_LastInputWasByAKeyboard = false;
                    m_CurrentActiveItem = item.NumberInScreen;
                }

                if(item.ItemActive)
                {
                    foundAnActiveItem = true;
                }
            }

            if (!foundAnActiveItem && !m_LastInputWasByAKeyboard)
            {
                m_CurrentActiveItem = k_NoItemSelectedIndex;
            }
        }

        private void checkKeyPressedWithNoItemsSelected()
        {
            m_LastInputWasByAKeyboard = true;
            if (m_CurrentActiveItem == k_NoItemSelectedIndex)
            {
                m_CurrentActiveItem = m_PreviousActiveItem;
            }
            else
            {
                r_GameItems[m_CurrentActiveItem].m_CannotBeSelectedByMouse = true;
            }
        }

        private void updateKeyboardNavigation()
        {
            if (InputManager.KeyPressed(m_NavigateDownKey))
            {
                checkKeyPressedWithNoItemsSelected();

                m_CurrentActiveItem = (m_CurrentActiveItem + 1) % r_GameItems.Count;
            }
            else if(InputManager.KeyPressed(m_NavigateUpKey))
            {
                checkKeyPressedWithNoItemsSelected();

                if(m_CurrentActiveItem == 0)
                {
                    m_CurrentActiveItem = r_GameItems.Count - 1;
                }
                else
                {
                    m_CurrentActiveItem = (m_CurrentActiveItem - 1) % r_GameItems.Count;
                }
            }
        }

        private void checkIfActiveItemChanged()
        {
            if (r_GameItems.Count != 0 && m_CurrentActiveItem != m_PreviousActiveItem)
            {
                if (m_CurrentActiveItem != k_NoItemSelectedIndex)
                {
                    r_GameItems[m_CurrentActiveItem].ItemActive = true;
                }

                if (m_PreviousActiveItem != k_NoItemSelectedIndex)
                {
                    r_GameItems[m_PreviousActiveItem].ItemActive = false;
                }
            }

            if(m_CurrentActiveItem != k_NoItemSelectedIndex)
            {
                m_PreviousActiveItem = m_CurrentActiveItem;
            }
        }

        protected int NumberOfItemsOnScreen()
        {
            return r_GameItems.Count;
        }

        public bool KeyBoardEnabled
        {
            get
            {
                return m_KeyBoardEnabled;
            }
            set
            {
                m_KeyBoardEnabled = value;
            }
        }

        public bool MouseEnabled
        {
            get
            {
                return m_MouseEnabled;
            }
            set
            {
                m_MouseEnabled = value;
            }
        }

        public Keys NavigateUpKey
        {
            get
            {
                return m_NavigateUpKey;
            }
            set
            {
                m_NavigateUpKey = value;
            }
        }

        public Keys NavigateDownKey
        {
            get
            {
                return m_NavigateDownKey;
            }
            set
            {
                m_NavigateDownKey = value;
            }
        }

        public float ItemHeightMargin
        {
            get
            {
                return m_ItemHeightMargin;
            }
            set
            {
                m_ItemHeightMargin = value;
            }
        }

        protected override void Dispose(bool i_Disposing)
        {
            if(i_Disposing)
            {
                DisposeItems();
            }

            base.Dispose(i_Disposing);
        }

        protected void DisposeItems()
        {
            foreach(GameItem item in r_GameItems)
            {
                item.Dispose();
            }
        }

        public void AddGameItem(GameItem i_Item)
        {
            r_GameItems.Add(i_Item);
        }

    }
}
