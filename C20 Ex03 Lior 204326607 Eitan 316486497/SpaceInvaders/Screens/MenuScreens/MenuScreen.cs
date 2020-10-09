using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities;
using C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure.ObjectModel.Screens.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens.MenuScreens
{
    class MenuScreen : NavigableScreen
    {
        private const int k_HeaderOffsetY = 50;
        private readonly Background r_Background;
        private ScreenHeader m_ScreenHeader;

        public MenuScreen(Game i_Game) : base(i_Game)
        {
            r_Background = new Background(this);
            m_OffsetY = - 130;
        }

        protected void SetScreenHeader(string i_HeaderAsset,  float i_Scale)
        {
            m_ScreenHeader = new ScreenHeader(this, i_HeaderAsset, i_Scale, GraphicsDevice.Viewport.Width / 2f, k_HeaderOffsetY, true);
        }

        protected TextItem AddOptionItem(string i_Message, Color i_ActiveColor, EventHandler i_EventHandler, bool i_IsToggleItem = false)
        {
            TextItem textItem = new TextItem(this, i_Message, NumberOfItemsOnScreen(), i_ActiveColor);
            textItem.AddToOnClick(i_EventHandler);
            textItem.IsToggleItem = i_IsToggleItem;
            textItem.RedirectsToScreen = !i_IsToggleItem;
            AddGameItem(textItem);
            return textItem;
        }
    }
}
