//*** Guy Ronen © 2008-2011 ***//
using System;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class BlinkAnimator : SpriteAnimator
    {
        private TimeSpan m_BlinkLength;
        private TimeSpan m_TimeLeftForNextBlink;

        // CTORs
        public BlinkAnimator(string i_Name, TimeSpan i_BlinkLength, TimeSpan i_AnimationLength)
            : base(i_Name, i_AnimationLength)
        {
            this.m_BlinkLength = i_BlinkLength;
            this.m_TimeLeftForNextBlink = i_BlinkLength;
        }

        public BlinkAnimator(TimeSpan i_BlinkLength, TimeSpan i_AnimationLength)
            : this("BlinkAnimation", i_BlinkLength, i_AnimationLength)
        {
            this.m_BlinkLength = i_BlinkLength;
            this.m_TimeLeftForNextBlink = i_BlinkLength;
        }

        protected override void RevertToOriginal()
        {
            this.BoundSprite.Visible = this.m_OriginalSpriteInfo.Visible;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            this.m_TimeLeftForNextBlink -= i_GameTime.ElapsedGameTime;
            if (this.m_TimeLeftForNextBlink.TotalSeconds < 0)
            {
                // we have elapsed, so blink
                this.BoundSprite.Visible = !this.BoundSprite.Visible;
                this.m_TimeLeftForNextBlink = this.m_BlinkLength;
            }
        }

        public TimeSpan BlinkLength
        {
            get
            {
                return this.m_BlinkLength;
            }
            set
            {
                this.m_BlinkLength = value;
            }
        }
    }
}
