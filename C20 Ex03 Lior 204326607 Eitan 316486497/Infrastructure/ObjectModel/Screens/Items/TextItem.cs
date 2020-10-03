using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.Infrastructure.ObjectModel.Screens.Items
{
    public class TextItem : ClickableItem
    {
        private SpriteFont m_Font;
        private Color m_FontColor;
        private string m_TextMessage;
        private Vector2 r_Offset;

        public TextItem(GameScreen i_GameScreen, string i_TextMessage, Vector2 i_Position, int i_ItemNumber)
            : this(i_GameScreen, i_TextMessage, i_Position, i_ItemNumber, Color.White, Color.Black)
        {
        }
        public TextItem(GameScreen i_GameScreen, string i_TextMessage, Vector2 i_Position, int i_ItemNumber, Color i_ActiveColor)
            : this(i_GameScreen, i_TextMessage, i_Position, i_ItemNumber, i_ActiveColor, Color.Black)
        {
        }

        public TextItem(GameScreen i_GameScreen, string i_TextMessage, Vector2 i_Position, int i_ItemNumber, Color i_ActiveColor, Color i_FontColor)
            : base(@"Screens/bubble3", i_GameScreen, i_ItemNumber)
        {
            r_Offset = i_Position;
            m_TextMessage = i_TextMessage;
            m_ActiveColor = i_ActiveColor;
            m_FontColor = i_FontColor;
            m_Scales = new Vector2(0.45f, 0.45f);
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
            m_Position = new Vector2(r_Offset.X - Width/2, r_Offset.Y - Height/2);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            m_Font = Game.Content.Load<SpriteFont>(@"Fonts\Consolas");
        }

        private Vector2 text = Vector2.Zero;

        public override void Draw(GameTime i_GameTime)
        {
            if(text == Vector2.Zero)
            {
                Vector2 measuredText = m_Font.MeasureString(m_TextMessage);
                text = new Vector2(
                    m_Position.X + Width / 2 - measuredText.X / 2,
                    m_Position.Y + Height / 2 - measuredText.Y / 2);
            }
            base.Draw(i_GameTime);
            m_SpriteBatch.DrawString(m_Font, m_TextMessage, text, m_FontColor);
        }
    }



}
