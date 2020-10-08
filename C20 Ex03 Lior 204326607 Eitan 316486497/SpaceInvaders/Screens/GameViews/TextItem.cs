using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure.ObjectModel.Screens.Items
{
    public class TextItem : GameItem
    {
        private const string k_TextAsset = @"Screens/bubble3";
        private const float k_ScaleX = 0.45f;
        private const float k_ScaleY = 0.45f;
        private readonly Color r_FontColor;
        private SpriteFont m_Font;
        private Vector2 m_TextPosition;
        private string m_TextMessage;

        public TextItem(GameScreen i_GameScreen, string i_TextMessage, int i_ItemNumber, Color i_ActiveColor, Keys i_KeyRedirection = Keys.None)
            : this(i_GameScreen, i_TextMessage, i_ItemNumber, i_ActiveColor, Color.Black, i_KeyRedirection)
        {
        }

        public TextItem(GameScreen i_GameScreen, string i_TextMessage, Color i_ActiveColor, Color i_FontColor, bool i_TouchLock = false)
            : this(i_GameScreen, i_TextMessage, 0, i_ActiveColor, i_FontColor, Keys.None, i_TouchLock)
        {
        }

        public TextItem(GameScreen i_GameScreen, string i_TextMessage, int i_ItemNumber, Color i_ActiveColor, Color i_FontColor, bool i_TouchLock = false)
            : this(i_GameScreen, i_TextMessage, i_ItemNumber, i_ActiveColor, i_FontColor, Keys.None, i_TouchLock)
        {
        }

        public TextItem(GameScreen i_GameScreen, string i_TextMessage, int i_ItemNumber, Color i_ActiveColor, Color i_FontColor, Keys i_KeyRedirection = Keys.None, bool i_TouchLock = false)
            : base(k_TextAsset, i_GameScreen, i_ItemNumber)
        {
            m_TextMessage = i_TextMessage;
            m_ActiveColor = i_ActiveColor;
            r_FontColor = i_FontColor;
            m_Scales = new Vector2(k_ScaleX, k_ScaleY);
            m_TouchLocked = i_TouchLock;
            m_TextPosition = Vector2.Zero;
            if(i_KeyRedirection != Keys.None)
            {
                m_KeyRedirection = i_KeyRedirection;
            }
            if (m_TouchLocked)
            {
                TintColor = i_ActiveColor;
            }
        }

        public string TextMessage
        {
            get
            {
                return m_TextMessage;
            }
            set
            {
                m_TextMessage = value;
            }
        }

        protected override void InitBounds()
        {
            base.InitBounds();
            m_Position = new Vector2( - Width / 2,  - Height / 2);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            m_Font = Game.Content.Load<SpriteFont>(@"Fonts\Consolas");
        }

        public override void Draw(GameTime i_GameTime)
        {
            if(m_TextPosition == Vector2.Zero)
            {
                Vector2 measuredText = m_Font.MeasureString(m_TextMessage);
                bool isMultiLine = m_TextMessage.Split(Environment.NewLine).Length != 1;
                m_TextPosition = new Vector2(
                    m_Position.X + Width / 2 - measuredText.X / 2,
                    m_Position.Y + Height / 2 - measuredText.Y / 2 - (isMultiLine ? m_Font.LineSpacing / 2 : 0));
            }
            base.Draw(i_GameTime);
            m_SpriteBatch.DrawString(m_Font, m_TextMessage, m_TextPosition, r_FontColor);
        }
    }
}
