using C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens;
using Microsoft.Xna.Framework.Graphics;

namespace C20_Ex03_Lior_204326607_Eitan_316486497
{
    public class Invaders : BaseGame
    {
        private const int k_WindowSizeX = 800;
        private const int k_WindowSizeY = 600;
        private const string k_WindowTitle = "Invaders 👽";
        private readonly ScoreManager r_ScoreManager;
        private readonly PlayManager r_PlayManager;
        private SpriteBatch m_SpriteBatch;

        public Invaders() : base()
        {
            r_ScoreManager = new ScoreManager(this);
            r_PlayManager = new PlayManager(this);
            r_Graphics.PreferredBackBufferWidth = k_WindowSizeX;
            r_Graphics.PreferredBackBufferHeight = k_WindowSizeY;
            r_Graphics.ApplyChanges();

            /*DEBUG*/
            Window.AllowUserResizing = true;
            /*END OF DEBUG*/

            Window.Title = k_WindowTitle;
            r_Screens.SetCurrentScreen(new WelcomeScreen(this));
        }

        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            MusicUtils.LoadSoundEffects(r_SoundManager, Content);
            /*DEBUG*/
            r_SoundManager.ChangeBackgroundMusicVolumeLevel(-99);
            r_SoundManager.ChangeSoundEffectsVolumeLevel(-99);
            r_SoundManager.SoundToggle = true;
            /*END OF DEBUG*/
            base.LoadContent();
        }
    }
}
