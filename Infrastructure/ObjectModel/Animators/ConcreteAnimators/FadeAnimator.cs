using Microsoft.Xna.Framework;
using System;

namespace Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class FadeAnimator : SpriteAnimator
    {
        public FadeAnimator(string i_Name, TimeSpan i_AnimationLength)
            : base(i_Name, i_AnimationLength)
        {
        }

        public FadeAnimator(TimeSpan i_AnimationLength)
            : base("FadeAnimation", i_AnimationLength)
        {
        }

        protected override void RevertToOriginal()
        {
            BoundSprite.Opacity = m_OriginalSpriteInfo.Opacity;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            float currentOpacity = (float) i_GameTime.ElapsedGameTime.TotalSeconds * (float) m_OriginalSpriteInfo.Opacity / (float) AnimationLength.TotalSeconds;
            float minOpacity = 0;
            float maxOpacity = BoundSprite.Opacity;

            BoundSprite.Opacity -= MathHelper.Clamp(currentOpacity, minOpacity, maxOpacity);
        }
    }
}
