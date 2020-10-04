using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities;
using C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure.ObjectModel.Screens.Items;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens
{
    class NavigableScreen : GameScreen
    {
        private readonly Background r_Background;
        private readonly List<GameItem> r_GameItems;
        private Game m_Game;
        private Vector2 m_firstPosition;
        private float m_OffsetX;
        private float m_OffsetY;
        private float m_MarginFromScreenMembers;
        private int m_CurrentNumberOnScreen;
        private int m_PreviousNumberOnScreen;
        private bool m_MouseEnabled;
        private bool m_KeyBoardEnabled;

        public NavigableScreen(Game i_Game) : base(i_Game)
        {
            m_Game = i_Game;
            r_GameItems = new List<GameItem>();
            r_Background = new Background(this);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();
            initializeFirstPosition();
        }

        protected int CurrentNumberOfItemsOnScreen()
        {
            return r_GameItems.Count;
        }

        private void initializeFirstPosition()
        {
            m_firstPosition = new Vector2((m_Game.Window.ClientBounds.Width / 3f) - m_OffsetX, (m_Game.Window.ClientBounds.Height / 2.5f) + m_OffsetY);
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
