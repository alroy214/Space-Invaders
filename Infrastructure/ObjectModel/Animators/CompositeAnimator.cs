//*** Guy Ronen � 2008-2011 ***//
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel.Animators
{
    public class CompositeAnimator : SpriteAnimator
    {
        private readonly Dictionary<string, SpriteAnimator> m_AnimationsDictionary = 
            new Dictionary<string, SpriteAnimator>();
        protected readonly List<SpriteAnimator> m_AnimationsList = new List<SpriteAnimator>();

        // CTORs

        // CTOR: Me as an AnimationsManager
        public CompositeAnimator(Sprite i_BoundSprite, string i_Name = "AnimationsManager")
            : this(i_Name, TimeSpan.Zero, i_BoundSprite)
        {
            this.Enabled = false;
        }
        
        // CTOR: me as a ParallelAnimations animation:
        public CompositeAnimator(string i_Name, TimeSpan i_AnimationLength, Sprite i_BoundSprite, params SpriteAnimator[] i_Animations)
            : base (i_Name, i_AnimationLength)
        {
            this.BoundSprite = i_BoundSprite;
            foreach (SpriteAnimator animation in i_Animations)
            {
                this.Add(animation);
            }
        }

        public void Add(SpriteAnimator i_Animation)
        {
            i_Animation.BoundSprite = this.BoundSprite;
            i_Animation.Enabled = true;
            this.m_AnimationsDictionary.Add(i_Animation.Name, i_Animation);
            this.m_AnimationsList.Add(i_Animation);
        }

        public void Remove(string i_AnimationName)
        {
            this.m_AnimationsDictionary.TryGetValue(i_AnimationName, out SpriteAnimator animationToRemove);
            if (animationToRemove != null)
            {
                this.m_AnimationsDictionary.Remove(i_AnimationName);
                this.m_AnimationsList.Remove(animationToRemove);
            }
        }

        public SpriteAnimator this[string i_Name]
        {
            get
            {
                this.m_AnimationsDictionary.TryGetValue(i_Name, out SpriteAnimator retVal);
                return retVal;
            }            
        }

        public override void Restart()
        {
            base.Restart();
            foreach (SpriteAnimator animation in this.m_AnimationsList)
            {
                animation.Restart();
            }
        }

        public override void Restart(TimeSpan i_AnimationLength)
        {
            base.Restart(i_AnimationLength);
            foreach (SpriteAnimator animation in this.m_AnimationsList)
            {
                animation.Restart();
            }
        }

        protected override void RevertToOriginal()
        {
            foreach (SpriteAnimator animation in this.m_AnimationsList)
            {
                animation.Reset();
            }
        }

        protected override void CloneSpriteInfo()
        {
            base.CloneSpriteInfo();
            foreach (SpriteAnimator animation in this.m_AnimationsList)
            {
                animation.m_OriginalSpriteInfo = this.m_OriginalSpriteInfo;
            }
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            foreach (SpriteAnimator animation in this.m_AnimationsList)
            {
                animation.Update(i_GameTime);
            }
        }
    }
}