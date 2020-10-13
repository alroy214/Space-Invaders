using Microsoft.Xna.Framework;
using System;

namespace Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class ShrinkAnimator : SpriteAnimator
    {
        private TimeSpan m_ShrinkLength;

        public ShrinkAnimator(string i_Name, TimeSpan i_AnimationLength)
            : base(i_Name, i_AnimationLength)
        {
        }

        public ShrinkAnimator(TimeSpan i_AnimationLength)
            : base("ShrinkAnimation", i_AnimationLength)
        {
            m_ShrinkLength = i_AnimationLength;
        }

        protected override void RevertToOriginal()
        {
            BoundSprite.Scales = m_OriginalSpriteInfo.Scales;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            TimeSpan currentSize = this.m_ShrinkLength;

            currentSize -= i_GameTime.ElapsedGameTime;
            BoundSprite.Scales *= new Vector2((float)currentSize.TotalSeconds / (float) m_ShrinkLength.TotalSeconds);
        }

        public TimeSpan ShrinkLength
        {
            get
            {
                return m_ShrinkLength;
            }
            set
            {
                m_ShrinkLength = value;
            }
        }
    }
}