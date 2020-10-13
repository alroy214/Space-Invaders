using System.Drawing;
using System.Windows.Forms;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens;
using Infrastructure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace C20_Ex03_Lior_204326607_Eitan_316486497
{
    public class Invaders : BaseGame
    {
        private const int k_WindowSizeX = 800;
        private const int k_WindowSizeY = 600;
        private const int k_MinimumSizeX = 600;
        private const int k_MinimumSizeY = 600;
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
            setMinimumBackground();
            r_Graphics.ApplyChanges();
            Window.Title = k_WindowTitle;
            r_Screens.SetCurrentScreen(new WelcomeScreen(this));
        }

        protected override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            if (m_InputManager.KeyPressed(MusicUtils.k_MusicMuteKey) && r_ScoreManager != null)
            {
                r_SoundManager.SoundToggle = !r_SoundManager.SoundToggle;
            }
        }

        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            MusicUtils.LoadSoundEffects(r_SoundManager, Content);
            /*DEBUG*/
            r_SoundManager.ChangeBackgroundMusicVolumeLevel(-90);
            r_SoundManager.ChangeSoundEffectsVolumeLevel(-90);
            /*END OF DEBUG*/
            base.LoadContent();
        }

        private void setMinimumBackground()
        {
            if (Control.FromHandle(Window.Handle) is Form form)
            {
                form.MinimumSize = new Size(k_MinimumSizeX, k_MinimumSizeY);
            }
        }
    }
}