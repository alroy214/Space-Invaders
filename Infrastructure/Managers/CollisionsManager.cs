//*** Guy Ronen © 2008-2011 ***//
using System;
using System.Collections.Generic;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using Infrastructure.ServiceInterfaces;

namespace Infrastructure.Managers
{
    // TODO 10: Implement the collisions manager service:
    public class CollisionsManager : GameService, ICollisionsManager
    {
        protected readonly List<ICollidable> r_Collidables = new List<ICollidable>();

        public CollisionsManager(Game i_Game) :
            base(i_Game, int.MaxValue)
        { }

        protected override void RegisterAsService()
        {
            this.Game.Services.AddService(typeof(ICollisionsManager), this);
        }

        public void AddObjectToMonitor(ICollidable i_Collidable)
        {
            if (!this.r_Collidables.Contains(i_Collidable))
            {
                this.r_Collidables.Add(i_Collidable);
                i_Collidable.PositionChanged += collidable_PositionChanged;
                i_Collidable.Disposed += collidable_Disposed;
            }
        }

        private void collidable_PositionChanged(object i_Collidable)
        {
            if (i_Collidable is ICollidable)
            {// to be on the safe side :)
                checkCollision(i_Collidable as ICollidable);
            }
        }

        private void checkCollision(ICollidable i_Source)
        {
            List<ICollidable> collidedComponents = new List<ICollidable>();

            // finding who collided with i_Source:
            foreach (ICollidable target in r_Collidables)
            {
                if (i_Source.Visible && i_Source != target && target.Visible)
                {
                    if (target.CheckCollision(i_Source))
                    {
                        collidedComponents.Add(target);
                    }
                }
            }

            // Informing i_Source and all the collided targets about the collision:
            foreach (ICollidable target in collidedComponents)
            {
                target.Collided(i_Source);
                i_Source.Collided(target);
            }
        }

        private void collidable_Disposed(object sender, EventArgs e)
        {
            if (sender is ICollidable collidable && this.r_Collidables.Contains(collidable))
            {
                collidable.PositionChanged -= collidable_PositionChanged;
                collidable.Disposed -= collidable_Disposed;
                r_Collidables.Remove(collidable);
            }
        }
    }
    // -- end of TODO 10
}