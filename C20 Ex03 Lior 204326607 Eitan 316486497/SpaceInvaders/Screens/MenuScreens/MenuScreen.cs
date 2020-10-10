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
        private const int k_ItemsOffsetY = -130;
        private const string k_ToggleOnMessage = "On";
        private const string k_ToggleOffMessage = "Off";
        private readonly Background r_Background;
        private ScreenHeader m_ScreenHeader;

        public MenuScreen(Game i_Game) : base(i_Game)
        {
            r_Background = new Background(this);
            m_OffsetY = k_ItemsOffsetY;
        }

        protected void SetScreenHeader(string i_HeaderAsset,  float i_Scale)
        {
            m_ScreenHeader = new ScreenHeader(this, i_HeaderAsset, i_Scale, GraphicsDevice.Viewport.Width / 2f, k_HeaderOffsetY, true);
        }

        protected string GetDefaultToggleMessage(bool i_ToggleValue)
        {
            return i_ToggleValue ? k_ToggleOnMessage : k_ToggleOffMessage;
        }

        protected TextItem AddOptionItem(string i_PrefixMessage, bool i_StartingValue, Color i_ActiveColor, EventHandler i_EventHandler)
        {
            return AddOptionItem(GetDefaultToggleMessage(i_StartingValue), i_ActiveColor, i_EventHandler, true, i_PrefixMessage);
        }

        protected TextItem AddOptionItem(string i_Message, Color i_ActiveColor, EventHandler i_EventHandler, bool i_IsToggleItem = false, string i_PrefixMessage = "")
        {
            TextItem textItem = new TextItem(this, i_Message, NumberOfItemsOnScreen(), i_ActiveColor);
            textItem.AddToOnClick(i_EventHandler);
            textItem.IsToggleItem = i_IsToggleItem;
            textItem.PrefixMessage = i_PrefixMessage;
            AddGameItem(textItem);
            return textItem;
        }
    }
}
