using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure
{
    public class BaseGame : Game
    {
        private readonly GraphicsDeviceManager r_Graphics;
        protected InputManager m_InputManager;
        protected CollisionsManager m_CollisionManager;

        public BaseGame()
            : base()
        {
            r_Graphics = new GraphicsDeviceManager(this);
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
        public InputManager InputManager
        {
            get
            {
                return m_InputManager;
            }
        }
    }
}
