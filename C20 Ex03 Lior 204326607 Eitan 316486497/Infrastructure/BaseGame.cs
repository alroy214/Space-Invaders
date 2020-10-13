using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Managers;
using Infrastructure.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure
{
    public class BaseGame : Game
    {
        protected readonly ScreensManager r_Screens;
        protected readonly GraphicsDeviceManager r_Graphics;
        protected readonly SoundManager r_SoundManager;
        protected InputManager m_InputManager;
        protected CollisionsManager m_CollisionManager;

        public BaseGame() : base()
        {
            r_Graphics = new GraphicsDeviceManager(this);
            r_Screens = new ScreensManager(this);
            r_SoundManager = new SoundManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            m_InputManager = new InputManager(this);
            m_CollisionManager = new CollisionsManager(this);
            base.Initialize();
        }

        protected override void Update(GameTime i_GameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            r_Graphics.GraphicsDevice.Clear(Color.Black);
            base.Update(i_GameTime);
        }

        public GraphicsDeviceManager GraphicsDeviceManager
        {
            get
            {
                return r_Graphics;
            }
        }
    }
}
