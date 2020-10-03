using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens
{
    class ScreenHeader : Sprite
    {
        private float m_OffsetX;
        private float m_OffsetY;

        public ScreenHeader(GameScreen i_GameScreen, string i_AssetName, float i_Scale, float i_OffsetX, float i_OffsetY) : base(i_AssetName, i_GameScreen)
        {
            m_Scales = new Vector2(i_Scale);
            m_OffsetX = i_OffsetX;
            m_OffsetY = i_OffsetY;
        }

        public ScreenHeader(GameScreen i_GameScreen, string i_AssetName, float i_Scale) : base(i_AssetName, i_GameScreen)
        {
            m_Scales = new Vector2(i_Scale);
        }

        public int SourceRecWidth
        {
            set
            {
                m_SourceRectangle.Width = value;
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            InitOrigins();
            m_GameScreen.Game.Window.ClientSizeChanged += this.WindowClientSize_Changed;
        }

        protected override void InitBounds()
        {
            base.InitBounds();
            updatePosition();
        }

        private void updatePosition()
        {
            Position = new Vector2((m_GameScreen.Game.Window.ClientBounds.Width / 2f) + m_OffsetX, m_OffsetY);
        }

        private void WindowClientSize_Changed(object sender, EventArgs e)
        { 
            updatePosition();
        }


        /*

        protected override void InitOrigins()
        {
            PositionOrigin = new Vector2(this.Texture.Width / 2f, 0);
            base.InitOrigins();
        }*/


        public float OffsetX
        {
            set
            {
                m_OffsetX = value;
                updatePosition();
            }
        }

        public float OffsetY
        {
            set
            { 
                m_OffsetY = value;
                updatePosition();
            }
        }
    }
}
