//*** Guy Ronen © 2008-2011 ***//
using Infrastructure.ObjectModel.Screens;

namespace Infrastructure.ServiceInterfaces
{
    public interface IScreensManager
    {
        GameScreen ActiveScreen { get; }

        bool AllowWindowResizing { get; set; }

        bool FullScreenMode { get; set; }

        bool MouseVisibility { get; set; }

        void SetCurrentScreen(GameScreen i_NewScreen);
        
        bool Remove(GameScreen i_Screen);
        
        void Add(GameScreen i_Screen);
    }
}
