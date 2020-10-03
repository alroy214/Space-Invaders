using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure.ObjectModel.Screens.Items
{
    public class ClickableItem : GameItem
    {
        private event EventHandler OnClicked;
        private Keys m_KeyRedirection;

        public ClickableItem(string i_AssetName, GameScreen i_GameScreen, int i_ItemNumber) : base(i_AssetName, i_GameScreen, i_ItemNumber)
        {
        }

        public void AddToOnClick(EventHandler i_Handler)
        {
            OnClicked += i_Handler;
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

        protected virtual void OnItemClicked(object sender, EventArgs args)
        {
            OnClicked?.Invoke(sender, args);
        }

        public override void Update(GameTime i_GameTime)
        {
            if (Enabled)
            {
                if (GameScreen.InputManager.ButtonReleased(eInputButtons.Left) && IsItemActive
                    || GameScreen.InputManager.KeyReleased(m_KeyRedirection))
                {
                    OnItemClicked(this, EventArgs.Empty);
                }
            }

            base.Update(i_GameTime);
        }
    }
}
