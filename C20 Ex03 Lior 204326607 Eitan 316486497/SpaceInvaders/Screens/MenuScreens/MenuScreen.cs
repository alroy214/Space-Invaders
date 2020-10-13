using System;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities;
using C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure.ObjectModel.Screens.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens.MenuScreens
{
    public class MenuScreen : NavigableScreen
    {
        private const int k_ItemsOffsetY = -130;
        private const string k_ToggleOnMessage = "On";
        private const string k_ToggleOffMessage = "Off";
        private readonly Background r_Background;

        public MenuScreen(Game i_Game) : base(i_Game)
        {
            r_Background = new Background(this);
            m_OffsetY = k_ItemsOffsetY;
            ActivationLength = TimeSpan.FromMilliseconds(300);
            DeactivationLength = TimeSpan.FromMilliseconds(200);
            BlendState = BlendState.NonPremultiplied;
            UseFadeTransition = true;
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);

            if (Math.Abs(TransitionPosition - 1) > 0)
            {
                r_Background.Opacity = TransitionPosition;
                if(m_ScreenHeader != null)
                {
                    m_ScreenHeader.Opacity = TransitionPosition;
                }
            }
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
            textItem.RedirectsToScreen = !i_IsToggleItem;
            textItem.PrefixMessage = i_PrefixMessage;
            AddGameItem(textItem);
            return textItem;
        }
    }
}
