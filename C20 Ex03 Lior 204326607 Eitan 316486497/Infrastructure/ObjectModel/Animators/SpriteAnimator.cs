//*** Guy Ronen © 2008-2011 ***//
using System;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel.Animators
{
    public abstract class SpriteAnimator
    {
        private Sprite m_BoundSprite;
        private TimeSpan m_AnimationLength;
        private TimeSpan m_TimeLeft;
        private bool m_IsFinished = false;
        private bool m_Enabled = false;
        private bool m_Initialized = false;
        private readonly string r_Name;
        protected bool m_ResetAfterFinish = true;
        protected internal Sprite m_OriginalSpriteInfo;

        public event EventHandler Finished;

        protected virtual void OnFinished()
        {
            if (m_ResetAfterFinish)
            {
                Reset();
                this.m_IsFinished = true;
            }

            Finished?.Invoke(this, EventArgs.Empty);

            this.Enabled = false;
        }

        protected SpriteAnimator(string i_Name, TimeSpan i_AnimationLength)
        {
            this.r_Name = i_Name;
            this.m_AnimationLength = i_AnimationLength;
        }

        protected internal Sprite BoundSprite
        {
            get
            {
                return this.m_BoundSprite;
            }
            set
            {
                this.m_BoundSprite = value;
            }
        }

        public string Name
        {
            get
            {
                return this.r_Name;
            }
        }

        public bool Enabled
        {
            get
            {
                return this.m_Enabled;
            }
            set
            {
                this.m_Enabled = value;
            }
        }

        public bool IsFinite
        {
            get
            {
                return this.m_AnimationLength != TimeSpan.Zero;
            }
        }

        public bool ResetAfterFinish
        {
            get
            {
                return this.m_ResetAfterFinish;
            }
            set
            {
                this.m_ResetAfterFinish = value;
            }
        }

        public virtual void Initialize()
        {
            if (!this.m_Initialized)
            {
                this.m_Initialized = true;
                CloneSpriteInfo();
                Reset();
            }
        }

        protected virtual void CloneSpriteInfo()
        {
            if (this.m_OriginalSpriteInfo == null)
            {
                this.m_OriginalSpriteInfo = this.m_BoundSprite.ShallowClone();
            }
        }

        public void Reset()
        {
            Reset(this.m_AnimationLength);
        }

        public void Reset(TimeSpan i_AnimationLength)
        {
            if (!this.m_Initialized)
            {
                Initialize();
            }
            else
            {
                this.m_AnimationLength = i_AnimationLength;
                this.m_TimeLeft = this.m_AnimationLength;
                this.IsFinished = false;
            }

            RevertToOriginal();
        }

        protected abstract void RevertToOriginal();

        public void Pause()
        {
            this.Enabled = false;
        }

        public void Resume()
        {
            this.m_Enabled = true;
        }

        public virtual void Restart()
        {
            Restart(this.m_AnimationLength);
        }

        public virtual void Restart(TimeSpan i_AnimationLength)
        {
            Reset(i_AnimationLength);
            Resume();
        }

        protected TimeSpan AnimationLength
        {
            get
            {
                return this.m_AnimationLength;
            }
        }

        public bool IsFinished
        {
            get
            {
                return this.m_IsFinished;
            }
            protected set
            {
                if (value != this.m_IsFinished)
                {
                    this.m_IsFinished = value;
                    if (this.m_IsFinished == true)
                    {
                        OnFinished();
                    }
                }
            }
        }

        public void Update(GameTime i_GameTime)
        {
            if (!this.m_Initialized)
            {
                Initialize();
            }

            if (this.Enabled && !this.IsFinished)
            {
                if (this.IsFinite)
                {
                    // check if we should stop animating:
                    this.m_TimeLeft -= i_GameTime.ElapsedGameTime;

                    if (this.m_TimeLeft.TotalSeconds < 0)
                    {
                        this.IsFinished = true;
                    }
                }

                if (!this.IsFinished)
                {
                    // we are still required to animate:
                    DoFrame(i_GameTime);
                }
            }
        }

        protected abstract void DoFrame(GameTime i_GameTime);
    }
}