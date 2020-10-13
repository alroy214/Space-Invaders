using Microsoft.Xna.Framework;
using System;
namespace Infrastructure.ObjectModel.Animators.ConcreteAnimators
{

    public class RotateAnimator : SpriteAnimator
    {
        private readonly float r_SpinPerSecond;

        // CTORs

        public RotateAnimator(string i_Name, TimeSpan i_AnimationLength, float i_SpinsPerSecond)
            : base(i_Name, i_AnimationLength)
        {
            r_SpinPerSecond = i_SpinsPerSecond;
        }

        public RotateAnimator(TimeSpan i_AnimationLength, float i_SpinsPerSecond)
            : base("RotateAnimator", i_AnimationLength)
        {
            r_SpinPerSecond = i_SpinsPerSecond;

        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            BoundSprite.Rotation += (float)(Math.PI * 2 * r_SpinPerSecond * i_GameTime.ElapsedGameTime.TotalSeconds);
        }

        protected override void RevertToOriginal()
        {
            BoundSprite.AngularVelocity = m_OriginalSpriteInfo.AngularVelocity;
            BoundSprite.Rotation = m_OriginalSpriteInfo.Rotation;
        }


    }
}