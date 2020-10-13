using System;
using C20_Ex03_Lior_204326607_Eitan_316486497.GameViews;
using Microsoft.Xna.Framework;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens.MenuScreens
{
    public class ScreenSettings : MenuScreen
    {
        private const string k_HeaderAssetName = @"Headers\ScreenSettings";
        private const float k_HeaderScale = 0.25f;
        private const string k_WindowResizingMessage = "Allow Window Resizing: ";
        private const string k_FullScreenMessage = "Full Screen Mode: ";
        private const string k_MouseVisibilityMessage = "Mouse Visibility: ";
        private const string k_DoneMessage = "Done";
        private const string k_MouseVisibleMessage = "Visible";
        private const string k_MouseInvisibleMessage = "Inisible";
        private readonly GraphicsDeviceManager r_Graphics;

        public ScreenSettings(Game i_Game) : base(i_Game)
        {
            SetScreenHeader(k_HeaderAssetName, k_HeaderScale);
            r_Graphics = (GraphicsDeviceManager)i_Game.Services.GetService(typeof(IGraphicsDeviceManager));
            AddOptionItem(k_WindowResizingMessage, Game.Window.AllowUserResizing, Color.MediumSeaGreen, windowResizing_OnClicked);
            AddOptionItem(k_FullScreenMessage, r_Graphics.IsFullScreen, Color.CornflowerBlue, fullScreen_OnClicked);
            AddOptionItem(mouseVisibilityMessage(), Color.Bisque, mouseVisibility_OnClicked, true, k_MouseVisibilityMessage);
            AddOptionItem(k_DoneMessage, Color.PaleVioletRed, done_OnClicked);
        }

        private string mouseVisibilityMessage()
        {
            return Game.IsMouseVisible ? k_MouseVisibleMessage : k_MouseInvisibleMessage;
        }

        private void windowResizing_OnClicked(object sender, EventArgs e)
        {
            bool allowResizing = !Game.Window.AllowUserResizing;

            Game.Window.AllowUserResizing = allowResizing;
            ((TextItem)sender).TextMessage = GetDefaultToggleMessage(allowResizing);
        }

        private void fullScreen_OnClicked(object sender, EventArgs e)
        {
            r_Graphics.ToggleFullScreen();
            ((TextItem)sender).TextMessage = GetDefaultToggleMessage(r_Graphics.IsFullScreen);
        }

        private void mouseVisibility_OnClicked(object sender, EventArgs e)
        {
            Game.IsMouseVisible = !Game.IsMouseVisible;
            ((TextItem)sender).TextMessage = mouseVisibilityMessage();
        }

        private void done_OnClicked(object sender, EventArgs e)
        {
            ExitScreen();
        }
    }
}
